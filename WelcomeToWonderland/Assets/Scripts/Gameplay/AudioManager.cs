using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WW.Managers {
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour
    {

        public static AudioManager Instance;

        private AudioSource m_audioSource;

        [Header("Voice Lines")]
        [SerializeField]
        private List<VoiceLine> m_voiceLines;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }

            m_audioSource = GetComponent<AudioSource>();
        }

        [ExecuteInEditMode]
        void Start()
        {
            for (int i = 0; i < m_voiceLines.Count; i++)
            {
                m_voiceLines[i].m_index = i;
            }
        }

        /// <summary>
        /// Plays an audio file based on its index in the list of voiceLines
        /// </summary>
        /// <param name="a_voiceLineIndex">The index at which you want to grab the voice line to play</param>
        public void PlayVoiceLine(int a_voiceLineIndex, bool overrideLast = false)
        {
            if (a_voiceLineIndex > m_voiceLines.Count - 1)
            {
                Debug.LogError("Error Detected! -- Attempting to play an audio clip at an index that exceeds the array length! Exiting function to avoid issues!");
                return;
            }

            AudioClip aClip = m_voiceLines[(int)a_voiceLineIndex].m_clipToPlay;
            if (aClip == null)
            {
                Debug.LogError("Error Detected! -- The voice line you are attempting to play at index [" + a_voiceLineIndex + "] does not contain an audio clip!");
                return;
            }

            if (m_voiceLines[(int)a_voiceLineIndex].m_onlyPlaysOnce)
            {
                Debug.Log("this was called");
                if (m_voiceLines[(int)a_voiceLineIndex].m_hasPlayed)
                {
                    return;
                }
                else
                {
                    m_voiceLines[(int)a_voiceLineIndex].m_hasPlayed = true;
                }
            }

            if (overrideLast) m_audioSource.Stop();
            m_audioSource.PlayOneShot(aClip);
        }
        /// <summary>
        /// Plays an audio file based on its index in the list of voiceLines
        /// </summary>
        /// <param name="a_voiceLineIndex">The index at which you want to grab the voice line to play</param>
        public void PlayVoiceLine(int a_voiceLineIndex)
        {
            if (a_voiceLineIndex > m_voiceLines.Count - 1)
            {
                Debug.LogError("Error Detected! -- Attempting to play an audio clip at an index that exceeds the array length! Exiting function to avoid issues!");
                return;
            }

            AudioClip aClip = m_voiceLines[(int)a_voiceLineIndex].m_clipToPlay;
            if (aClip == null)
            {
                Debug.LogError("Error Detected! -- The voice line you are attempting to play at index [" + a_voiceLineIndex + "] does not contain an audio clip!");
                return;
            }

            if (m_voiceLines[(int)a_voiceLineIndex].m_onlyPlaysOnce)
            {
                if (m_voiceLines[(int)a_voiceLineIndex].m_hasPlayed)
                {
                    return;
                }
                else
                {
                    m_voiceLines[(int)a_voiceLineIndex].m_hasPlayed = true;
                }
            }
            if (m_voiceLines[a_voiceLineIndex].isOverridable)
            {
                m_audioSource.Stop();
                m_audioSource.PlayOneShot(aClip);
            }
            else if( !m_audioSource.isPlaying)
            {
                m_audioSource.PlayOneShot(aClip);

            }

        }

        public void PlayVoiceLineDelayed (int a_voiceLineIndex, int delay)
        {
            StartCoroutine(DelayPlay(a_voiceLineIndex, delay));
            
        }

        private IEnumerator DelayPlay(int a_voiceLineIndex, int delay)
        {
            yield return new WaitForSeconds(delay);
            if (a_voiceLineIndex > m_voiceLines.Count - 1)
            {
                Debug.LogError("Error Detected! -- Attempting to play an audio clip at an index that exceeds the array length! Exiting function to avoid issues!");
                yield break; 
            }

            AudioClip aClip = m_voiceLines[(int)a_voiceLineIndex].m_clipToPlay;
            if (aClip == null)
            {
                Debug.LogError("Error Detected! -- The voice line you are attempting to play at index [" + a_voiceLineIndex + "] does not contain an audio clip!");
                yield break;
            }

            if (m_voiceLines[(int)a_voiceLineIndex].m_onlyPlaysOnce)
            {
                if (m_voiceLines[(int)a_voiceLineIndex].m_hasPlayed)
                {
                    yield break;
                }
                else
                {
                    m_voiceLines[(int)a_voiceLineIndex].m_hasPlayed = true;
                }
            }
            if (m_voiceLines[a_voiceLineIndex].isOverridable)
            {
                m_audioSource.Stop();
                m_audioSource.PlayOneShot(aClip);
            }
            else if (!m_audioSource.isPlaying)
            {
                m_audioSource.PlayOneShot(aClip);

            }
        }


        /// <summary>
        /// Simply plays the audio file via the name (LESS EFFICIENT THAN INDEX)
        /// </summary>
        /// <param name="a_nameOfVoiceline">The name of the voiceLine you wish to play</param>
        public void PlayVoiceLine(string a_nameOfVoiceline)
        {
            AudioClip clipToPlay = null;
            for (int i = 0; i < m_voiceLines.Count; i++)
            {
                if (m_voiceLines[i].m_nameOfLine.ToLower() == a_nameOfVoiceline.ToLower())
                {
                    clipToPlay = m_voiceLines[i].m_clipToPlay;
                    break;
                }
            }

            if (clipToPlay == null)
            {
                Debug.LogError("Error Detected! -- The voice line you are attempting to play with the name [" + a_nameOfVoiceline + "] does not contain an audio clip!");
                return;
            }

            m_audioSource.PlayOneShot(clipToPlay);
        }

       public void PlayNoVoicelineSelection()
        {
            int a = UnityEngine.Random.Range(20, 32);
            PlayVoiceLine(a);
        }

    }

  [System.Serializable]
    public class VoiceLine {
        public string m_nameOfLine;
        public AudioClip m_clipToPlay;
        public bool isOverridable;

        public bool m_onlyPlaysOnce;
        public int m_index;

        [HideInInspector] public bool m_hasPlayed;
    }
}