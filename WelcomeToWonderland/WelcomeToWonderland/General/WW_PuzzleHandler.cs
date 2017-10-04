using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WW_PuzzleHandler : MonoBehaviour {
    [SerializeField] List<WW_Puzzle> m_puzzleTriggers;
    
    [SerializeField] UnityEvent m_Outcome;
    private void Start( ) {
        foreach ( WW_Puzzle puzzle in m_puzzleTriggers ) {
            puzzle.m_handler = this;
        }
        }
    public void CheckCompleted( ) {
        foreach ( WW_Puzzle puzzle in m_puzzleTriggers ) {
            if ( puzzle.m_completed == false ) {
                return;
            }

            else {
                continue;
            }
        }
            CompletePuzzle();

    }
     void CheckCompleted(int a_Override) {
        CompletePuzzle();
    }

    void CompletePuzzle( ) {

    }
}
