using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaserDisabler : MonoBehaviour {

    NewtonVR.NVRInteractableItem item;
    ParticleSystem particle;
    
	// Use this for initialization
	void Start () {
        particle = GetComponentInChildren<ParticleSystem>();
        item = GetComponent<NewtonVR.NVRInteractableItem>();
	}
	
    public void DisableTaser( ) {
        particle.Stop();

    }

    public void EnableTaser( ) {
        particle.Play();
    }

}
