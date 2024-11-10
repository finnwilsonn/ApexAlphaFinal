using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class engineaudio : MonoBehaviour
{
    AudioSource audioSource;
    public float minimumPitch = 0.5f; // The minimum pitch of the engine sound
    public float maximumPitch = 2f;   // The maximum pitch of the engine sound
    public float engineSpeed = 1f;    // This will be updated by the PlayerControllerShail script

    // Start is called before the first frame update
    void Start()
    {
        // Get the AudioSource component attached to the object
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
        // Checking if the game is paused (time scale is 0)
        if (Time.timeScale == 0)
        {
            // Pause the engine sound if the game is paused
            if (audioSource.isPlaying)
            {
                audioSource.Pause();
            }
        }
        else
        {
            // Resume the engine sound if the game is not paused
            if (!audioSource.isPlaying)
            {
                audioSource.UnPause();
            }

            // Update the pitch of the engine sound based on the engineSpeed
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