using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScene : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.RightControl) && Input.GetKeyUp(KeyCode.Escape) ) {
            SceneManager.LoadScene(0);
        }
	}

    public void ResetScene() {
        SceneManager.LoadScene(0);
    }
}
