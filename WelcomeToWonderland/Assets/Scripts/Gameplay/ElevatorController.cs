using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// TODO: FIX! This is getting re-done
/// </summary>
public class ElevatorController : MonoBehaviour {

    [SerializeField] private List<LevelInfo> m_level;

    private int m_activeLevelIndex;

    private void Start () {
        SetupInitScene ();
    }

    void Update () {
        //Press the space key to start coroutine
        if (Input.GetKey (KeyCode.Space)) {
            //Use a coroutine to load the Scene in the background
            StartCoroutine (
                LoadYourAsyncScene (
                    NextScene()));
        }
    }

    IEnumerator LoadYourAsyncScene (int a_sceneIndex) {
        // The Application loads the Scene in the background at the same time as the current Scene.
        // This is particularly good for creating loading screens. You could also load the scene by build //number.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync (a_sceneIndex);

        // Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone) {
            yield return null;
        }
    }

    private void SetupInitScene() {
        for (int i = 0; i < m_level.Count; i++) {
            if (m_level[i].m_isActiveScene) {
                m_activeLevelIndex = i;
                break;
            }

            if (i == m_level.Count - 1) {
                m_activeLevelIndex = 0;
                m_level[0].m_isActiveScene = true;
            }
        }
    }

    private int NextScene() {
        int sceneToLoadIndex = m_activeLevelIndex + 1;
        if (sceneToLoadIndex == m_level.Count - 1)
            return 0;
        else return sceneToLoadIndex;
    }
}

[System.Serializable]
public class LevelInfo {
    public string m_levelName;

    [Header("Level State")]
    public bool m_isActiveScene;
    public bool m_fullyLoaded;

    [Header ("Level Information")]
    public List<GameObject> m_levelItemsToLoad;
    public List<LevelStats> m_storedLevelStats;
}

[System.Serializable]
public struct LevelStats {
    public float m_bestTimeToComplete;
    public int m_trophiesEarned;
}


