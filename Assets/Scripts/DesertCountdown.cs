using System.Collections;
using UnityEngine;
using TMPro;

public class DesertCountdown : MonoBehaviour
{
    public GameObject CountDown;
    public AudioSource countdownAudio; // Full countdown audio
    public GameObject lapTimer;
    public GameObject engineAudioGameObject; // Reference to the GameObject containing the engine audio script
    public PlayercontrolerShail playerController; // Reference to the player controller script

    private void Start()
    {
        StartCoroutine(CountDownRoutine());
    }

    IEnumerator CountDownRoutine()
    {
        countdownAudio.Play(); // Play the entire countdown audio at once

        yield return new WaitForSeconds(0.5f);
        CountDown.GetComponent<TextMeshProUGUI>().text = "3";
        CountDown.SetActive(true);

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

        // Enable the GameObject containing the engine audio script
        engineAudioGameObject.SetActive(true); // Enable the engine audio GameObject
        playerController.enabled = true; // Enable the player controller
        lapTimer.SetActive(true); // Enable the lap timer here
    }
}
