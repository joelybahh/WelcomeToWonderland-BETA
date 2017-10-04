using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace WW.Interactables {
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(HingeJoint))]
    public class PowerOutletSwitch : MonoBehaviour {

        public enum eSwitchState {
            ON,
            OFF
        }
        [SerializeField]
        UnityEvent m_OnON;
        [SerializeField]
        UnityEvent m_OnOFF;

        private HingeJoint m_switchJoint;
        private JointSpring m_spring;

        private int m_switchOn = 0;

        private float m_min;
        private float m_max;

        public float offsetOn = 91.0f;
        public float offsetOff = 93.0f;
        public float currentRot;

        public bool m_startOn = false;

        public eSwitchState m_switchState = eSwitchState.OFF;

        void Start() {
            m_switchJoint = GetComponent<HingeJoint>();
            m_spring = m_switchJoint.spring;
            m_max = m_switchJoint.limits.max;
            m_min = m_switchJoint.limits.min;
            m_switchOn = 0;

            if ( m_startOn ) {
                transform.rotation = new Quaternion(32, transform.rotation.y, transform.rotation.z, 1);
            }
        }

        public void SetSwitchState(string a_state) {

            if(a_state.ToUpper() == "ON") {
                transform.rotation = new Quaternion(32.0f, transform.rotation.y, transform.rotation.z, 1.0f);
            } else if (a_state.ToUpper() == "OFF") {
                transform.rotation = new Quaternion(0.1f, transform.rotation.y, transform.rotation.z, 1.0f);
            }
        }

        void Update() {
            currentRot = Quaternion.Angle(Quaternion.identity, this.transform.rotation);
            switch ( m_switchState ) {
                case eSwitchState.ON: CheckForSwitchOff(); break;
                case eSwitchState.OFF: CheckForSwitchOn(); break;
            }



            UpdateSwitchLimits();
        }

        private void SetSwitch( int a_on ) {
            m_switchOn = a_on;
            m_switchState = (eSwitchState) m_switchOn;

        }

        // always check
        private void CheckForSwitchOff() {
            if ( currentRot < offsetOn ) {
                SetSwitch(1);
                m_OnOFF.Invoke();
            }
        }

        private void CheckForSwitchOff( int a_toChangeTo ) {
            if ( currentRot < offsetOn ) {
                SetSwitch(a_toChangeTo);
                m_OnOFF.Invoke();
            }
        }


        private void CheckForSwitchOn() {
            if ( currentRot > offsetOff ) {
                SetSwitch(0);
                m_OnON.Invoke();
            }

        }

        private void UpdateSwitchLimits() {
            if ( m_switchState == eSwitchState.OFF ) m_spring.targetPosition = m_min;
            else m_spring.targetPosition = m_max - 1;

            m_switchJoint.spring = m_spring;
        }
    }
}
