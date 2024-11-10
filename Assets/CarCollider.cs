using UnityEngine;
using System.Collections;

public class CarCollider : MonoBehaviour
{
    // public variables 
    public Timer timer; // this is the timer script 
    public float timeAddition = -5f; // time to add on a collision
    public float immunityDuration = 2f; // player immunity after collision
    private bool isImmune = false;
    public AudioSource crashsource; // crash noise


    void Start()
    {
        crashsource = GetComponent<AudioSource>(); // get crash noise from an audio source component
    }

    // when the car collides with an obstacle..
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") && !isImmune) // if the gameobject the car collides with is an obstacle..
        {

            timer.AddTime(timeAddition); // add on 5 seconds to the timer

            StartCoroutine(ChangeTimerColor()); // begin to coroutine to change the timer text to red

            StartCoroutine(ActivateImmunity()); // start the players immunity

            crashsource.Play(); // play crash noise
        }
    }
    private IEnumerator ChangeTimerColor()
    {
        // Change timer text color to red
        timer.TimerText.color = Color.red;

        // wait for half a second
        yield return new WaitForSeconds(0.5f);

        // change colour from red back to white
        timer.TimerText.color = Color.white;
    }

    private IEnumerator ActivateImmunity()
    {
        isImmune = true; // activate immunity
        yield return new WaitForSeconds(immunityDuration); // wait 2 seconds for the immunity to wear off
        isImmune = false; // disable immunity
    }
}
