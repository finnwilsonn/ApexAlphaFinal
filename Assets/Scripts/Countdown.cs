using System.Collections;
using UnityEngine;
using TMPro;


public class Countdown : MonoBehaviour
{
    // public variables
    public GameObject CountDown;
    public AudioSource countdownAudio; 
    public GameObject lapTimer;
    public GameObject engineAudioGameObject; // reference to the gameobject which has the engine audio script
    public PlayerControllerFinn playerController; // reference to my player controller script

    private void Start()
    {
        StartCoroutine(CountDownRoutine()); // begin a coroutine
    }

    IEnumerator CountDownRoutine()
    {
        countdownAudio.Play(); // play the countdown audio

        yield return new WaitForSeconds(0.5f); // wait half a second 
        CountDown.GetComponent<TextMeshProUGUI>().text = "3"; // get '3' text
        CountDown.SetActive(true); // set '3' text to active

        // repeat three times for '2' , '1' and 'go'
        yield return new WaitForSeconds(1f);
        CountDown.SetActive(false);
        CountDown.GetComponent<TextMeshProUGUI>().text = "2";
        CountDown.SetActive(true);

        yield return new WaitForSeconds(1f);
        CountDown.SetActive(false);
        CountDown.GetComponent<TextMeshProUGUI>().text = "1";
        CountDown.SetActive(true);

        yield return new WaitForSeconds(1f);
        CountDown.SetActive(false);
        CountDown.GetComponent<TextMeshProUGUI>().text = "GO!";
        CountDown.SetActive(true);

        // enable the gameobject which has the engine audio script 
        engineAudioGameObject.SetActive(true); // enable the engine audio gameobject
        playerController.enabled = true; // enable the player controller so tney can drive
        lapTimer.SetActive(true); // start the timer
    }
}
