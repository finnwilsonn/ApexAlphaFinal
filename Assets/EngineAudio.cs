using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EngineAudio : MonoBehaviour
{
    AudioSource audioSource;
    public float minimumPitch = 0.5f; // minimum pitch of the engine sound
    public float maximumPitch = 2f;   // maximum pitch of the engine sound
    public float engineSpeed = 1f;    // will be updated by PlayerControllerShail script

    // Start is called before the first frame update
    void Start()
    {
        //  AudioSource component attached to object
        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = minimumPitch;

        // Making sure the engine audio plays continuously
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the game is paused 
        if (Time.timeScale == 0)
        {
            // Pause the engine sound if the game paused
            if (audioSource.isPlaying)
            {
                audioSource.Pause();
            }
        }
        else
        {
            // Resume the engine sound if the game not paused
            if (!audioSource.isPlaying)
            {
                audioSource.UnPause();
            }

            // Update pitch of engine sound based on engineSpeed
            audioSource.pitch = Mathf.Clamp(engineSpeed, minimumPitch, maximumPitch);
        }
    }

    // Way to stop the engine audio
    public void StopEngineAudio()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
