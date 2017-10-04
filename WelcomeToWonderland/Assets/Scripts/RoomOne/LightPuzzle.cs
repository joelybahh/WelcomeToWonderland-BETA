using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WW.Managers;

namespace WW.Puzzles {
    public class LightPuzzle : Puzzle {
        [SerializeField] private StageLight[] m_lights;
        public StageLight[] Lights { get { return m_lights; } }
        [SerializeField] private float m_timer;
        Animator m_ani;
        [Tooltip("The Number the Lights have to be assigned from 0-3. EXAMPLE Light1 = 2, light2 = 4, light3 = 1, light4 = 3. the key would be 2,4,1,3")]
        [SerializeField] private int[] m_PuzzleKey = { 2, 3, 1, 4 };
        [SerializeField] private List<int> m_buttonIds = new List<int>();

        [SerializeField] public List<StageLightButton> m_stageLightButtons;

        public int[] PuzzleKey {
            get { return m_PuzzleKey; }
            private set { m_PuzzleKey = value; }
        }

        public static int m_identifier = 4;

        private bool PuzzleCorrect;
        void Start() {
            m_ani = GetComponent<Animator>();
        }
        private void Update() {
            if (m_buttonIds.Count == 4) {
                PuzzleCorrect = true;
                for (int i = 0; i < m_buttonIds.Count; i++) {
                    if (m_buttonIds[i] == m_PuzzleKey[i]) { m_lights[i].Correct(); } else { m_lights[i].Incorrect(); PuzzleCorrect = false; }
                }
                m_buttonIds = new List<int>();
                if (!PuzzleCorrect)
                {
                    StartCoroutine(IncorrectSequence(1.0f));
                    AudioManager.Instance.PlayVoiceLine(8);
                }
            } else if (m_buttonIds.Count > 4) {
                m_buttonIds = new List<int>();

                StartCoroutine(IncorrectSequence(1.0f));
                AudioManager.Instance.PlayVoiceLine(8);

            }
            if ( PuzzleCorrect ) CompletePuzzle();

        }

        /// <summary>
        /// Disables all lights
        /// </summary>
        public void DisableAllLights() {
            for (int i = 0; i < m_lights.Length; i++) {
                m_lights[i].SetLight(false);
            }
        }

        /// <summary>
        /// Enables all lights
        /// </summary>
        public void EnableAllLights() {
            for (int i = 0; i < m_lights.Length; i++) {
                m_lights[i].SetLight(true);
            }
        }

        /// <summary>
        /// Sets the power of all the lights
        /// </summary>
        /// <param name="a_On">On or Off</param>
        public void SetPowerLights( bool a_On ) {
            for ( int i = 0; i < m_lights.Length; i++ ) {
                m_lights[i].GetPowered = a_On;
            }

            for(int i = 0; i < m_stageLightButtons.Count; i++) {
                m_stageLightButtons[i].PoweredOn = a_On;
            }
        }

        public void SetLightTriggered(bool a_triggered) {
            for (int i = 0; i < m_lights.Length; i++) {
                m_lights[i].triggered = a_triggered;
            }
        }

        private IEnumerator IncorrectSequence(float a_time) {

            foreach (StageLightButton slB in m_stageLightButtons) {
                slB.UnPressable = true;
            }

            yield return new WaitForSeconds(a_time);
            Debug.Log("reseting");
            m_identifier = 0;   
            SetLightTriggered(false);

            foreach (StageLightButton slB in m_stageLightButtons) {
                slB.UnPressable = false;
            }

            DisableAllLights();
        }

        public override void CompletePuzzle()
        {
            base.CompletePuzzle();

            foreach (StageLightButton slB in m_stageLightButtons) {
                slB.m_buttonLight.SetColor(Color.green);
            }

            for (int i = 0; i < 2; i++)
            {
                m_lights[i].Light.color = Color.red;
            }
            for (int i = 2; i < 4; i++)
            {
                m_lights[i].Light.color = Color.blue;
            }

            AudioManager.Instance.PlayVoiceLine(9);
            AudioManager.Instance.PlayVoiceLineDelayed(8, 10);
        }

       public void AddToList(StageLight aLight) {
            if (aLight.GetPowered) {
                if (!aLight.triggered) {
                    m_buttonIds.Add(aLight.m_SetId);
                    aLight.triggered = true;
                    if (m_buttonIds[m_buttonIds.Count - 1] == m_PuzzleKey[m_buttonIds.Count - 1]) { aLight.Correct(); } else {
                        aLight.Incorrect();
                    };
                }
            }
        }
       public void RemoveFromList(StageLight aLight) {
            m_buttonIds.Remove(aLight.m_SetId);
        }
    }
}