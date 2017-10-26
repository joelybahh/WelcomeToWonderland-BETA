using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace WW.CustomPhysics {
    /// <summary>
    /// Desc: This script handles the logic for snapping the flick switches
    ///       to the left and right, depending on their rotation. If it exceeds
    ///       the min threshold, and we are faceing left, face right. 
    ///       Furthermore, also invokes a unity event for left and right being triggered.
    /// Author: Joel Gabriel
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(HingeJoint))]
    public class FlickSwitch : MonoBehaviour {

        public enum eSwitchState {
            LEFT,
            RIGHT
        }

        [SerializeField] UnityEvent m_LeftON;
        [SerializeField] UnityEvent m_RightOFF;

        [SerializeField] private bool startLeft = false;
        [SerializeField] private float offsetOn = 91.0f;
        [SerializeField] private float offsetOff = 93.0f;

        private HingeJoint m_switchJoint;
        private JointSpring m_spring;

        private int m_switchOn = 0;

        private float currentRot;
        private float m_min;
        private float m_max;    
        
        public eSwitchState m_switchState;
       
        void Awake() {
            // Get the hinge joint off the switch.
            m_switchJoint = GetComponent<HingeJoint>();

            // store a reference to our hing spring, in our spring variable.
            m_spring = m_switchJoint.spring;

            // Setup the min and max, to the min and max on the joints limits
            m_max = m_switchJoint.limits.max;
            m_min = m_switchJoint.limits.min;

            // Set the int representation of the enum to 0, essentially saying  eSwitchState.LEFT; (except now we can do math related things)
            m_switchOn = 0;

            // If we want the hinge to start left.
            if (startLeft) {
                // Set the spring target position to be the max rotation take 1.
                m_spring.targetPosition = m_max - 1;

                // Set the spring on the hinge, to the altered spring variable.
                m_switchJoint.spring = m_spring;

                // Set the state to left
                m_switchState = eSwitchState.LEFT;

                // Invoke the leftOn logic
                m_LeftON.Invoke ();
            } 
            else {
                // Otherwise do the same thing we did above for the right.

                m_spring.targetPosition = m_min;
                m_switchJoint.spring = m_spring;
                
                m_switchState = eSwitchState.RIGHT;
                m_RightOFF.Invoke ();
            }
        }

        void Update() {
            // set our rotation float to the euler angles of the y axis every frame.
            currentRot = transform.rotation.eulerAngles.y;

            // Handles the spring snapping, based on the flickers rotation.
            UpdateSwitchLimits();

            // Check the switch state
            switch ( m_switchState ) {
                // If its left, check for switch right.
                case eSwitchState.LEFT: CheckForSwitchRight(); break;
                // If its right, check for switch left.
                case eSwitchState.RIGHT: CheckForSwitchLeft(); break;                    
            }           
        }

        /// <summary>
        /// Sets the enum state with an int parameter.
        /// </summary>
        /// <param name="a_on"></param>
        private void SetSwitch( int a_on ) {
            m_switchOn = a_on;
            m_switchState = (eSwitchState) m_switchOn;

        }

        /// <summary>
        /// Checks if the current rotation, exceeds the rotation 
        /// for snapping to the right. If so update the enum.
        /// </summary>
        private void CheckForSwitchRight() {
            if ( currentRot < offsetOn ) {
                SetSwitch(1);
                m_RightOFF.Invoke();
            }
        }

        /// <summary>
        /// Checks if the current rotation, exceeds the rotation 
        /// for snapping to the left. If so update the enum.
        /// </summary>
        private void CheckForSwitchLeft() {
            if ( currentRot > offsetOff ) {
                SetSwitch(0);
                m_LeftON.Invoke();
            }

        }

        /// <summary>
        /// Updates the switches limits based on the current switch state.
        /// Therefore when it changes, so does the springs target pos.
        /// This creates the snap effect.
        /// </summary>
        private void UpdateSwitchLimits() {
            if ( m_switchState == eSwitchState.RIGHT ) m_spring.targetPosition = m_min;
            else m_spring.targetPosition = m_max - 1;

            m_switchJoint.spring = m_spring;
        }
    }
}