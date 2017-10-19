using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace WW.Managers
{

    public class TimeManager : MonoBehaviour
    {
        public static TimeManager Instance;
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
            //if loser
            if ( seconds <= 0 && minutes <= 0 && !PlayerWin ) {
                s.LoadScene(5, 0);
                ExplosionParticle.Play();
            }
            //if winner
            else if ( seconds <= 0 && minutes <= 0 && PlayerWin ) {
                s.LoadScene(18, 0);
                ConfettiParticle.Play();
            }
            else seconds -= Time.deltaTime;
            //countdown a minute
            if (seconds < 0 && minutes > 0 )
            {
                seconds = 60;
                if (minutes > 0) minutes -= 1;

            }
            //call final countdown
            if (minutes == 0 && (int)seconds == 15)
            {
                AudioManager.Instance.PlayVoiceLine(36);
            }
            if(seconds > 10)display.text = (minutes + ":" + (int)seconds);
            else display.text = (minutes + ":0" + (int)seconds);

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
        public void SetMinutes(int a)
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