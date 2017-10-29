using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WW.Puzzles {
    /// <summary>
    /// Author: Joel Gabriel
    /// Desc:   This class is used to handle the CardSwipe logic/puzzle. 
    ///         It correctly updates the puzzle completion status, and utilises
    ///         the PayPad class to correctly set the display on the screen, based
    ///         off the data from this script.
    /// </summary>
    public class CardSwipe : Puzzle {

        #region Serialized Variables

        [Header("Swipe Time Settings")]
        [SerializeField] private float m_swipeTime;

        [Header("Swipe Machine References")]
        [SerializeField] private PayPad m_payPad;

        [Header("Physics Material Reference")]
        [SerializeField] private PhysicMaterial m_swipePhysMat;

        #endregion

        #region Private Variables

        private BoxCollider m_collider;

        private float m_simpleTimer;

        private bool m_hasEntered;
        private bool m_hasExited;

        #endregion

        #region Unity Methods

        private void Start() {
            m_collider = GetComponent<BoxCollider>();
            m_hasEntered = false;
            m_simpleTimer = 0.0f;
        }

        private void Update() {
            if(m_hasEntered && !m_completed) {
                // TODO: Create a timer that will start going down, if it hits 0, has entered is now false.
                m_simpleTimer += Time.deltaTime;
                if ( m_simpleTimer >= m_swipeTime ) {
                    Reset();
                    m_payPad.UpdateCurText(eCurText.INVALID);
                    StartCoroutine(DisplayErrorText(1.0f));  // DISPLAY TEXT FOR INVALID SWIPE FOR 1sec.
                }
            } 

            if(m_hasEntered && m_hasExited) {
                // TODO: Successful swipe, unlock gate / tp zone, update text on display
                m_collider.material = null;
                m_payPad.UpdateCurText(eCurText.GRANTED);
                CompletePuzzle();                
            }
        }

        private void OnTriggerEnter( Collider col ) {
            if ( col.tag.Equals("CardEntry") ) {
                m_hasEntered = true;
                m_collider.material = m_swipePhysMat;
            }

            // if we go through the exit zone, and we have entered the first zone previously, 
            // has exited is now true.
            if ( col.tag.Equals("CardExit") && m_hasEntered) {
                m_hasExited = true;
                m_collider.material = null;
            }
        }

        #endregion

        #region Private Methods/Coroutines

        private IEnumerator DisplayErrorText(float a_timeToDisplay) {
            yield return new WaitForSeconds(a_timeToDisplay);
            m_payPad.UpdateCurText(eCurText.DEFAULT);
            m_collider.material = null;
        }

        private void Reset() {
            m_simpleTimer = 0.0f;
            m_hasEntered = false;
            m_hasExited = false;
        }

        #endregion
    }
}