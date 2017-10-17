using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NewtonVR;
using UnityEngine.Events;
using WW.CustomPhysics;

public class ClawMachineButton : MonoBehaviour {
    [Header("Claw Machine Lever Control Reference")]
    [SerializeField] private ClawMachineController m_clawMachineRef;

    [Header ("Button Events")]
    [SerializeField] private UnityEvent m_onPressed;

    [Header ("Audio Settings")]
    [SerializeField] private AudioClip m_clip;
    [SerializeField] private AudioSource m_source;

    private NVRButton m_button;

    void Start () {
        m_button = GetComponent<NVRButton> ();
    }

    void Update () {
        if (m_button.ButtonWasPushed) {
            m_onPressed.Invoke ();
            m_source.PlayOneShot (m_clip);
            m_clawMachineRef.CurMachineState = eMachineState.GRABBING;
        }
    }
}
