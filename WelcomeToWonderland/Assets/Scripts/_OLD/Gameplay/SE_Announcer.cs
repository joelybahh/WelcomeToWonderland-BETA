using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(SE_OxygenTimer))]
public class SE_Announcer : MonoBehaviour {

    [SerializeField] private List<SE_Announcement> m_timedAnnouncements = new List<SE_Announcement>();

    private Queue<SE_Announcement> m_curAnnouncementsQ;
    private SE_OxygenTimer m_o2Timer;
    private AudioSource m_audioSource;
    private bool m_isPlayingAnnouncement;

    void Start() {
        m_o2Timer = GetComponent<SE_OxygenTimer>();
        m_audioSource = GetComponent<AudioSource>();
        m_curAnnouncementsQ = new Queue<SE_Announcement>();
    }

    void Update() {

        if (m_audioSource.isPlaying) m_isPlayingAnnouncement = true;
        else m_isPlayingAnnouncement = false;

        // loop through all the timed announcements
        for (int i = 0; i < m_timedAnnouncements.Count; i++) {
            // if both the clock, and the current timed events minutes are the same
            if ((int)m_timedAnnouncements[i].Minutes == (int)m_o2Timer.m_o2Clock.Minutes) {
                // check if the seconds are also the same
                if ((int)m_timedAnnouncements[i].Seconds == (int)m_o2Timer.m_o2Clock.Seconds) {
                    // if so add it to the queue
                    if(!m_curAnnouncementsQ.Contains(m_timedAnnouncements[i]) && !m_timedAnnouncements[i].HasPlayed) AddAnnouncementToQueue(m_timedAnnouncements[i]);
                    m_timedAnnouncements[i].HasPlayed = true;
                    break;
                }
            }
        }

        // if there is an announcement in the queue, play it
        if (m_curAnnouncementsQ.Count > 0 && !m_isPlayingAnnouncement) {
            //Debug.Log("Count" + m_curAnnouncementsQ.Count);
            PlayAnouncement(); 
        }
    }

    private void PlayAnouncement() {
        SE_Announcement toPlay = m_curAnnouncementsQ.Dequeue();
        m_audioSource.PlayOneShot(toPlay.AnnouncementAudio);
    }

    public void AddAnnouncementToQueue(SE_Announcement a_announcement) {
        m_curAnnouncementsQ.Enqueue(a_announcement);
    }

    public void AddAnnouncementToQueue(AudioClip a_announcement) {
        SE_Announcement announcement = new SE_Announcement(a_announcement);
        m_curAnnouncementsQ.Enqueue(announcement);
    }
}
