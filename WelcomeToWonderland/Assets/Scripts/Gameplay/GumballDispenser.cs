using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WW.Managers;

namespace WW.Physics {
    /// <summary>
    /// Desc: This class handles the dispensing of gumballs
    /// Author: Joel Gabriel
    /// </summary>
    public class GumballDispenser : MonoBehaviour {

        [Header("Rotation Settings")]
        [SerializeField] private eRotAxis m_rotationAxis;

        [Tooltip("The value the rotation has to be less than, in order to open the dispenser flap")]
        [SerializeField] private float m_dispenseRotationThreshold = 40.0f;

        [Header("Dispenser Flap Reference")]
        [SerializeField] private GameObject m_flap;

        public eTwistState m_twistState = eTwistState.CLOSED;

        private void Update() {
            SetTwistState();
            UpdateTwistState();
        }

        /// <summary>
        /// Sets the twist state based on the IsOpened() return value
        /// </summary>
        private void SetTwistState() {
            if (IsOpened()) {
                m_twistState = eTwistState.OPENED;
            } else {
                m_twistState = eTwistState.CLOSED;
            }
        }

        /// <summary>
        /// Updates the twisted state, disables flap gameobject according to state
        /// </summary>
        private void UpdateTwistState() {
            switch (m_twistState) {
                case eTwistState.CLOSED: m_flap.SetActive(true);  break;
                case eTwistState.OPENED: m_flap.SetActive(false); AudioManager.Instance.PlayVoiceLine(34); break;
            }
        }

        /// <summary>
        /// Checks if the dispenser should be opened or closed
        /// </summary>
        /// <returns>The current opened state(true = open)</returns>
        private bool IsOpened() {
            switch (m_rotationAxis) {
                case eRotAxis.X:
                    if (transform.rotation.eulerAngles.x <= m_dispenseRotationThreshold) { return true; } 
                    else { return false; }
                case eRotAxis.Y:
                    if (transform.rotation.eulerAngles.y <= m_dispenseRotationThreshold) return true;
                    else return false;
                case eRotAxis.Z:
                    if (transform.rotation.eulerAngles.z <= m_dispenseRotationThreshold) return true;
                    else return false;
                default: return false;
            }
        }
    }

    public enum eRotAxis {
        X,
        Y,
        Z
    }
    public enum eTwistState {
        OPENED,
        CLOSED
    }
}