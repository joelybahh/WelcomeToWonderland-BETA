using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class WW_Puzzle : MonoBehaviour {

    public bool m_completed;
    [HideInInspector] public WW_PuzzleHandler m_handler;
    [Header("Puzzle Events")] public UnityEvent m_onInitialize;
    public virtual bool CheckPuzzle( ) {
        return true;
    }
    public virtual void CompletePuzzle( ) {
        m_completed = true;
        m_handler.CheckCompleted();
    } 


}
