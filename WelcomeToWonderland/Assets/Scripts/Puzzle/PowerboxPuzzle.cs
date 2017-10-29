using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace WW.Puzzles {
    /// <summary>
    /// Desc: The logic for the power box 3-digit door code 
    /// Author: Joel Gabriel
    /// </summary>
    public class PowerboxPuzzle : MonoBehaviour {

        #region Serialized Variables

        [Header("                       Puzzle Combination Settings")]
        [SerializeField] private int[] m_correctInput = new int[3];
        [Space()][Range(1,5)]
        [SerializeField] private float m_timeToResetCode = 4.0f;

        [Header("                                  Visual Settings")]
        [SerializeField] private Light[] m_indicatorLights;
        [SerializeField] private Text[] m_displayText;

        [Header("                                   Puzzle Events")]
        [SerializeField] private UnityEvent m_onSolved;

        #endregion

        #region Private Variables

        private int[] m_currentInput;
        private float m_timeSinceLastInteraction;
        private ePuzzleState m_currentPuzzleState;

        #endregion

        #region Unity Methods

        private void Start() {
            m_currentInput = new int[3];
        }

        private void Update() {
            m_timeSinceLastInteraction += Time.deltaTime;

            switch (m_currentPuzzleState) {
                case ePuzzleState.LOCKED:   UpdateState(true);  break;
                case ePuzzleState.UNLOCKED: UpdateState(false); break;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Updates Display Text On The Lock Display
        /// </summary>
        private void UpdateDisplayText() {
            m_displayText[0].text = m_currentInput[0].ToString();
            m_displayText[1].text = m_currentInput[1].ToString();
            m_displayText[2].text = m_currentInput[2].ToString();
        }

        /// <summary>
        /// Updates Indicator Lights On The Lock
        /// </summary>
        private void UpdateIndicatorLights() {

        }

        /// <summary>
        /// Updates both the display and indicator lights appropriatly.
        /// </summary>
        /// <param name="a_locked">if this value is false it will remove this script</param>
        private void UpdateState(bool a_locked) {
            UpdateDisplayText();
            UpdateIndicatorLights();
            if (!a_locked) {
                Destroy(this);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns the current completion state of the puzzle
        /// </summary>
        /// <returns>true if solved, false if not</returns>
        public bool Solved() {
            for (int i = 0; i < m_currentInput.Length; i++)
                if (m_currentInput[i] == m_correctInput[i])
                    if (i == m_currentInput.Length) { m_onSolved.Invoke(); return true; }
            return false;
        }

        /// <summary>
        /// Updates the code at a given index in an array
        /// </summary>
        /// <param name="a_codeIndex">The desired index</param>
        /// <param name="a_value">The new value</param>
        public void UpdateCodeInput(int a_codeIndex, int a_value) {
            if (a_codeIndex + 1 >= m_currentInput.Length) return;
            if (a_value > 9 || a_value < 0) return;

            m_currentInput[a_codeIndex] = a_value;
        }

        public void LogInteraction() {
            m_timeSinceLastInteraction = 0.0f;
        }

        #endregion
    }

    public enum ePuzzleState {
        LOCKED,
        UNLOCKED
    }
}