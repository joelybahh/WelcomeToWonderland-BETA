using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaserDisabler : MonoBehaviour {

    NewtonVR.NVRInteractableItem item;
    ParticleSystem particle;
    public bool isOn = false;
	// Use this for initialization
	void Start () {
        particle = GetComponentInChildren<ParticleSystem>();
        item = GetComponent<NewtonVR.NVRInteractableItem>();
	}
	
    public void DisableTaser( ) {
        particle.Stop();
        isOn = false;
    }

    public void EnableTaser( ) {
        particle.Play();
        isOn = true;
    }

    public void SetIsOn(bool aBool)
    {
        isOn = aBool;
    }
}
