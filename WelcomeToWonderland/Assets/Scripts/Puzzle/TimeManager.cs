using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace WW.Managers
{
    /// <summary>
    /// Desc: Manages the game timer for puzzle one, and handles win lose functionality.
    /// Author: Matthew Jones
    /// </summary>
    public class TimeManager : MonoBehaviour
    {
        public static TimeManager Instance;      

        [SerializeField] private float m_minutes = 1, m_seconds = 0;
        [SerializeField] private Text m_display;
        [SerializeField] private List<Text> m_largeScreens = new List<Text>();

        //[SerializeField] private List<ParticleSystem> m_confettiParticle;
        [SerializeField] private List<DoOnceParticle> m_confettiParticles;

        [SerializeField] private ParticleSystem m_explosionParticle;

        private SwitchScene m_sceneSwitcher;
        private bool m_playerWin = false;

        void Start()
        {
            Instance = this;
            m_sceneSwitcher = GetComponent<SwitchScene>();
        }

        // Update is called once per frame
        void Update()
        {
            //if loser
            if ( m_seconds <= 0 && m_minutes <= 0 && !m_playerWin ) {
                m_sceneSwitcher.LoadScene(5, 0);
                m_explosionParticle.Play();
            }
            //if winner
            else if ( m_seconds <= 0 && m_minutes <= 0 && m_playerWin ) {
                m_sceneSwitcher.LoadScene(18, 0);
                foreach (var con in m_confettiParticles)  {
                    if (!con.hasShot) {
                        con.m_particle.Play ();
                        con.hasShot = true;
                    }
                }
                    
            }
            else m_seconds -= Time.deltaTime;
            //countdown a minute
            if (m_seconds < 0 && m_minutes > 0 )
            {
                m_seconds = 60;
                if (m_minutes > 0) m_minutes -= 1;

            }
            //call final countdown
            if (m_minutes == 0 && (int)m_seconds == 15)
            {
                //AudioManager.Instance.PlayVoiceLine(36);
            }
            if(m_seconds > 10)m_display.text = (m_minutes + ":" + (int)m_seconds);
            else m_display.text = (m_minutes + ":0" + (int)m_seconds);

            if (m_minutes == 0 && (int)m_seconds <= 15)
            {
                foreach (var t in m_largeScreens)
                {
                    t.text = m_display.text;
                }
            }
        }

        /// <summary>
        /// Sets minutes
        /// </summary>
        /// <param name="a">Minutes</param>
        public void SetMinutes(int a)
        {
            m_minutes = a;

        }

        /// <summary>
        /// Set Current Seconds
        /// </summary>
        /// <param name="a">Seconds</param>
        public void SetSeoncds(float a)
        {
            m_seconds = a;
        }


        /// <summary>
        /// Set playerWin State
        /// </summary>
        /// <param name="aBool">state</param>
        public void SetPlayerWin(bool aBool)
        {
            m_playerWin = aBool;
        }
    }

    [System.Serializable]
    public class DoOnceParticle {
        public ParticleSystem m_particle;

        [HideInInspector] public bool hasShot;
    }
}