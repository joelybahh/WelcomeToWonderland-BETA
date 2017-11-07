using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace WW.Puzzles {
    public class PuzzleTracker : MonoBehaviour {

        [Header("Timed Settings")]
        [Range(1, 300)][SerializeField] private float m_totalPuzzleIntTime = 120.0f;
        [Range(1, 300)][SerializeField] private float m_totalNormalIntTime =  30.0f;
        [Range(1, 300)][SerializeField] private float m_totalNormalIntTimeLong =  60.0f;

        [Space()]

        [Header("Puzzle Interaction Events")]
        [SerializeField] private UnityEvent m_OnNoPuzzleInteraction;
        [SerializeField] private UnityEvent m_OnNoNormalInteraction;
        [SerializeField] private UnityEvent m_OnNoNormalLongInteraction;

        private float m_lastPuzzleInteraction = 0.0f;
        private float m_lastNormalInteraction = 0.0f;
        private float m_lastNormalInteractionLong = 0.0f;

        private Puzzle m_curPuzzle = null;

        public Puzzle CurrentPuzzle { 
            private get { return m_curPuzzle; }
            set { m_curPuzzle = value; }
        }

        private bool m_startTimer = false;
        public bool StartTimer
        {
            get { return m_startTimer; }
            set { m_startTimer = value; }
        }

        void Update() {
            if (StartTimer) {
                m_lastPuzzleInteraction += Time.deltaTime;
                m_lastNormalInteraction += Time.deltaTime;
                m_lastNormalInteractionLong += Time.deltaTime;

                if (m_lastNormalInteraction >= m_totalNormalIntTime)
                {
                    m_OnNoNormalInteraction.Invoke();
                }

                if (m_lastPuzzleInteraction >= m_totalPuzzleIntTime)
                {
                    m_OnNoPuzzleInteraction.Invoke();
                }

                if(m_lastNormalInteractionLong >= m_totalNormalIntTimeLong)
                {
                    m_OnNoNormalLongInteraction.Invoke();
                }
            }
        }

        /// <summary>
        /// Resets the timer for puzzle interaction
        /// </summary>
        public void LogPuzzleInteraction(Puzzle a_puzzleInteractedWith) {
            if (!a_puzzleInteractedWith.m_completed) {
                m_lastPuzzleInteraction = 0.0f;
            }
            m_lastNormalInteraction = 0.0f;
            m_lastNormalInteractionLong = 0.0f;
        }

        /// <summary>
        /// Resets the timer for normal interaction
        /// </summary>
        public void LogNormalInteraction() {
            m_lastNormalInteraction = 0.0f;
            m_lastNormalInteractionLong = 0.0f;
        }
    }
}
