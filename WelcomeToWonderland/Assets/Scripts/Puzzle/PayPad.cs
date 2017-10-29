using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WW.Puzzles {
    /// <summary>
    /// Author: Joel Gabriel
    /// Description: Stores reference to the pay pad screen visuals, and its 
    ///              current display state. It provides a public method to be accessed elsewhere
    ///              that handles the swapping of the display text.
    /// </summary>
    public class PayPad : MonoBehaviour {

        #region Serialized Variables

        [Header("Visual Text References")]
        [SerializeField] private GameObject m_accessGrantedRef;
        [SerializeField] private GameObject m_accessDeniedRef;
        [SerializeField] private GameObject m_invalidSwipeRef;
        [SerializeField] private GameObject m_defaultTextRef;

        #endregion

        #region Private Variables

        private eCurText m_curText;

        #endregion

        #region Unity Methods

        private void Start() {
            m_curText = eCurText.DEFAULT;
            UpdateCurText(m_curText);
        }

        #endregion

        #region Public Methods 

        /// <summary>
        /// FIXME: Gross method, quick and easy
        /// This method simply swaps out the active screen visuals depending on the
        /// passed in enum.
        /// </summary>
        /// <param name="a_textToShow">The text you wish to change too</param>
        public void UpdateCurText(eCurText a_textToShow) {
            switch(a_textToShow) {
                case eCurText.DEFAULT:
                m_defaultTextRef.SetActive(true);
                m_accessGrantedRef.SetActive(false);
                m_accessDeniedRef.SetActive(false);
                m_invalidSwipeRef.SetActive(false);
                break;
                case eCurText.DENIED:
                m_invalidSwipeRef.SetActive(false);
                m_accessGrantedRef.SetActive(false);
                m_accessDeniedRef.SetActive(true);
                m_defaultTextRef.SetActive(false);
                break;
                case eCurText.GRANTED:
                m_invalidSwipeRef.SetActive(false);
                m_accessGrantedRef.SetActive(true);
                m_accessDeniedRef.SetActive(false);
                m_defaultTextRef.SetActive(false);
                break;
                case eCurText.INVALID:
                m_invalidSwipeRef.SetActive(true);
                m_accessGrantedRef.SetActive(false);
                m_accessDeniedRef.SetActive(false);
                m_defaultTextRef.SetActive(false);
                break;
            }
        }

        #endregion
    }

    public enum eCurText {
        GRANTED,
        DENIED,
        INVALID,
        DEFAULT
    }
}