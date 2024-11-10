using TMPro;
using UnityEngine;

public class RaceFinish : MonoBehaviour
{
    // public variables
    public LeaderBoard LeaderBoard; // reference to leaderboard script
    public GameObject RaceFinished; // race finsihed UI
    public TextMeshProUGUI FinalTimeText; 
    public GameObject HUD; 
    public Timer raceTimer;
    public static bool GameIsFinished = false; 
    private const string PlayerTag = "Player"; // tag of car for collisions

    void Start()
    {
        RaceFinished.SetActive(false); // set UI to not active
        GameIsFinished = false;
    }

    // checks for collisions between the player and the gameobject that has this script
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == PlayerTag) // if the collision includes the player
        {
            RaceFinished.SetActive(true); // set the race finished UI to active
            GameIsFinished = true;
            Time.timeScale = 0f; // stop the timer

            // hide the HUD
            if (HUD != null)
            {
                HUD.SetActive(false);
            }

            // get the final time and show it on the finish screen
            float finalTime = raceTimer.GetFinalTime();
            FinalTimeText.text = $"{finalTime:F2}";

            // pass the final time onto the leaderboard script
            LeaderBoard.SetFinalTime(finalTime);



        }
    }
}