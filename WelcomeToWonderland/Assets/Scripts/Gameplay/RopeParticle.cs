using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WW.Particles {
    public class RopeParticle : MonoBehaviour {
        private Transform m_transform;

        [HideInInspector]
        public Vector3 m_oldPos;
        public bool m_isFixed;

        #region Unity Methods
        void Awake() {
            m_transform = transform;
        }

        void Start() {
            m_oldPos = m_transform.position;
            position = m_transform.position;
        }
        #endregion

        #region public Getters & Setters
        public Vector3 position {
            get { return m_transform.position; }
            set { m_transform.position = value; }
        }
        #endregion
    }
}