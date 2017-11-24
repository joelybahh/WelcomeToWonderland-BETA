using NewtonVR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace WW.Helpers {
    /// <summary>
    /// Auth: Joel Gabriel  
    /// Desc: This class is used as an extension to NVRButton, it simple wraps up some of its bools 
    ///       into unity events, is an optional component to add to the button.
    /// </summary>
    [RequireComponent (typeof (NVRButton))]
    public class ButtonEvents : MonoBehaviour {

        private NVRButton m_buttonRef;

        private bool down;
        private bool up;

        [SerializeField] private UnityEvent m_whileButtonDown;
        [SerializeField] private UnityEvent m_whileButtonUp;

        [SerializeField] private UnityEvent m_onButtonDown;
        [SerializeField] private UnityEvent m_onButtonUp;

        private bool oneShotClip = false;
        public bool OneShotClip {
            set { OneShotClip = value; }
        }

        AudioSource source;

        private void Start () {
            m_buttonRef = GetComponent<NVRButton> ();
            source = GetComponent<AudioSource> ();
            down = false;
            up = false;
        }

        private void Update () {
            if (m_buttonRef.ButtonDown) {
                m_whileButtonDown.Invoke ();
                down = true;
            }
            if (m_buttonRef.ButtonUp) {
                m_whileButtonUp.Invoke ();
                oneShotClip = false;
                up = true;
            }
        }

        private void LateUpdate () {
            if (m_buttonRef.ButtonUp && down) {
                m_onButtonUp.Invoke ();
                down = false;
            }

            if (m_buttonRef.ButtonDown && up) {
                m_onButtonDown.Invoke ();
                oneShotClip = false;
                up = false;
            }
        }

        public void PlayOneShotClip(AudioClip clip) {
            if(!oneShotClip) {
                source.PlayOneShot (clip);
                oneShotClip = true;
            }
        }
    }
}