using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaserDisabler : MonoBehaviour {

    NewtonVR.NVRInteractableItem item;
    ParticleSystem particle;
    public bool isOn = false;

    [SerializeField] private Material m_onMat;
    [SerializeField] private Material m_offMat;

    [SerializeField] private Renderer[] m_prongs;

    // Use this for initialization
    void Start () {
        particle = GetComponentInChildren<ParticleSystem>();
        item = GetComponent<NewtonVR.NVRInteractableItem>();

        if(isOn) {
            foreach (Renderer pRend in m_prongs) {
                pRend.material = m_onMat;
            }
        } else {
            foreach (Renderer pRend in m_prongs) {
                pRend.material = m_offMat;
            }
        }
	}
	
    public void DisableTaser( ) {
        particle.Stop();
        
        foreach(Renderer pRend in m_prongs) {
            pRend.material = m_offMat;
        }

        isOn = false;
    }

    public void EnableTaser( ) {
        particle.Play();

        foreach (Renderer pRend in m_prongs) {
            pRend.material = m_onMat;
        }

        isOn = true;
    }

    public void SetIsOn(bool aBool)
    {
        isOn = aBool;
    }
}
