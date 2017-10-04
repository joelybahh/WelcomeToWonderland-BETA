using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE_Clock {

    private float m_minutes;
    private float m_seconds;
    private bool m_hasFinished;
	
    public float Minutes {
        get { return m_minutes; }
    }

    public float Seconds {
        get { return m_seconds; }
    }

    public bool HasFinished {
        get { return m_hasFinished; }
    }

    public SE_Clock(float a_minutes) {
        m_minutes = a_minutes;
        m_seconds = 0.0f;
        m_hasFinished = false;
    }
	
	public void Countdown (float a_deltaTime) {
        if ( m_hasFinished ) return;

        m_seconds -= a_deltaTime;

        if (m_seconds <= 0) {
            if ( m_minutes > 0 ) {
                m_minutes--;
                m_seconds = 59.0f;
            } else {
                m_hasFinished = true;
            }
        }
	}

    public void Stop() {
        m_hasFinished = true;
    }
}
