using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WW.Puzzles {
    /// <summary>
    /// Desc: DEPRECATED
    /// Author: Joel Gabriel
    /// </summary>
    public class StageLightController : MonoBehaviour {
        
        [Header("Light Puzzle Reference")]
        [SerializeField] LightPuzzle m_lightPuzzle;
        [SerializeField] LightSequence m_lightSequence;

        [Header("Stage Light Button References")]
        [SerializeField] StageLightButton m_farLeft;
        [SerializeField] StageLightButton m_left;
        [SerializeField] StageLightButton m_right;
        [SerializeField] StageLightButton m_farRight;

        [Header("Timer Settings")]
        [SerializeField] private float m_maxNoInteractionTime;
        [SerializeField] private float m_stayRedTimer;

        private List<int> m_combinationList;
        private float m_noIntTimer = 0.0f;
        private float m_redTimer = 0.0f;

        private bool m_poweredOn = false;
        [Obsolete("The Method is obsolete, now utilising stage light script")]
        public bool PoweredOn {
            get { return m_poweredOn; }
            set { m_poweredOn = value; }
        }

        private bool m_hasFourInputs = false;
        private bool m_correctCombination = false;

        void Start() {
            m_combinationList = new List<int>(4);
        }
        
        void Update() {
            if (!PoweredOn) return;

            m_noIntTimer += Time.deltaTime;
            if(m_noIntTimer >= m_maxNoInteractionTime) {
                // RESET
            }

            if (m_hasFourInputs) {
                if (!m_correctCombination) {
                    // clear list
                    // flash red for visual indication it was incorrect
                    // reset timer for sequence flashing
                    m_redTimer += Time.deltaTime;
                    if(m_redTimer >= m_stayRedTimer) {
                        m_hasFourInputs = false;
                        m_redTimer = 0.0f;
                    }
                } else {
                    // clear list
                    // remove component
                    // open stage curtains
                }
            }
        }

        [Obsolete("The Method is obsolete, now utilising stage light script")]
        private bool Completed() {            
            return false;
        }

        [Obsolete("The Method is obsolete, now utilising stage light script")]
        public void AddToCombinationList(int a_index) {

            // SAFEGUARD ---------------------------------------
            if (m_combinationList.Count == 4) return;
            for(int i = 0; i < m_combinationList.Count; i++) {
                if(m_combinationList[i] == a_index) {
                    return;
                }
            }
            //--------------------------------------------------

            m_combinationList.Add(a_index);

            if (m_combinationList.Count > 3) {
                m_hasFourInputs = true;
                m_combinationList.Clear();
            }

            m_noIntTimer = 0.0f;
        }
    }
}