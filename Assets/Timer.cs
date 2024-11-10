using UnityEngine;
using TMPro;
using System.Collections;

public class Timer : MonoBehaviour
{
    [Header("Component")]
    public TextMeshProUGUI TimerText;

    [Header("Timer Settings")]
    public float CurrentTime;
    public bool CountDown;

    private const string TimeFormat = "0.00"; // Format for displaying time

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpdateTimer());
    }

    // Coroutine to update timer
    private IEnumerator UpdateTimer()
    {
        while (true)
        {
            // Check if game is paused
            if (Time.timeScale != 0)
            {
                // Update current time based on countdown setting
                CurrentTime = CountDown ? CurrentTime -= Time.deltaTime : CurrentTime += Time.deltaTime;
                TimerText.text = CurrentTime.ToString(TimeFormat);
            }
            yield return null; // Wait for next frame
        }
    }

    // Method to add time to timer
    public void AddTime(float timeToAdd)
    {
        CurrentTime -= timeToAdd; // Decrease the time when counting
        if (CurrentTime < 0)
        {
            CurrentTime = 0; // Prevent time from going negative
        }
    }

    // Method to get the current time
    public float GetFinalTime()
    {
        return CurrentTime;
    }

}

