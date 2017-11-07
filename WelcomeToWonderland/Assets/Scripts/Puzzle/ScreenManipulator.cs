using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class ScreenManipulator : MonoBehaviour {

   public UnityEvent m_on;
   public UnityEvent m_off;
   public UnityEvent m_start;

    public Canvas m_AngryScreen;
    public Canvas m_timerScreen;

    public void StartAngryFace()
    {
        SetTimerScreen(false);
        SetAngryFaceScreen(true);
    }

   public void SetTimerScreen(bool aBool)
    {
        if (aBool)
        {
            m_timerScreen.enabled = true;
            m_timerScreen.GetComponentInChildren<Text>().color = Color.red;
            m_AngryScreen.GetComponentInChildren<Text>().text = "⋋_⋌";
        }
        if (!aBool) m_timerScreen.enabled = false;

    }
    public void SetAngryFaceScreen(bool aBool)
    {
        if (aBool)
        {
            m_AngryScreen.enabled = true;
            m_AngryScreen.GetComponentInChildren<Text>().color = Color.red;
        }
        if (!aBool) m_AngryScreen.enabled = false;

    }
}

