using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairLights : MonoBehaviour {

    public List<GameObject> LightHoldersOff;
    public List<GameObject> LightHoldersOn;
    public List<NewtonVR.NVRButton> buttons;

    private Color white;
    private Color green;

	// Use this for initialization
	void Start () {
        white = Color.white;
        green = Color.green;
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < buttons.Count; i++) {
            if (buttons[i].ButtonIsPushed) {
                buttons[i].transform.parent.parent.GetChild (2).gameObject.SetActive (true);
                buttons[i].transform.parent.parent.GetChild (1).gameObject.SetActive (false);            
            } else if(!buttons[i].ButtonIsPushed) {
                buttons[i].transform.parent.parent.GetChild (2).gameObject.SetActive (false);
                buttons[i].transform.parent.parent.GetChild (1).gameObject.SetActive (true);
            }
        }
    }
}
