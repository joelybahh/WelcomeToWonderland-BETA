//          ======= Copyright (c) BlackPandaStudios, All rights reserved. =======
//              _____________________________________________________________ 
// <author>    | -  Joel Gabriel                                             |  
// <date>      | -  01/04/2017                                               |  
// <name>      | -  VRBasicController.cs                                     |  
// <summary>   | -  This class utilizes SteamVR functionality to get SteamVR |            
//             |    Controller Input, and create simple interactions with it.|            
//             |    It currently only handles swipe detection to rotate rig. |
//             |_____________________________________________________________|

using UnityEngine;
using UnityEngine.Events;

namespace Loco.VR.Input {
    public class VRBasicController : MonoBehaviour {

		public enum ViveButton{
			APPLICATION_MENU = Valve.VR.EVRButtonId.k_EButton_ApplicationMenu,
			SYSTEM_MENU = Valve.VR.EVRButtonId.k_EButton_System,
			GRIP = Valve.VR.EVRButtonId.k_EButton_Grip,
			HAIR_TRIGGER = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger,
			TOUCHPAD = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad/*,
			TOUCHPAD_LEFT,
			TOUCHPAD_RIGHT,
			TOUCHPAD_TOP,
			TOUCHPAD_BOTTON*/
		}

		//public ViveButton buttonToUse;

        #region Serialized Variables
        [SerializeField] private UnityEvent m_OnSwipeLeft      = null;
        [SerializeField] private UnityEvent m_OnSwipeRight     = null;

        #endregion

        #region Private Variables

        private enum SwipeDirection {
            SWIPE_LEFT,
            SWIPE_RIGHT,
            SWIPE_UP,
            SWIP_DOWN,
            NULL
        };

        private SteamVR_TrackedObject     m_trackedObj = null;
        private SteamVR_Controller.Device m_deivce     = null;

        private SwipeDirection m_swipeDir            = SwipeDirection.NULL;
        private Vector2        m_startPos            = Vector2.zero;
        private Vector2        m_endPos              = Vector2.zero;
        private Vector3        m_dir                 = Vector3.zero;
        private Vector3        m_startInteraction    = Vector3.zero;
        private Vector3        m_endInteraction      = Vector3.zero;
        private Transform      m_holding             = null;
        private bool           m_touched             = false;
        private bool           m_holdingObj          = false;
        private bool           m_interacting         = false;

        #endregion

        [HideInInspector] public bool hoveringRope = false;
        [HideInInspector] public string ropeTag = "Rope";

        #region Unity Methods

        void Start() {
            m_trackedObj = GetComponent<SteamVR_TrackedObject>();
             m_deivce = SteamVR_Controller.Input((int)m_trackedObj.index);
        }

        void Update() {
           m_deivce = SteamVR_Controller.Input((int)m_trackedObj.index);

            #region Vive Touchpad logic

            if (isTouching()) {
                UpdateTouch();
            } else {
                StopTouching();
            }

            #endregion

            m_endPos = Vector2.zero;
        }

        void OnTriggerEnter(Collider col) {
            if(col.tag == "Interactable") {
                m_interacting = true;
                m_holding = col.transform;
            }

            if (col.tag == ropeTag)
            {
                // Set pulley stuff up
                hoveringRope = true;
                Debug.Log("Hovering Rope");
            }
        }
        

        void OnTriggerExit(Collider col) {
            if (col.tag == "Interactable") {
                m_interacting = false;
            }

            if (col.tag == ropeTag)
            {
                // Reset pulley stuff up
                hoveringRope = false;
                Debug.Log("Stopped Hovering Rope");
            }
        }


        #endregion

        #region Private Functions

        void UpdateTouch(){
            m_startPos = m_deivce.GetAxis();
            m_touched = true;
        }
        void StopTouching()
        {
            if (m_touched)
            {
                m_endPos = m_deivce.GetAxis();
                m_touched = false;
                m_dir = m_endPos - m_startPos;
                Vector3.Normalize(m_dir);

                if (m_dir.x > .3 && (m_dir.y < .3 && m_dir.y > -.3))
                {
                    m_swipeDir = SwipeDirection.SWIPE_LEFT;
                    m_OnSwipeLeft.Invoke();
                }
                if (m_dir.x < -.3 && (m_dir.y < .3 && m_dir.y > -.3))
                {
                    m_swipeDir = SwipeDirection.SWIPE_RIGHT;
                    m_OnSwipeRight.Invoke();
                }
                if (m_dir.y > .3 && (m_dir.x < .3 && m_dir.x > -.3))
                {
                    m_swipeDir = SwipeDirection.SWIPE_UP;
                }
                if (m_dir.y < -.3 && (m_dir.x < .3 && m_dir.x > -.3))
                {
                    m_swipeDir = SwipeDirection.SWIP_DOWN;
                }
                //Debug.Log(m_swipeDir);
            }
        }
		public bool isTouching() {if(m_deivce!=null) { return (m_deivce.GetAxis().x != 0 || m_deivce.GetAxis().y != 0); }else return false;}

		public Vector3 GetFaceDirection() { return transform.forward; }

        #endregion

        #region Public funtions

		public Vector3 getDeviceVelocity(){
			if(m_deivce != null) {
				return m_deivce.velocity;
			}
			return Vector3.zero;
		}

        public bool GetTrigger()
        {
            if (m_deivce != null) { return m_deivce.GetPress(SteamVR_Controller.ButtonMask.Trigger); }
            return false;
        }
        public bool GetTriggerDown(){
            if (m_deivce != null) { return m_deivce.GetPressDown(SteamVR_Controller.ButtonMask.Trigger); }
            return false;
        }
        public bool GetTriggerUp()
        {
            if (m_deivce != null) {  return m_deivce.GetPressUp(SteamVR_Controller.ButtonMask.Trigger); }
            return false;
        }

        public bool GetButton(Valve.VR.EVRButtonId a_buttonMask)
        {
            if (m_deivce != null) { return m_deivce.GetPress(a_buttonMask); }
            return false;
        }

		public bool GetButtonDown(Valve.VR.EVRButtonId a_buttonMask)
		{
			if (m_deivce != null) { return m_deivce.GetPressDown(a_buttonMask); }
			return false;
		}

		public bool GetButtonUp(Valve.VR.EVRButtonId a_buttonMask)
		{
			if (m_deivce != null) { return m_deivce.GetPressUp(a_buttonMask); }
			return false;
		}

        public bool GetGrip()
        {
            if (m_deivce != null) { return m_deivce.GetPress(SteamVR_Controller.ButtonMask.Grip); }
            return false;
        }
        public bool GetGripDown()
        {
            if (m_deivce != null) return m_deivce.GetPressDown(SteamVR_Controller.ButtonMask.Grip);
            return false;
        }
        public bool GetGripUp()
        {
            if (m_deivce != null) return m_deivce.GetPressUp(SteamVR_Controller.ButtonMask.Grip);
            return false;
        }

        public void TriggerHapticPulse(ushort a_amount = 3999) {
            m_deivce.TriggerHapticPulse(a_amount);
        }

        #endregion
    }
}
