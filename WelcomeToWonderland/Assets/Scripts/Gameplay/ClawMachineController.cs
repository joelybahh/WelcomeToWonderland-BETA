using UnityEngine;

namespace WW.CustomPhysics {
    /// <summary>
    /// Author: Joel Gabriel    
    /// Desc:   This class is used to handle the entire I/O of the claw machine 
    /// TODO:   The use of a configurable joint? Or a look at method when grabbed (rotational math :D )
    /// </summary>
    public class ClawMachineController : MonoBehaviour {

        #region Public Variables

        [HideInInspector]
        public eLeverDir CurLeverDir = eLeverDir.NULL;

        public eMachineState CurMachineState {
            get { return m_curMachineState; }
            set { m_curMachineState = value; }
        }

        public bool HasItem {
            get { return m_hasItem; }
            set { m_hasItem = value; }
        }

        [HideInInspector]
        public Rigidbody GrabbedItem;

        #endregion

        #region Serialized Variables

        [Header("Claw Machine State Settings")]
        [SerializeField]
        private eMachineState m_initalMachineState;

        [Header ("Claw Movement Axis References")]
        [SerializeField]
        private Transform m_xCarriageRef;
        [SerializeField]
        private Transform m_zCarriageRef;
        [SerializeField]
        private Transform m_dropperRef;

        [Header ("Min & Max Settings")]
        [SerializeField]
        private float m_xMin;
        [SerializeField]
        private float m_xMax;
        [SerializeField]
        private float m_zMin;
        [SerializeField]
        private float m_zMax;

        [Header ("Claw Move Speed")]
        [SerializeField]
        private float m_clawSpeed;

        [Header ("Claw Dropper Settings")]
        [SerializeField]
        private Transform m_dropperMinTransform;
        [SerializeField]
        private Transform m_dropperMaxTransform;
        #endregion

        #region Private Variables
        private float m_curEulerX;
        private float m_curEulerZ;

        private float m_thresholdMax = 345.5f;
        private float m_thresholdMin = 14f;

        private float m_dropperMinHeight;
        private float m_dropperMaxHeight;

        private float m_clawGrabTimer = 0.0f;
        private float m_clawGrabTime = 1.35f;

        private eMachineState m_curMachineState;

        private bool m_waitComplete;
        private bool m_hasItem;

        HingeJoint[]  m_joints;
        JointSpring[] m_jointSprings;

        #endregion

        private void Start() {
            m_curMachineState = m_initalMachineState;

            m_dropperMinHeight = m_dropperMinTransform.position.y;
            m_dropperMaxHeight = m_dropperMaxTransform.localPosition.y;

            m_waitComplete = false;
            m_hasItem = false;

            m_joints = m_dropperRef.GetChild(0).GetComponentsInChildren<HingeJoint>();
            m_jointSprings = new JointSpring[3];
        }

        private void Update() {
            switch ( m_curMachineState ) {
                case eMachineState.OFF: UpdateOffState(); break;
                case eMachineState.GRABBING: UpdateGrabAutomation(); break;
                case eMachineState.CONTROLLED: UpdateClawControls(); break;
            }
        }

        #region Main Methods

        /// <summary>
        /// This is the update loop for the claw controllers with the joystick
        /// </summary>
        private void UpdateClawControls() {
            CheckLeverDirection();
            UpdateCurMovement();

            // If the lever hasn't exceeded any of the rotational thresholds, then we assume its centered
            // and therefore need to set its leverDir back to centred
            if ( LeverCentered() ) {
                CurLeverDir = eLeverDir.CENTER;
            }
        }

        /// <summary>
        /// This is the update loop for when you press the big red button to drop the claw.
        /// It handles all the automation logic regardless to if you got an item or not.
        /// </summary>
        private void UpdateGrabAutomation() {
            // if the dropper is above the minimum height and has no item yet.. Go down.
            if ( m_dropperRef.position.y <= m_dropperMinHeight && !m_hasItem )
                m_dropperRef.position -= Vector3.up * m_clawSpeed * Time.deltaTime;
            else {
                // If we are at the bottom OR we have an item, CLOSE the claw
                UpdateClaw(eClawState.CLOSE);

                if ( m_waitComplete == false ) {
                    #region WAIT UNTIL GRAB DELAY HAS FINISHED
                    m_clawGrabTimer += Time.deltaTime;
                    if ( m_clawGrabTimer >= m_clawGrabTime ) {
                        m_clawGrabTimer = 0.0f;
                        m_waitComplete = true;
                    }
                    #endregion
                } else {
                    // Check if we actually grabbed something,
                    // and then actually 'grab' it           
                    if ( GrabbedItem != null ) GrabItem();

                    // now if we aren't at the top of the screen.. go up
                    if ( m_dropperRef.position.y <= m_dropperMaxHeight ) {
                        m_dropperRef.position += Vector3.up * m_clawSpeed * Time.deltaTime;
                    } else {
                        // Move back until we hit the back wall, if we hit the back wall (GoBack == false),
                        if ( !GoBack() ) {  //(if we can't go back any further)
                            // Once we hit the back wall, recenter.
                            if ( ReCenter() ) {
                                // Once we have recentered...
                                // Peform the final release of the claw (this happens with or without a succesful item grab attempt)
                                FinalItemRelease();
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This is the update loop if the claw machine is off.
        /// </summary>
        private void UpdateOffState() {
            /* TODO:
             * Wait and check for the required stage to be 'PASSED' 
             */
        }

        #endregion

        #region Player Claw Control Methods

        /// <summary>
        /// Checks the current direction of the lever, and 
        /// sets the direction state based on this information
        /// </summary>
        private void CheckLeverDirection() {
            Vector3 eulerAngles = transform.localEulerAngles;

            m_curEulerX = eulerAngles.x;
            m_curEulerZ = eulerAngles.z;

            if ( JoystickLeft() ) CurLeverDir = eLeverDir.LEFT;
            if ( JoystickRight() ) CurLeverDir = eLeverDir.RIGHT;
            if ( JoystickForward() ) CurLeverDir = eLeverDir.FORWARD;
            if ( JoystickBack() ) CurLeverDir = eLeverDir.BACK;
        }

        /// <summary>
        /// Checks what the current lever direction is, and updates
        /// the claw movement accordingly
        /// </summary>
        private void UpdateCurMovement() {
            switch ( CurLeverDir ) {
                case eLeverDir.LEFT: UpdateLeft(); break;
                case eLeverDir.RIGHT: UpdateRight(); break;
                case eLeverDir.FORWARD: UpdateForward(); break;
                case eLeverDir.BACK: UpdateBack(); break;
            }
        }

        #endregion

        #region Joystick Face Direction Methods

        /// <summary>
        /// Checks to see if the lever is centered
        /// </summary>
        /// <returns>true if the lever is centered, false if not</returns>
        private bool LeverCentered() {
            if ( !JoystickLeft() && !JoystickRight() && !JoystickForward() && !JoystickBack() ) {
                return true;
            } else return false;
        }

        /// <summary>
        /// Checks to see if the lever is facing left.
        /// </summary>
        /// <returns>true if the lever is facing left, false if not</returns>
        private bool JoystickLeft() {
            if ( ( m_curEulerZ < 350 && m_curEulerZ > 330 ) && CurLeverDir != eLeverDir.LEFT ) {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks to see if the lever is facing right.
        /// </summary>
        /// <returns>true if the lever is facing right, false if not</returns>
        private bool JoystickRight() {
            if ( m_curEulerZ > m_thresholdMin && m_curEulerZ < m_thresholdMin + 16 && CurLeverDir != eLeverDir.RIGHT ) {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks to see if the lever is facing forward.
        /// </summary>
        /// <returns>true if the lever is facing forward, false if not</returns>
        private bool JoystickForward() {
            if ( ( m_curEulerX < m_thresholdMax && m_curEulerX > 330 ) && CurLeverDir != eLeverDir.FORWARD ) {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks to see if the lever is facing back.
        /// </summary>
        /// <returns>true if the lever is facing back, false if not</returns>
        private bool JoystickBack() {
            if ( m_curEulerX > m_thresholdMin && m_curEulerX < m_thresholdMin + 16 && CurLeverDir != eLeverDir.BACK ) {
                return true;
            }
            return false;
        }

        #endregion

        #region Claw Movement Helper Methods

        /// <summary>
        /// Updates the claw movement for left
        /// </summary>
        private void UpdateLeft() {
            if ( m_xCarriageRef.localPosition.x <= m_xMax )
                m_xCarriageRef.position += m_xCarriageRef.right * m_clawSpeed * Time.deltaTime;
        }

        /// <summary>
        /// Updates the claw movement for right
        /// </summary>
        private void UpdateRight() {
            if ( m_xCarriageRef.localPosition.x >= m_xMin )
                m_xCarriageRef.position += -m_xCarriageRef.right * m_clawSpeed * Time.deltaTime;
        }

        /// <summary>
        /// Updates the claw movement for forward
        /// </summary>
        private void UpdateForward() {
            if ( m_zCarriageRef.localPosition.z >= m_zMin )
                m_zCarriageRef.position += -m_xCarriageRef.forward * m_clawSpeed * Time.deltaTime;
        }

        /// <summary>
        /// Updates the claw movement for back
        /// </summary>
        private void UpdateBack() {
            if ( m_zCarriageRef.localPosition.z <= m_zMax )
                m_zCarriageRef.position += m_xCarriageRef.forward * m_clawSpeed * Time.deltaTime;
        }

        /// <summary>
        /// Moves back while its not at the back wall
        /// </summary>
        /// <returns>Returns a bool that returns true when you can move back, and false when you hit the wall</returns>
        private bool GoBack() {
            if ( m_zCarriageRef.localPosition.z <= m_zMax /*- 0.17f*/) {
                m_zCarriageRef.position += m_xCarriageRef.forward * m_clawSpeed * Time.deltaTime;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Recenters the claw
        /// </summary>
        /// <returns>true when the claw is recentered, false when its not</returns>
        private bool ReCenter () {
            /* TODO: Firstly check if we are centered, if so return true,
             * Otherwise, Get was side from the center we are at, 
             * and move back towards the center. Once we are there return true,
             * otherwise keep returning false */
            return true;
        }

        /// <summary>
        /// Function that sets the state to center
        /// </summary>
        public void StopMovement() {
            CurLeverDir = eLeverDir.NULL;
        }

        #endregion

        #region Claw Automation Methods

        /// <summary>
        /// This method simply Updates the claw mouth to go
        /// from open, to close.
        /// </summary>
        /// <param name="a_newClawState">This enum determines whether you want to open or close the claw</param>
        private void UpdateClaw( eClawState a_newClawState ) {
            // Loop through and assign the jointSpring info to the newly created array
            for ( int i = 0; i < 3; i++ ) {
                m_jointSprings[i] = m_joints[i].spring;
                m_jointSprings[i].spring = m_joints[i].spring.spring;
                m_jointSprings[i].damper = m_joints[i].spring.damper;

                switch ( a_newClawState ) {
                    case eClawState.CLOSE: m_jointSprings[i].targetPosition = -15; break;
                    case eClawState.OPEN: m_jointSprings[i].targetPosition = 15; break;
                }
            }

            // loop through again to assign the updated information to the real spring joint
            for ( int i = 0; i < 3; i++ ) {
                m_joints[i].spring = m_jointSprings[i];
            }
        }

        /// <summary>
        /// Grabs an item if there is actually one to grab
        /// </summary>
        private void GrabItem() {
            // Set the grabbed item to kinematic
            GrabbedItem.isKinematic = true;
            // Set its parent to the claw, therefore 'grabbing' the item
            GrabbedItem.transform.parent = m_dropperRef;
        }

        /// <summary>
        /// Drops an item if there is actually one to grab
        /// </summary>
        private void DropItem() {
            GrabbedItem.isKinematic = false;
            GrabbedItem.transform.parent = null;
            GrabbedItem = null;
        }

        /// <summary>
        /// Performs the logic for the final phase of automation, The release.
        /// </summary>
        private void FinalItemRelease() {
            UpdateClaw(eClawState.OPEN);

            m_hasItem = false;

            // Check if we actually grabbed something,
            // if so simply 'drop' it  
            if ( GrabbedItem != null ) {
                DropItem();
            }

            m_curMachineState = eMachineState.CONTROLLED;
            m_waitComplete = false;
        }

        #endregion
    }

    #region public enums

    public enum eLeverDir {
        //\\
        FORWARD,
        BACK,
        LEFT,
        RIGHT,
        CENTER,
        NULL
    }

    public enum eMachineState {
        CONTROLLED,
        GRABBING,
        OFF
    }

    public enum eClawState {
        OPEN,
        CLOSE
    }

    #endregion
}

