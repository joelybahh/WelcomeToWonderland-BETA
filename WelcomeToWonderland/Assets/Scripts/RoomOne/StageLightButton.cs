using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NewtonVR;
using UnityEngine.Events;

namespace WW.Puzzles {
    /// <summary>
    /// Desc:
    /// Author: Joel Gabriel
    /// </summary>
    public class StageLightButton : MonoBehaviour {

        [Header("Button Light References")]
        public DoubleLight m_buttonLight;

        [Header("Button Events")]
        [SerializeField] private UnityEvent m_onPressed;

        [Header ("TEMP: Audio")]
        [SerializeField] private AudioClip m_clip;
        private AudioSource m_source;

        private NVRButton m_button;

        private bool m_poweredOn;
        public bool PoweredOn {
            get { return m_poweredOn; }
            set { m_poweredOn = value; }
        }

        private bool m_unPressable = false;
        public bool UnPressable {
            private get { return m_unPressable; }
            set { m_unPressable = value; }
        }

        void Start() {
            m_button = GetComponent<NVRButton>();
            m_source = GetComponent<AudioSource>();
        }

        void Update() {
            if (m_button.ButtonWasPushed) {
                m_onPressed.Invoke();

                // If its not in an incorrect state
                if (!UnPressable && PoweredOn) {
                    m_buttonLight.SetColor(Color.green);
                    m_source.PlayOneShot(m_clip);
                    UnPressable = true;
                }
            }   
        }
    }

    [System.Serializable]
    public class DoubleLight {
        [SerializeField] private Light m_buttonLightInner;
        [SerializeField] private Light m_buttonLightOuter;

        public void SetColor(Color a_color) {
            m_buttonLightInner.color = a_color;
            m_buttonLightOuter.color = a_color;
        }
    }
}