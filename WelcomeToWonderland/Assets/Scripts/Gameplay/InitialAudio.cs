using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WW.Managers;

public class InitialAudio : MonoBehaviour {

    AudioSource m_source;
    bool m_playedFirstInitial = false;

    public static bool HasPlayedFirstVoiceLine = false;
    public GameObject m_tv;
    public AudioClip m_elevatorArrive;
    public Renderer renderer;
    public Material dingMat;

    private bool hasPlayedArriveDing = false;

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
            HasPlayedFirstVoiceLine = true;

            if (!hasPlayedArriveDing) {
                m_source.PlayOneShot (m_elevatorArrive);
                hasPlayedArriveDing = true;
            }
            StartCoroutine (DelayedMaterialChange (2.8f));

            AudioManager.Instance.PlayVoiceLineDelayed(1, 8);
            m_tv.SetActive(true);
        }
    }

    private IEnumerator DelayedMaterialChange(float a_time) {
        yield return new WaitForSeconds (a_time);
        renderer.material = dingMat;

    }
}
