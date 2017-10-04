using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE_PuzzleEventHandler : MonoBehaviour {
    [SerializeField]
    List<SE_PuzzleEvent> m_puzzles;

    private SE_Announcer m_announcer;
    public SE_Announcer Announcer { get { return m_announcer; } }

    public static SE_PuzzleEventHandler Instance;

    void Awake() {
        if (Instance == null)
            Instance = this;     
        else if (Instance != this)
            Destroy(gameObject);
    }

    public void CompleteEvent(SE_PuzzleEvent a_event ) {
        foreach (SE_PuzzleEvent e in m_puzzles)
        {
           if(e.Completed)
            {
                e.CompleteEvent();
            }
        }
    }
}
