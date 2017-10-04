using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NewtonVR;

public class SE_WinGame : MonoBehaviour {
    // Enable life support systems
    // Take them to the win screen

    [SerializeField] private Text m_lifeSupportT;
    [SerializeField] private SE_OxygenTimer m_o2Timer;
    [SerializeField] private Text m_countdownMinT;
    [SerializeField] private Text m_countdownSecT;

    [SerializeField] private Transform m_playerTrans;
    [SerializeField] private Transform m_playerWinTrans;

    private NVRLetterSpinner m_spinner;
    private Rigidbody m_rb;
    private bool isComplete;
    private bool doOnce;

    void Start() {
        m_rb = GetComponent<Rigidbody>();
        m_spinner = GetComponent<NVRLetterSpinner>();
    }

    void Update() {
        if(m_spinner.CurrentAngle >= 170) {
            isComplete = true;            
        }

        if(isComplete) {
            if(!doOnce) {
                m_spinner.CurrentAngle = 0;
                m_playerTrans.position = m_playerWinTrans.position;
                m_o2Timer.hasWon = true;
                m_rb.isKinematic = true;
                m_lifeSupportT.text = "<color=#00FF56FF>LIFE SUPPORT SYSTEMS:</color><color=#00FF56FF>ONLINE</color>";
                m_countdownMinT.text = "<color=#00FF56FF>" + m_countdownMinT.text + "</color>";
                m_countdownSecT.text = "<color=#00FF56FF>" + m_countdownSecT.text + "</color>";
                doOnce = true;
            }
        }      
    }
}
