using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WW.Managers;

public class InitialAudio : MonoBehaviour {

    AudioSource m_source;
    bool m_playedFirstInitial = false;

    public GameObject m_tv;

	void Start () {
        m_source = GetComponent<AudioSource>();
        AudioManager.Instance.PlayVoiceLine(0);
	}

    void Update() {
        if(m_source.isPlaying) {
            m_playedFirstInitial = false;
        } else {
            m_playedFirstInitial = true;
        }

        if(m_playedFirstInitial) {
            AudioManager.Instance.PlayVoiceLine(1);
            m_tv.SetActive(true);
        }
    }
}
