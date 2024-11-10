using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DesertRaceFinish : MonoBehaviour
{
    public DesertLeaderboard DesertLeaderboard;
    public GameObject Player;
    public GameObject RaceFinished;
    public TextMeshProUGUI FinalTimeText;
    public GameObject HUD;
    public Timer raceTimer;
    public static bool GameIsFinished = false;

    void Start()
    {
        RaceFinished.SetActive(false);
        GameIsFinished = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)
        {
            RaceFinished.SetActive(true);
            GameIsFinished = true;
            Time.timeScale = 0f;
            

            // Hide the HUD
            if (HUD != null)
            {
                HUD.SetActive(false);
            }

            // Get the final time and display it on the finish screen
            float finalTime = raceTimer.GetFinalTime();
            FinalTimeText.text = $"{finalTime:F2}";

            // Pass the final time to the leaderboard script
            DesertLeaderboard.SetFinalTime(finalTime);

            // Disable player controls or any other interaction (instead of pausing the entire game)
            Player.GetComponent<PlayercontrolerShail>().enabled = false;
           
        }
    }
}
