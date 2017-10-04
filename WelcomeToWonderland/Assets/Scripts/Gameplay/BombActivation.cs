using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WW {
    public class BombActivation : MonoBehaviour {
        public enum eBombState {
            ARMED,
            LOCKED,
            OFF
        }

        [SerializeField] private List<GameObject> m_tvScreenText;
        [SerializeField] private float m_bombTime;

        private bool m_hasTriggeredOnce;
        private eBombState m_bombState;

        private float m_bombTimer = 0.0f;

        void Start() {
            m_hasTriggeredOnce = false;
            m_bombState = eBombState.OFF;
        }

        void Update() {
            switch(m_bombState) {
                case eBombState.OFF:    UpdateOff();    break;
                case eBombState.LOCKED: UpdateLocked(); break;
                case eBombState.ARMED:  UpdateArmed();  break;
            }
        }

        private void UpdateArmed() {
            m_bombTimer += Time.deltaTime;

            if(m_bombTimer >= m_bombTime) {
                //DETONATION
            }
        }

        private void UpdateLocked() {

        }

        private void UpdateOff() {

        }
    }
}