using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityAudio : MonoBehaviour
{
    private float minPitch = 0.9f;
    private float maxPitch = 1.5f;
    private float maxSpeed = 10f;
    private AudioSource audioSource;
    private Rigidbody target;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        target = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (target == null)
            return;
        UpdateAudioPitch();
    }
    private void UpdateAudioPitch()
    {
        var clampedSpeed = Mathf.Clamp(target.velocity.magnitude, 0, maxSpeed);
        var pitchPercentage = clampedSpeed / maxSpeed;
        var pitch = Mathf.Lerp(minPitch, maxPitch, pitchPercentage);

        audioSource.pitch = pitch;
    }
    public void stopEngineSound()
    {
        audioSource.Pause();
    }
}
