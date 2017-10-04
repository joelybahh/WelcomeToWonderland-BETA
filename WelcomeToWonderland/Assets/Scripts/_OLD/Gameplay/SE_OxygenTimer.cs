using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SE_OxygenTimer : MonoBehaviour {

    [Header("o2 Timer Settings")]
    [SerializeField] private float m_minutesUntilZero;
    
    [Header("UI Settings")]
    [SerializeField] private Text m_minT;
    [SerializeField] private Text m_secT;

    public SE_Clock m_o2Clock;
    public bool hasWon = false;

	void Start() {
        m_o2Clock = new SE_Clock(m_minutesUntilZero);
    }

	// Update is called once per frame
	void Update () {
		if(!m_o2Clock.HasFinished && !hasWon) {
            m_minT.text = "Minutes: " + m_o2Clock.Minutes;
            m_secT.text = "Seconds: " + (int)m_o2Clock.Seconds;

            m_o2Clock.Countdown(Time.deltaTime);
        } else if(hasWon) {
            m_o2Clock.Stop();
        } else {
            m_o2Clock.Stop();
            // wait a couple of seconds while the player dies,
            SceneManager.LoadScene(0);
            // TODO: you lose
        }
    }
}
