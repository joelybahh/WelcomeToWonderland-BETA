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

        [Header("Button References")]
        [SerializeField] private NVRButton m_emergencyButton;
        [SerializeField] private NVRButton m_openDoorsButton;
        [SerializeField] private NVRButton m_closeDoorsButton;

        [Header("Standard Button Events")]
        [SerializeField] private UnityEvent m_onEmergencyButtonDown;
        [SerializeField] private UnityEvent m_onOpenButtonDown;
        [SerializeField] private UnityEvent m_onCloseButtonDown;

        [Header ("Malfunction Button Events")]
        [SerializeField] private UnityEvent m_onEmergMalButtonDown;
        [SerializeField] private UnityEvent m_onOpenMalButtonDown;
        [SerializeField] private UnityEvent m_onCloseMalButtonDown;

        private static bool m_hasMalfunctioned = false;
        private bool isStopped = false;

        private Animator m_animator;

        private void Start () {
            m_animator = GetComponent<Animator> ();
        }

        private void Update () {
            isStopped = InitialAudio.HasPlayedFirstVoiceLine;
            if (isStopped) {
                if (!m_hasMalfunctioned) UpdateMalfunctionAnimStates ();
                else UpdateStandardAnimStates ();
            }
        }

        /// <summary>
        /// Updates the states and unity events for button presses during malfunction phase
        /// </summary>
        private void UpdateMalfunctionAnimStates() {

            if (m_emergencyButton.ButtonDown) {
                m_onEmergMalButtonDown.Invoke ();
                m_hasMalfunctioned = true;
            }
            if (m_openDoorsButton.ButtonDown || m_openDoorsButton.ButtonIsPushed) {
                m_onOpenMalButtonDown.Invoke ();
            }
            if (m_closeDoorsButton.ButtonDown || m_closeDoorsButton.ButtonIsPushed) {
                m_onCloseMalButtonDown.Invoke ();
            }
        }

        /// <summary>
        /// Updates the states and unity events for button presses during the standard phase
        /// </summary>
        private void UpdateStandardAnimStates () {
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