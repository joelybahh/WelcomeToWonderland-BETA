using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// TODO: NEED TO RESET TO DEFAULT SETTINGS ON CUSTOM LOAD...
/// </summary>
public class ElevatorController : MonoBehaviour {

    [SerializeField] private List<LevelInfo> m_level;

    private int m_activeLevelIndex;

    private void Start () {
        for(int i = 0; i < m_level.Count; i++) {
            if (m_level[i].m_isActiveScene) {
                m_activeLevelIndex = i;
                break;
            }

            if(i == m_level.Count-1) {
                m_activeLevelIndex = 0;
                m_level[0].m_isActiveScene = true;
            }
        }

        LoadLevel ();
        LoadLevelByIndex (1);
    }

    void Update () {
        //Press the space key to start coroutine
        if (Input.GetKey (KeyCode.Space)) {
            //Use a coroutine to load the Scene in the background
            StartCoroutine (LoadYourAsyncScene ());
        }
    }

    IEnumerator LoadYourAsyncScene () {
        // The Application loads the Scene in the background at the same time as the current Scene.
        //This is particularly good for creating loading screens. You could also load the scene by build //number.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync (1);

        //Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone) {
            yield return null;
        }
    }
    /// <summary>
    /// Loads a level defined by the parameter, unloads the previous level
    /// </summary>
    /// <param name="a_levelIndex">new level to load index</param>
    public void LoadLevelByIndex(int a_levelIndex) {
        UnloadLevel ();

        m_activeLevelIndex = a_levelIndex;

        LoadLevel ();
    }

    /// <summary>
    /// Loads in a level based on the current active index
    /// </summary>
    /// <returns>The loop delay</returns>
    private void LoadLevel() {
        for(int i = 0; i < m_level[m_activeLevelIndex].m_levelItemsToLoad.Count; i++) {
            m_level[m_activeLevelIndex].m_levelItemsToLoad[i].SetActive (true);
        }       
    }

    /// <summary>
    /// Unloads in a level based on the current active index
    /// </summary>
    /// <returns>The loop delay</returns>
    private void UnloadLevel () {
        for (int i = 0; i < m_level[m_activeLevelIndex].m_levelItemsToLoad.Count; i++) {
            m_level[m_activeLevelIndex].m_levelItemsToLoad[i].SetActive (false);
        }
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


