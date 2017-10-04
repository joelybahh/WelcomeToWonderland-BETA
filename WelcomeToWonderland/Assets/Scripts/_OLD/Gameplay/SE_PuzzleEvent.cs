using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SE_PuzzleEvent : MonoBehaviour {


    [SerializeField] protected bool m_completed = false;
    [SerializeField] UnityEvent m_onCompleteEvent;
    AudioClip Announcement;

    public bool Completed
    {
        get
        {
            return m_completed;
        }
    }

    public virtual void CompleteEvent( ) {
        m_onCompleteEvent.Invoke();
    } 
}
