using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NewtonVR.Example;

namespace WW.Puzzles {
    /// <summary>
    /// Author: Joel Gabriel
    /// Description: The coloured light puzzle handles the logic for reading in 
    ///              rgb values from an input, and uses that to determine what and 
    ///              how much hidden text to reveal.
    /// </summary>
    public class ColourLightPuzzle : MonoBehaviour {

        [Header("Desired Color Settings")]
        [SerializeField]
        private NVRExampleRGBResult m_result;

        [Header("Threshold Settings")]
        [Tooltip("The min value of the combined red and green values")]
        [SerializeField]
        private float m_minOffColourThreshold = 0.2f;

        [Header("Hidden Text")]
        [SerializeField]
        private List<Text> m_redLightText;
        [SerializeField]
        private List<Text> m_greenLightText;
        [SerializeField]
        private List<Text> m_blueLightText;

        [Header("Room Light References")]
        [SerializeField]
        private GameObject m_mainRoomLight;

        private bool m_redGood   = false;
        private bool m_greenGood = false;
        private bool m_blueGood  = false;

        private Color m_colourCheck = new Color();
        private Float3 m_rgb     = new Float3(0.0f, 0.0f, 0.0f);     // stores 3 rgb floats representing the difference in colour

        void Update() {
            m_colourCheck = m_result.Result.material.color;

            if ( LightsOn() ) HideHiddenText();
            else CheckForHiddenText();
        }

        /// <summary>
        /// Checks if the room lights are on
        /// </summary>
        /// <returns>A boolean that is true if the lights are on in the room</returns>
        private bool LightsOn() {
            if ( m_mainRoomLight.activeSelf )
                return true;
            else return false;
        }

        /// <summary>
        /// Checks for hidden text in the room
        /// </summary>
        private void CheckForHiddenText() {
            CheckRedAmount();
            CheckGreenAmount();
            CheckBlueAmount();
        }

        /// <summary>
        /// Checks the current red value of the light
        /// </summary>
        private void CheckRedAmount() {
            if ( m_colourCheck.b >= 0.8f ) m_redGood = true;
            else m_redGood = false;
            // If red good,
            // check min max threshhold between the rg values, if they are fairly low, its close to red.

            if ( m_redGood ) {
                if ( m_colourCheck.r + m_colourCheck.g <= m_minOffColourThreshold ) {
                    m_rgb.rDiff = ( m_colourCheck.r + m_colourCheck.g );
                    // Set hidden texts alpha value, based off the differences 
                    for ( int i = 0; i < m_redLightText.Count; i++ ) {
                        m_redLightText[i].color = new Color(1, 1, 1, ( m_minOffColourThreshold ) - m_rgb.rDiff);
                    }
                } else {
                    for ( int i = 0; i < m_redLightText.Count; i++ ) {
                        m_redLightText[i].color = new Color(1, 1, 1, 0);
                    }
                }
            } else {
                for ( int i = 0; i < m_redLightText.Count; i++ ) {
                    m_redLightText[i].color = new Color(1, 1, 1, 0);
                }
            }

            // if the total value of the red and green combined is greater than a min threshold, it is red enough?
            // so check how close the red and green values are to 0, add the two, and compare that to a hacked in float value...
        }

        /// <summary>
        /// Checks the current green value of the light
        /// </summary>
        private void CheckGreenAmount() {
            if ( m_colourCheck.b >= 0.8f ) m_greenGood = true;
            else m_greenGood = false;
            // If green good,
            // check min max threshhold between the rg values, if they are fairly low, its close to green.

            if ( m_greenGood ) {
                if ( m_colourCheck.r + m_colourCheck.g <= m_minOffColourThreshold ) {
                    m_rgb.gDiff = ( m_colourCheck.r + m_colourCheck.g );
                    // Set hidden texts alpha value, based off the differences 
                    for ( int i = 0; i < m_greenLightText.Count; i++ ) {
                        m_greenLightText[i].color = new Color(1, 1, 1, ( m_minOffColourThreshold ) - m_rgb.gDiff);
                    }
                } else {
                    for ( int i = 0; i < m_greenLightText.Count; i++ ) {
                        m_greenLightText[i].color = new Color(1, 1, 1, 0);
                    }
                }
            } else {
                for ( int i = 0; i < m_greenLightText.Count; i++ ) {
                    m_greenLightText[i].color = new Color(1, 1, 1, 0);
                }
            }

            // if the total value of the red and green combined is greater than a min threshold, it is green enough?
            // so check how close the red and green values are to 0, add the two, and compare that to a hacked in float value...
        }

        /// <summary>
        /// Checks the current blue value of the light
        /// </summary>
        private void CheckBlueAmount() {
            if ( m_colourCheck.b >= 0.8f ) m_blueGood = true;
            else m_blueGood = false;
            // If blue good,
            // check min max threshhold between the rg values, if they are fairly low, its close to blue.

            if ( m_blueGood ) {
                if ( m_colourCheck.r + m_colourCheck.g <= m_minOffColourThreshold ) {
                    m_rgb.bDiff = ( m_colourCheck.r + m_colourCheck.g );
                    // Set hidden texts alpha value, based off the differences 
                    for ( int i = 0; i < m_blueLightText.Count; i++ ) {
                        m_blueLightText[i].color = new Color(1, 1, 1, ( m_minOffColourThreshold ) - m_rgb.bDiff);
                    }
                } else {
                    for ( int i = 0; i < m_blueLightText.Count; i++ ) {
                        m_blueLightText[i].color = new Color(1, 1, 1, 0);
                    }
                }
            } else {
                for ( int i = 0; i < m_blueLightText.Count; i++ ) {
                    m_blueLightText[i].color = new Color(1, 1, 1, 0);
                }
            }

            // if the total value of the red and green combined is greater than a min threshold, it is blue enough?
            // so check how close the red and green values are to 0, add the two, and compare that to a hacked in float value...
        }

        /// <summary>
        /// Hides the hidden text (ironic)
        /// </summary>
        private void HideHiddenText() {
            for ( int i = 0; i < m_redLightText.Count; i++ ) {
                m_redLightText[i].color = new Color(1, 1, 1, 0);
            }
            for ( int i = 0; i < m_greenLightText.Count; i++ ) {
                m_greenLightText[i].color = new Color(1, 1, 1, 0);
            }
            for ( int i = 0; i < m_blueLightText.Count; i++ ) {
                m_blueLightText[i].color = new Color(1, 1, 1, 0);
            }
        }
    }

    /// <summary>
    /// Holds three float values
    /// </summary>
    public class Float3 {
        public float rDiff;
        public float gDiff;
        public float bDiff;

        /// <summary>
        /// Float3 Constructor
        /// </summary>
        /// <param name="a_rDiff">Default rDiff Value</param>
        /// <param name="a_gDiff">Default gDiff Value</param>
        /// <param name="a_bDiff">Default bDiff Value</param>
        public Float3( float a_rDiff, float a_gDiff, float a_bDiff ) {
            rDiff = a_rDiff;
            gDiff = a_gDiff;
            bDiff = a_bDiff;
        }
    }
}