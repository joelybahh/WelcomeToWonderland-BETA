using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NewtonVR;
using UnityEngine.Events;

namespace WW.Managers {
    /// <summary>
    /// Author: Joel Gabriel
    /// Description: This class is used to handle the logic associated
    ///              with all the buttons inside the elevator.
    /// </summary>
    public class ElevatorButtonHandler : MonoBehaviour {

        // TODO: Set up a state machine on this handler that store if the elevator is functioning/malfunctioning

        [Header("Button References")]
        [SerializeField] private NVRButton m_emergencyButton;
        [SerializeField] private NVRButton m_openDoorsButton;
        [SerializeField] private NVRButton m_closeDoorsButton;

        [Header("Button Events")]
        [SerializeField] private UnityEvent m_onEmergencyButtonDown;
        [SerializeField] private UnityEvent m_onOpenButtonDown;
        [SerializeField] private UnityEvent m_onCloseButtonDown;

        void Update () {
            if (m_emergencyButton.ButtonDown) {
                m_onEmergencyButtonDown.Invoke ();
            }
            if (m_openDoorsButton.ButtonDown) {
                m_onOpenButtonDown.Invoke ();
            }
            if (m_closeDoorsButton.ButtonDown) {
                m_onCloseButtonDown.Invoke ();
            }
        }
    }
}