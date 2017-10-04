using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE_ScreenManager : MonoBehaviour {

    [SerializeField] private List<GameObject> m_tabletScreens;
	
	void Update () {
        switch (SE_TabletScreenState.GetCurState()) {
            case SE_TabletScreenState.eScreenState.LOCK_SCREEN: {
                    SetActiveFromIndex((int)SE_TabletScreenState.eScreenState.LOCK_SCREEN);
                    break;
                }
            case SE_TabletScreenState.eScreenState.HOME_SCREEN: {
                    SetActiveFromIndex((int)SE_TabletScreenState.eScreenState.HOME_SCREEN);
                    break;
                }
            case SE_TabletScreenState.eScreenState.CAMERA_APP:
                {
                    SetActiveFromIndex((int)SE_TabletScreenState.eScreenState.CAMERA_APP);
                    break;
                }
            case SE_TabletScreenState.eScreenState.NOTES_APP:
                {
                    SetActiveFromIndex((int)SE_TabletScreenState.eScreenState.NOTES_APP);
                    break;
                }
        }
	}

    /// <summary>
    /// Sets the visual element of the screen to active in the scene
    /// </summary>
    /// <param name="a_index">The scene index you wish to set active</param>
    private void SetActiveFromIndex(int a_index) {
        DisableInactiveScreens(a_index);
        for (int i = 0; i < m_tabletScreens.Count; i++) {
            if (i == a_index){
                if (!m_tabletScreens[i].activeInHierarchy){
                    m_tabletScreens[i].SetActive(true);
                    break;
                }
            }
        }
    }

    /// <summary>
    /// Disables all screens, except the currently active scene
    /// </summary>
    /// <param name="a_currentActiveIndex">The current active scene index</param>
    private void DisableInactiveScreens(int a_currentActiveIndex) {
        for (int i = 0; i < m_tabletScreens.Count; i++) {
            if (i != a_currentActiveIndex) {
                m_tabletScreens[i].SetActive(false);
            }
        }
    }
}
