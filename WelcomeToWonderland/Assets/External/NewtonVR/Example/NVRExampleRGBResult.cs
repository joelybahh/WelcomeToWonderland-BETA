using UnityEngine;
using System.Collections;

namespace NewtonVR.Example
{
    public class NVRExampleRGBResult : MonoBehaviour
    {
        public NVRSlider SliderRed;
        public NVRSlider SliderGreen;
        public NVRSlider SliderBlue;

        public Renderer Result;

        private Light m_light;

        private void Start() {
            m_light = GetComponent<Light>();
        }

        private void Update()
        {
            Result.material.color = new Color(SliderRed.CurrentValue, SliderGreen.CurrentValue, SliderBlue.CurrentValue);
            m_light.color = new Color(SliderRed.CurrentValue, SliderGreen.CurrentValue, SliderBlue.CurrentValue);
        }
    }
}