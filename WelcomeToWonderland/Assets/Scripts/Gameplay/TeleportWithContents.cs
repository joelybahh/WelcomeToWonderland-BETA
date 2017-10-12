using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WW.Movement {
    /// <summary>
    /// Author: Joel Gabriel
    /// Desc: You can now teleport with a box full of items. It checks for a rigidbody component, if it has one it parents it, 
    ///       if not it does nothing. This will work on any box/basket/container.
    /// </summary>
    public class TeleportWithContents : MonoBehaviour {
        [SerializeField] private Transform m_parent;
        [SerializeField] private Transform m_startingParent;

        private void OnTriggerEnter( Collider col ) {
            Rigidbody rb = col.GetComponent<Rigidbody> ();
            if(rb != null)
                if(rb.gameObject.layer != 16)
                    col.transform.parent = m_parent;
        }

        private void OnTriggerExit ( Collider col ) {
            Rigidbody rb = col.GetComponent<Rigidbody> ();
            if (rb != null)
                if (rb.gameObject.layer != 16)
                    col.transform.parent = m_startingParent;
        }
    }
}