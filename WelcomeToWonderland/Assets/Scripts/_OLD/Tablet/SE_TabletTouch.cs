using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SE_TabletTouch : MonoBehaviour {

    [SerializeField] private UnityEngine.UI.Text m_inputField;

    [SerializeField] private string m_passcode = "1234";

    public NewtonVR.NVRHand             m_activeHand;
    private NewtonVR.NVRInteractableItem m_intItem;

    void Start() {
        m_intItem = GetComponent<NewtonVR.NVRInteractableItem>();
    }

    void Update() {
        if (m_intItem != null)
        {
            if (m_intItem.AttachedHands.Count > 0)
                m_activeHand = m_intItem.AttachedHands[0];
            else m_activeHand = null;
        }
    }

    void OnTriggerEnter(Collider col) {
        switch (SE_TabletScreenState.GetCurState()) {
            case SE_TabletScreenState.eScreenState.LOCK_SCREEN: {
                    if(col.tag == "TabletKeypad") {
                        if (m_activeHand != null) m_activeHand.GetInputDevice.TriggerHapticPulse(2999);
                        col.GetComponent<Image>().color = Color.green;               
                        SE_LockScreen lockScreen = col.transform.parent.parent.GetComponent<SE_LockScreen>();
                        lockScreen.UpdateLockScreen(col, "TabletKeypad");
                    }
                    break;
            }
            case SE_TabletScreenState.eScreenState.HOME_SCREEN: {
                    if (col.tag == "CameraApp" || col.tag == "NotesApp") {
                        SE_HomeScreen homeScreen = col.transform.parent.GetComponent<SE_HomeScreen>();
                        homeScreen.UpdateHomeScreen(col, "NotesApp", "CameraApp");
                    }
                    break;
                }
            case SE_TabletScreenState.eScreenState.CAMERA_APP: {
                    if (col.tag == "SwapCamera") {
                        SE_CameraScreen cameraScreen = col.transform.parent.GetComponent<SE_CameraScreen>();
                        cameraScreen.SwapCamera();
                    } else if (col.tag == "HomeScreen") {
                        SE_TabletScreenState.SwapToState(SE_TabletScreenState.eScreenState.HOME_SCREEN);
                    } else if (col.tag == "TakePhoto") {
                        SE_CameraScreen cameraScreen = col.transform.parent.GetComponent<SE_CameraScreen>();
                        if(cameraScreen.PhotosTaken < 5) StartCoroutine(cameraScreen.TakePhoto());
                    }
                    break;
                }
            case SE_TabletScreenState.eScreenState.NOTES_APP: {
                    if (col.tag == "HomeScreen") {
                        SE_TabletScreenState.SwapToState(SE_TabletScreenState.eScreenState.HOME_SCREEN);
                    }
                    break;
                }
                
        }
        
    } 

    void OnTriggerExit(Collider col) {
        switch (SE_TabletScreenState.GetCurState()) {
            case SE_TabletScreenState.eScreenState.LOCK_SCREEN: {
                    if (col.tag == "TabletKeypad") {
                        col.GetComponent<Image>().color = Color.white;
                    } break;
                }
        }
    }

    public void SetupInputField()
    {
       // m_inputField = GameObject.Find("Input").GetComponent<UnityEngine.UI.Text>();
    }
}
