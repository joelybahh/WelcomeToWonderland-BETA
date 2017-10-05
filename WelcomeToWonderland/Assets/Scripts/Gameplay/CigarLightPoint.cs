using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CigarLightPoint : MonoBehaviour {
    [SerializeField] ParticleSystem particle;
    public bool isLit = false;

	// Use this for initialization
	void Start () {
        // particle = GetComponent<ParticleSystem>();
        
	}

    // Update is called once per frame
    private void OnTriggerEnter(Collider other) {
        if ( other.tag == "Lighter" || other.name == "CigarPoint") {
            if (other.GetComponent<TaserDisabler>().isOn || other.GetComponent<CigarLightPoint>().isLit)
            {
                isLit = true;
                particle.Play();
            }
        }
    }
}
