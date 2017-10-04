using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WW.Interactables;

namespace WW.Managers {
    public class RoomLightHandler : MonoBehaviour {

        [Header("Room Lights")]
        [SerializeField]
        private List<GameObject> m_roomLightObjs;

        [Header("Room Light Switch")]
        [SerializeField]
        private PowerOutletSwitch m_lightSwitch;

        [Space(50)]

        [Header("Puzzle Lights")]
        [SerializeField]
        private List<GameObject> m_puzzleLightObjs;

        [Header("Puzzle Light Switch Settings")]
        [SerializeField]
        private PowerOutletSwitch m_puzzleLightSwitch;

        private bool m_roomLightsOn = true;
        private bool m_puzzleLightsOn = true;

        void Update() {
            switch ( m_lightSwitch.m_switchState ) {
                case PowerOutletSwitch.eSwitchState.OFF: SetLights(PowerOutletSwitch.eSwitchState.OFF, true); break;
                case PowerOutletSwitch.eSwitchState.ON: SetLights(PowerOutletSwitch.eSwitchState.ON, true); break;
            }

            switch ( m_puzzleLightSwitch.m_switchState ) {
                case PowerOutletSwitch.eSwitchState.OFF: SetLights(PowerOutletSwitch.eSwitchState.OFF, false); break;
                case PowerOutletSwitch.eSwitchState.ON: SetLights(PowerOutletSwitch.eSwitchState.ON, false); break;
            }
        }

        /// <summary>
        /// Toggles the main lights in the room.
        /// </summary>
        private void ToggleLights() {
            m_roomLightsOn = !m_roomLightsOn;
            for ( int i = 0; i < m_roomLightObjs.Count; i++ ) {
                if ( m_roomLightsOn ) {
                    m_roomLightObjs[i].SetActive(false);
                } else {
                    m_roomLightObjs[i].SetActive(true);
                }
            }
        }

        private void SetLights( PowerOutletSwitch.eSwitchState a_state, bool a_isMainLights ) {
            if ( a_isMainLights ) {
                for ( int i = 0; i < m_roomLightObjs.Count; i++ ) {
                    switch ( a_state ) {
                        case PowerOutletSwitch.eSwitchState.OFF: m_roomLightObjs[i].SetActive(false); break;
                        case PowerOutletSwitch.eSwitchState.ON: m_roomLightObjs[i].SetActive(true); break;
                    }
                }
            } else {
                for ( int i = 0; i < m_puzzleLightObjs.Count; i++ ) {
                    switch ( a_state ) {
                        case PowerOutletSwitch.eSwitchState.OFF: m_puzzleLightObjs[i].GetComponent<Light>().enabled = false; break;
                        case PowerOutletSwitch.eSwitchState.ON: m_puzzleLightObjs[i].GetComponent<Light>().enabled = true; break;
                    }
                }
            }
        }
    }
}
