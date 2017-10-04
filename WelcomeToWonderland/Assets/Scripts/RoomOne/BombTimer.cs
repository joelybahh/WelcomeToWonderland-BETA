using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class BombTimer : MonoBehaviour {
    List<UnityEvent> m_timersUIOn = new List<UnityEvent>();
    List<UnityEvent> m_timersUIOff = new List<UnityEvent>();

    [SerializeField] List<GameObject> UIElements = new List<GameObject>();
    [SerializeField] List<Text> UIElementsTimers = new List<Text>();

    [Header("Final Countdown")]
    [SerializeField] UnityEvent m_explosions;
    [Header("Debugging Tools")]
    [SerializeField] float m_timer = 10.0f;
    [SerializeField] bool m_triggered;

    private static bool hasBeenSetOnce = false;
    private bool firstSet = true;

    private void Start()
    {
        foreach(GameObject g in UIElements)
        {
            m_timersUIOn.Add(g.GetComponent<ScreenManipulator>().m_on);
            m_timersUIOff.Add(g.GetComponent<ScreenManipulator>().m_off);
            UIElementsTimers.Add(g.GetComponentInChildren<Text>());
        }
    }

    private void Update()
    {
        if (hasBeenSetOnce) return;

        if (m_triggered)
        {
            m_timer -= Time.deltaTime;
     

            foreach (Text t in UIElementsTimers)
            {

                t.text = m_timer.ToString();
            }
            if(m_timer < 0.0f)
            {
                m_explosions.Invoke();
            }
        }
    }

    public void SetTriggered(bool aBool)
    {
        m_triggered = aBool;
        if (aBool)
        {
            foreach (UnityEvent e in m_timersUIOn)
            {
                e.Invoke();
            }
        }
        else if (!aBool)
        {
            foreach (UnityEvent e in m_timersUIOff)
            {
                e.Invoke();
            }
            if (!firstSet) {
                hasBeenSetOnce = true;
            }
            firstSet = false;
        }
    }

    public void MAKEEVERYTHINGEXPLODEINHELLFIRE() { }
}
