using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WW.Interactables;

namespace WW.Puzzles {
    /// <summary>
    /// Desc: A class for handling the visual flashing of the stage lights, hinting at the correct sequence to enter in.
    /// Author: Joel Gabriel
    /// </summary>
    public class LightSequence : MonoBehaviour {

        #region Serialized Variables

        [Header("Light References")]
        [SerializeField]
        private List<Light> m_lights;

        [Header("Flicker Switch References")]
        [SerializeField]
        private List<FlickSwitch> m_lightSwitches;


        [Header("Delay Settings")]
        [SerializeField]
        private float m_sequenceDelayTime;
        [SerializeField]
        private float m_eachLightDelay;
        [SerializeField]
        private float m_lightOnTime;
        [SerializeField]
        private float m_interactionDelayTime;

        #endregion

        #region Private Variables

        private LightPuzzle m_lightPuzzle;
        private eSequenceState m_sequenceState;

        private float m_interactionTimer = 0.0f;
        private float m_individualLightDelayTimer = 0.0f;
        private float m_lightOnTimer = 0.0f;

        private bool m_inUse = false;
        private bool m_hasInteracted = false;
        private bool m_poweredOn = false;

        private int m_interactions = 0;
        private int m_currentLightIndex = 0;

        #endregion

        #region Getters/Setters

        public bool PoweredOn {
            get { return m_poweredOn; }
            set {
                m_poweredOn = value;
                m_currentLightIndex = 0;
            }
        }

        public bool HasInteracted {
            get { return m_hasInteracted; }
            set {
                if (PoweredOn) {
                    m_hasInteracted = value;
                }
            }
        }

        #endregion

        #region Unity Methods

        void Start() {
            m_sequenceState = eSequenceState.DELAY;
            m_lightPuzzle = GetComponent<LightPuzzle>();
        }

        void Update() {
            if (m_poweredOn) {
                if (!m_inUse) {
                    switch (m_sequenceState) {
                        case eSequenceState.FLASHING: FlashInSequence(); break;
                        case eSequenceState.DELAY: StartCoroutine(WaitDelayTime(m_sequenceDelayTime)); break;
                        case eSequenceState.RESET: ResetLights(); break;

                    }
                }

                if (HasInteracted) WaitForInteraction(m_interactionDelayTime);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets current sequence state to a desired state
        /// </summary>
        /// <param name="a_state">Desired State</param>
        public void SetState(eSequenceState a_state) {
            m_sequenceState = a_state;
        }

        /// <summary>
        /// Logs an interaction with the switchboard, resets timer to keep the interations true
        /// </summary>
        public void LogInteraction() {
            if (m_poweredOn) {
                m_interactions++;
                m_interactionTimer = 0;
                m_inUse = true;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Flash lights in sequence
        /// </summary>
        private void FlashInSequence() {
            if (m_lightOnTimer <= 0) m_individualLightDelayTimer += Time.deltaTime;

            if (m_currentLightIndex > 3) {
                SetState(eSequenceState.DELAY);
                m_currentLightIndex = 0;
            }

            if (m_individualLightDelayTimer >= m_eachLightDelay) {
                m_lights[m_currentLightIndex].enabled = true;           // Flash Current light On
                m_lightOnTimer += Time.deltaTime;                       // Increase lights on timer
                if (m_lightOnTimer >= m_lightOnTime) {
                    m_lights[m_currentLightIndex].enabled = false;      // Turn Off Light
                    m_currentLightIndex++;                              // Increase Index By One
                    m_lightOnTimer = 0.0f;                              // Reset Timer
                    m_individualLightDelayTimer = 0.0f;                 // Reset Timer
                }
            }
        }

        /// <summary>
        /// Waits the delay time before flashing in sequence again
        /// </summary>
        /// <param name="a_timeToWait">The time to wait</param>
        private IEnumerator WaitDelayTime(float a_timeToWait) {
            yield return new WaitForSeconds(a_timeToWait);
            SetState(eSequenceState.FLASHING);
        }

        /// <summary>
        /// Waits for an interaction, if it occurs it goes back to sequenced flashing
        /// </summary>
        /// <param name="a_timeToWait">The desired time to wait before resetting</param>
        private void WaitForInteraction(float a_timeToWait) {
            m_interactionTimer += Time.deltaTime;
            if (m_interactionTimer >= a_timeToWait) {
                m_currentLightIndex = 0;
                HasInteracted = false;
                m_inUse = false;
                SetState(eSequenceState.FLASHING);

                foreach (StageLightButton slB in m_lightPuzzle.m_stageLightButtons) {
                    slB.m_buttonLight.SetColor(Color.red);
                }
                foreach (FlickSwitch fS in m_lightSwitches) fS.TurnOffFlickSwitch();
                foreach (Light l in m_lights) l.enabled = false;
            }
        }

        void ResetLights() {
            foreach (var light in m_lightPuzzle.Lights) {
                light.SetLight(false);
            }
        }
        #endregion
    }


    #region Enumerators
    /// <summary>
    /// The current state of the flashing sequence
    /// </summary>
    public enum eSequenceState {
        FLASHING,
        DELAY,
        RESET
    }
    #endregion
}