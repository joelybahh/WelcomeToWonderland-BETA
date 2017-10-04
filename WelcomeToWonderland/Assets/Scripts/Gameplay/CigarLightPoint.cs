using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CigarLightPoint : MonoBehaviour {
    [SerializeField] ParticleSystem particle;


	// Use this for initialization
	void Start () {
       // particle = GetComponent<ParticleSystem>();
        	
	}

    // Update is called once per frame
    private void OnTriggerEnter(Collider other) {
        if ( other.name == "WW_Tazer_Prop_02" || other.name == "CigarPoint") {
            particle.Play();
        }
    }
}
