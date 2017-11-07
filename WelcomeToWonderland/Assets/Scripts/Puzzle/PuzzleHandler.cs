using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace WW.Puzzles {
    public class PuzzleHandler : MonoBehaviour {
        [SerializeField]
        List<Puzzle> m_puzzleTriggers;

        [SerializeField]
        UnityEvent m_Outcome;
        [SerializeField]
        KeyCode key;
        private bool doOnce = true;
        private void Start() {
            foreach ( Puzzle puzzle in m_puzzleTriggers ) {
                puzzle.m_handler = this;
            }
        }
        public void CheckCompleted() {
            foreach ( Puzzle puzzle in m_puzzleTriggers ) {
                if ( puzzle.m_completed == false ) {
                    return;
                } else {
                    continue;
                }
            }
            if (doOnce) {
                CompletePuzzle();
                doOnce = false;
            }

        }

        void Update() {
            if (Input.GetKeyUp(key)) CompletePuzzle();
        }

        void CheckCompleted( int a_Override ) {
            CompletePuzzle();
        }

        void CompletePuzzle() {
            m_Outcome.Invoke();
        }
    }
}