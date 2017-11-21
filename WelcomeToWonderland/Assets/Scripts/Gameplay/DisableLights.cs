using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableLights : MonoBehaviour {
    [SerializeField] List<Light> m_lights;
	// Use this for initialization
	public void Disable( ) {
        foreach ( var l in m_lights ) {
            l.enabled = false;
        }
    }
    public void Enable( ) {
        foreach ( var l in m_lights ) {
            l.enabled = true;
        }
    }
}
