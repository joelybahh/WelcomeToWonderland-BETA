using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace WW.Puzzles {
    public class Puzzle : MonoBehaviour {

        public bool m_completed;
        [HideInInspector]
        public PuzzleHandler m_handler;
        [Header("Puzzle Events")]
        public UnityEvent m_onInitialize;

        public void Initialise() {
            m_onInitialize.Invoke();
        }

        public virtual bool CheckPuzzle() {
            return true;
        }
        public virtual void CompletePuzzle() {
            m_completed = true;
            m_handler.CheckCompleted();
        }


    }
}