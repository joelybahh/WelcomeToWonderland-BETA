using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WW.Particles {
    public class RopeParticleSystem : MonoBehaviour {

        #region Private Variables

        private List<RopeParticle> m_particles = new List<RopeParticle>();
        private LineRenderer m_lineRenderer       = null;
        private float m_timeRemainder             = 0f;
        private float m_substepTime               = 0.02f;

        #endregion

        #region Serialized Variables

        [SerializeField]
        private Transform m_prefabParticle;
        [SerializeField]
        private Transform m_cableStart;
        [SerializeField]
        private Transform m_cableEnd;
        [SerializeField]
        private int m_cableLength       = 10;
        [SerializeField]
        private int m_numSegments       = 10;
        [SerializeField]
        private int m_solverIterations  = 1;

        #endregion

        #region Unity Methods
        void Start() {
            int NumParticles = m_numSegments + 1;
            m_particles.Clear();

            // Use linerenderer as visual cable representation
            m_lineRenderer = transform.GetComponent<LineRenderer>();
            if ( m_lineRenderer == null ) {
                m_lineRenderer = gameObject.AddComponent<LineRenderer>();
            }
            m_lineRenderer.positionCount = m_numSegments;
            m_lineRenderer.startWidth = .02f;
            m_lineRenderer.endWidth = .02f;
            m_lineRenderer.startColor = Color.cyan;
            m_lineRenderer.endColor = Color.blue;

            Vector3 Delta = m_cableEnd.position - m_cableStart.position;

            for ( int ParticleIndex = 0; ParticleIndex < NumParticles; ParticleIndex++ ) {
                Transform newTransform = Instantiate(m_prefabParticle, Vector3.zero, Quaternion.identity) as Transform;

                float Alpha = (float)ParticleIndex / (float)NumParticles;
                Vector3 InitializePosition = m_cableStart.transform.position + (Alpha * Delta);

                RopeParticle particle = newTransform.GetComponent<RopeParticle>();
                particle.position = InitializePosition;
                particle.m_oldPos = InitializePosition;
                particle.transform.parent = this.transform;
                particle.name = "Particle_0" + ParticleIndex.ToString();
                m_particles.Add(particle);

                if ( ParticleIndex == 0 || ParticleIndex == ( NumParticles - 1 ) ) {
                    particle.m_isFixed = false;
                } else {
                    particle.m_isFixed = true;
                }
            }
        }

        void Update() {
            // Update start+end positions first
            RopeParticle StartParticle = m_particles[0];
            StartParticle.position = StartParticle.m_oldPos = m_cableStart.position;

            RopeParticle EndParticle = m_particles[m_numSegments];
            EndParticle.position = EndParticle.m_oldPos = m_cableEnd.position;

            Vector3 Gravity = UnityEngine.Physics.gravity;
            float UseSubstep = Mathf.Max(m_substepTime, 0.005f);

            m_timeRemainder += Time.deltaTime;
            while ( m_timeRemainder > UseSubstep ) {
                PreformSubstep(UseSubstep, Gravity);
                m_timeRemainder -= UseSubstep;
            }
        }
        #endregion

        #region private Methods
        private void PreformSubstep( float a_inSubstepTime, Vector3 a_gravity ) {
            VerletIntegrate(a_inSubstepTime, a_gravity);
            SolveConstraints();
        }

        private void VerletIntegrate( float a_inSubstepTime, Vector3 a_gravity ) {
            int NumParticles = m_numSegments + 1;
            float SubstepTimeSqr = a_inSubstepTime * a_inSubstepTime;

            for ( int ParticleIndex = 0; ParticleIndex < NumParticles; ParticleIndex++ ) {
                RopeParticle particle = m_particles[ParticleIndex];
                if ( particle.m_isFixed ) {
                    Vector3 Velocity = particle.position - particle.m_oldPos;
                    Vector3 NewPosition = particle.position + Velocity + (SubstepTimeSqr * a_gravity);

                    particle.m_oldPos = particle.position;
                    particle.position = NewPosition;
                }
            }
        }

        private void SolveConstraints() {
            float SegmentLength = m_cableLength / (float)m_numSegments;

            // For each iteration
            for ( int IterationIndex = 0; IterationIndex < m_solverIterations; IterationIndex++ ) {
                // For each segment
                for ( int SegmentIndex = 0; SegmentIndex < m_numSegments; SegmentIndex++ ) {
                    RopeParticle ParticleA = m_particles[SegmentIndex];
                    RopeParticle ParticleB = m_particles[SegmentIndex + 1];
                    // Solve for this pair of particles
                    SolveDistanceConstraint(ParticleA, ParticleB, SegmentLength);

                    // Update render position
                    m_lineRenderer.SetPosition(SegmentIndex, ParticleA.position);
                }
            }
        }

        void SolveDistanceConstraint( RopeParticle a_particleA, RopeParticle a_particleB, float a_desiredDistance ) {
            // Find current difference between particles
            Vector3 Delta = a_particleB.position - a_particleA.position;
            float CurrentDistance = Delta.magnitude;
            float ErrorFactor = (CurrentDistance - a_desiredDistance) / CurrentDistance;

            // Only move free particles to satisfy constraints
            if ( a_particleA.m_isFixed && a_particleB.m_isFixed ) {
                a_particleA.position += ErrorFactor * 0.5f * Delta;
                a_particleB.position -= ErrorFactor * 0.5f * Delta;
            } else if ( a_particleA.m_isFixed ) {
                a_particleA.position += ErrorFactor * Delta;
            } else if ( a_particleB.m_isFixed ) {
                a_particleB.position -= ErrorFactor * Delta;
            }
        }
        #endregion
    }
}