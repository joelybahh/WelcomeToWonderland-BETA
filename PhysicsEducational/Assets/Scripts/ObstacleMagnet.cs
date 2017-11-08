using System.Collections;   
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMagnet : MonoBehaviour {

    public eMagnetEnd m_end;
    public float m_speed;

    private Rigidbody2D m_rb2Dref;

    void Start () {
        m_rb2Dref = transform.parent.GetComponent<Rigidbody2D> ();
    }

    void OnTriggerEnter2D ( Collider2D col ) {
        if (col.tag == "North") {
            switch (m_end) {
                case eMagnetEnd.NORTH: {
                        // Get direction from this pos, the the target pos
                        // AddForce along that direction

                        Vector2 posA = transform.position;
                        Vector2 posB = col.transform.position;

                        Vector2 dir = (posB - posA).normalized;

                        m_rb2Dref.AddForce (-dir * m_speed);

                        break;
                    }
                case eMagnetEnd.SOUTH: {
                        // ATTACH TO THE OTHER END
                        break;
                    }
            }
        }

        if (col.tag == "South") {
            switch (m_end) {
                case eMagnetEnd.NORTH: {
                        // ATTACH TO THE OTHER END
                        break;
                    }
                case eMagnetEnd.SOUTH: {
                        // Get direction from this pos, the the target pos
                        // AddForce along that direction

                        Vector2 posA = transform.position;
                        Vector2 posB = col.transform.position;

                        Vector2 dir = (posB - posA).normalized;

                        m_rb2Dref.AddForce (-dir * m_speed);

                        break;
                    }

            }
        }
    }
}
public enum eMagnetEnd {
    NORTH,
    SOUTH
}
