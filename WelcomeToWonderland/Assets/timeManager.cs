using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace WW.Managers
{

    public class timeManager : MonoBehaviour
    {
        public static timeManager Instance;
        bool PlayerWin = false;
        public float minutes = 1, seconds = 0;
        public Text display;
        public List<Text> LargeScreens = new List<Text>();
        public ParticleSystem ConfettiParticle;
        public ParticleSystem ExplosionParticle;

        [HideInInspector] SwitchScene s;

        void Start()
        {
            Instance = this;
            s = GetComponent<SwitchScene>();
        }

        // Update is called once per frame
        void Update()
        {



            if ( seconds <= 0 && minutes <= 0 && !PlayerWin ) {
                ExplosionParticle.Play();
                s.LoadScene(5, 0);
            }
            else if ( seconds <= 0 && minutes <= 0 && PlayerWin ) {
                ConfettiParticle.Play();
                s.LoadScene(18, 0);
            }
            else seconds -= Time.deltaTime;
            if (seconds < 0 && minutes > 0 )
            {
                seconds = 60;
                if (minutes > 0) minutes -= 1;

            }

            if (minutes == 0 && (int)seconds == 15)
            {
                AudioManager.Instance.PlayVoiceLine(36);
            }
            display.text = (minutes + ":" + (int)seconds);

            if (minutes == 0 && (int)seconds <= 15)
            {
                foreach (var t in LargeScreens)
                {
                    t.text = display.text;
                }
            }
        }

        /// <summary>
        /// Sets minutes
        /// </summary>
        /// <param name="a">Minutes</param>
        public void setMinutes(int a)
        {
            minutes = a;

        }

        /// <summary>
        /// Set Current Seconds
        /// </summary>
        /// <param name="a">Seconds</param>
        public void SetSeoncds(float a)
        {
            seconds = a;
        }


        /// <summary>
        /// Set playerWin State
        /// </summary>
        /// <param name="aBool">state</param>
        public void SetPlayerWin(bool aBool)
        {
            PlayerWin = aBool;
        }
    }
}