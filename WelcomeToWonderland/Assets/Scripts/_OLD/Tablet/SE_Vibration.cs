using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE_Vibration : MonoBehaviour
{

    private Vector3 originPosition;
    private Quaternion originRotation;
    public float shake_decay = 0.002f;
    public float shake_intensity = .3f;

    private AudioSource m_audioSource;
    private float temp_shake_intensity = 0;
    public bool isVibrateOn;

    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
        isVibrateOn = true;
    }

    void Update()
    {
        Shake();
        
        if (temp_shake_intensity > 0 && isVibrateOn)
        {
            transform.position = originPosition + Random.insideUnitSphere * temp_shake_intensity;
            transform.rotation = new Quaternion(
                originRotation.x + Random.Range(-temp_shake_intensity, temp_shake_intensity) * .2f,
                originRotation.y + Random.Range(-temp_shake_intensity, temp_shake_intensity) * .2f,
                originRotation.z + Random.Range(-temp_shake_intensity, temp_shake_intensity) * .2f,
                originRotation.w + Random.Range(-temp_shake_intensity, temp_shake_intensity) * .2f);
        }
    }

    void Shake()
    {
        originPosition = transform.position;
        originRotation = transform.rotation;
        temp_shake_intensity = shake_intensity;
    }

    public void StopShakeOnInteract() {
        isVibrateOn = false;
        m_audioSource.Stop();
        m_audioSource.enabled = false;
        this.enabled = false;
    }
}