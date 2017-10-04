using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A class that handles the logic for the spinner puzzle
/// </summary>
public class WW_SpinnerPuzzle : WW_Puzzle {

    [Header("Ring References")]
    [SerializeField] private NewtonVR.NVRLetterSpinner m_outerSpinner;
    [SerializeField] private NewtonVR.NVRLetterSpinner m_middleSpinner;
    [SerializeField] private NewtonVR.NVRLetterSpinner m_centerSpinner;

    [Header("Ring Rotation Settings")]
    [SerializeField] private float m_outerDesiredRot;
    [SerializeField] private float m_middleDesiredRot;
    [SerializeField] private float m_centerDesiredRot;

    [Header("Ring Rotation Error Thresholds")]
    [Tooltip("The error threshold in degrees")]
    [SerializeField] private float m_errorThresh = 10.0f;


    [Header("Indicator Light References")]
    [SerializeField] private Light m_redLight;
    [SerializeField] private Light m_greenLight;
    [SerializeField] private Light m_blueLight;

    [Header("Button Reference")]
    [SerializeField] private NewtonVR.NVRButton m_button;

    private float m_spinnerOuterRot;
    private float m_spinnerMiddleRot;
    private float m_spinnerCenterRot;

    private NewtonVR.NVRLetterSpinner spinner;
    private bool m_poweredOn = true;
    private bool m_hasInitialized = false;

    public bool PowerOn {
        get { return m_poweredOn; }
        set { m_poweredOn = value; }
    }

    private void Update() {       
        if (!m_poweredOn) return; 
        else if (!m_hasInitialized && m_poweredOn) InitializeRotations();

        if (m_poweredOn) {
            m_redLight.enabled = true;
            m_greenLight.enabled = true;
            m_blueLight.enabled = true;
        }

        m_spinnerOuterRot = m_outerSpinner.CurrentAngle;
        m_spinnerMiddleRot = m_middleSpinner.CurrentAngle;
        m_spinnerCenterRot = m_centerSpinner.CurrentAngle;

        if (m_button.ButtonDown) {
            if (CheckPuzzle()) { CompletePuzzle(); Debug.Log("Solved!!!!!!"); }
        }
    }

    /// <summary>
    /// Sets up the rotations values, normalizes them, and sets the rotation.
    /// </summary>
    private void InitializeRotations() {
        m_spinnerOuterRot = m_outerSpinner.CurrentAngle;
        m_spinnerMiddleRot = m_middleSpinner.CurrentAngle;
        m_spinnerCenterRot = m_centerSpinner.CurrentAngle;

        m_onInitialize.Invoke();
        m_hasInitialized = true;
    }

    /// <summary>
    /// Clamps the rotation value between 0 and 360
    /// </summary>
    /// <param name="a_inputRot">A reference to the input rotation</param>
    private void NormalizeRotation(ref float a_inputRot) {
        float holder = a_inputRot;
        holder = (a_inputRot % 360) + (a_inputRot <= 0 ? 360: 0);
        a_inputRot = holder;
    }

    /// <summary>
    /// Checks if the puzzle is complete.
    /// </summary>
    /// <returns>A bool that states whether or not you have completed the puzzle</returns>
    public override bool CheckPuzzle() {
        return (m_spinnerOuterRot  >= (m_outerDesiredRot -  m_errorThresh) && m_spinnerOuterRot  <= (m_outerDesiredRot  + m_errorThresh)) &&
               (m_spinnerMiddleRot >= (m_middleDesiredRot - m_errorThresh) && m_spinnerMiddleRot <= (m_middleDesiredRot + m_errorThresh)) &&
               (m_spinnerCenterRot >= (m_centerDesiredRot - m_errorThresh) && m_spinnerCenterRot <= (m_centerDesiredRot + m_errorThresh)) ? true : false;
    }

    public void SetVictoryColor() {
        m_redLight.color = Color.green;
        m_greenLight.color = Color.green;
        m_blueLight.color = Color.green;
    }
}
