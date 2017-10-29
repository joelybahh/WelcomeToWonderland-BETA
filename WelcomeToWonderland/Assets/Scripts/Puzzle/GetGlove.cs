using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetGlove : MonoBehaviour {
    public GameObject m_glove;
    public GameObject m_gloveButton;
    public AudioSource m_source;

    bool m_gloveActive = false;
    bool doOnce = true;
    // Update is called once per frame
    void Update() {
        if (m_source.isPlaying) {
            m_gloveActive = false;
        } else {
            m_gloveActive = true;
        }

        if(m_gloveActive) {
            m_glove.SetActive(true);
            enabled = false;
        } else {
            m_glove.SetActive(false);
        }
    }

   
}
