using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Highscores
{
    public List<HighScoreEntry> highscoreEntryList; // create a list to store all of my high score entries
}

[System.Serializable]
public class HighScoreEntry
{
    public string name; // player name
    public float score; // player score
}

public class LeaderBoard : MonoBehaviour
{
    // private variables 
    [SerializeField] private Transform container; // a container in my UI that holds all my entries
    [SerializeField] private Transform entry; // the template for one leaderboard entry
    [SerializeField] private GameObject Leaderboardcanvas; // canvas to display leaderboard
    [SerializeField] private TextMeshProUGUI finalTimeText; // text to show players final time

    private float height = 150f; // vertical spacing between entries
    private float offset = 360f; // offset of my leaderboard entries 

    private List<HighScoreEntry> highScoreEntries; // this is a list of high score entries
    private List<Transform> highScoreEntryTransformList; // a trasnform list for my entries

    [SerializeField] private Button submitButton;
    [SerializeField] private TMP_InputField nameInputField;

    private bool hasSubmittedEntry; // this bool keeps track of whether the player has submitted a score or not

    public void SetFinalTime(float finalTime)
    {
        finalTimeText.text = $"{finalTime:F2}"; // sets the final time text and formatts it so it displays like: '3:02'
    }

    public void ResetEntrySubmission()
    {
        hasSubmittedEntry = false; // sets the bool to false, allowing them to enter a score
    }

    /* my submit entry method adds the players name, score and rank onto the leaderboard, ensuring they only enter their name once
       and that there are only 6 entries on the leaderboard at one time */
    public void SubmitEntry()
    {
        if (hasSubmittedEntry) return; // this prevents the player from spamming multiple scores

        string playerName = nameInputField.text; // get player name from the input field on my UI
        float playerScore;

        // try to parse the final time, ensuring that it outputs as a float not an int
        if (float.TryParse(finalTimeText.text, out playerScore))
        {
            // check the players score and see if it's low enough to be on the leaderboard 
            if (highScoreEntries.Count < 6 || playerScore < highScoreEntries[5].score)
            {
                if (highScoreEntries.Count == 6)
                {
                    highScoreEntries.RemoveAt(5); // if there is more than 6 entries remove the lowest one
                }

                // add the new high score entry
                HighScoreEntry newEntry = new HighScoreEntry { name = playerName, score = playerScore };
                highScoreEntries.Add(newEntry);

                SortLeaderboard(); // call the sort leaderboard method
                UpdateLeaderboardDisplay(); // call the UpdateLeaderboardDisplay to refresh UI
                SaveHighscores(); // call SaveHighscores method

                hasSubmittedEntry = true; // change bool to true, so the player can't enter their score again
            }
        }
    }

    // my awake method is called when my leaderboard script is loaded
    private void Awake()
    {
        Leaderboardcanvas.SetActive(false); // set leaderboard canvas to inactive
        entry.gameObject.SetActive(false); // set the leaderboard entries to inactive

        highScoreEntries = new List<HighScoreEntry>(); // set up the high score list
        highScoreEntryTransformList = new List<Transform>(); // set up the list of entry transforms

        hasSubmittedEntry = false; // change the bool back to false so the player can submit their score again

        LoadHighscores(); // call LoadHighscores method
        UpdateLeaderboardDisplay(); // show leaderboard
    }

    // method for loading the highscores each game
    private void LoadHighscores()
    {
        // load saved leaderboard JSON string for ForesthighscoreTable
        string jsonString = PlayerPrefs.GetString("ForesthighscoreTable", "{}");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        // If there are no score to load, set up an empty list
        if (highscores.highscoreEntryList == null)
        {
            highscores.highscoreEntryList = new List<HighScoreEntry>();
        }

        highScoreEntries = highscores.highscoreEntryList; // load the saved high score data in the highScoreEntries variable
    }

    // method for saving the entries on the leaderboard
    private void SaveHighscores()
    {
        // in order to save the leaderboard, convert it into a JSON string and save
        Highscores highscores = new Highscores { highscoreEntryList = highScoreEntries };
        string jsonString = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("ForesthighscoreTable", jsonString);
        PlayerPrefs.Save(); // save the data to my playerprefs
    }

    // method for sorting the leaderboard in the correct order
    private void SortLeaderboard()
    {
        // sort entries by score in ascending order (lowest to highest)
        for (int i = 0; i < highScoreEntries.Count; i++)
        {
            for (int j = i + 1; j < highScoreEntries.Count; j++)
            {
                if (highScoreEntries[j].score < highScoreEntries[i].score)
                {
                    // swap entries when a new score is added, if needed
                    HighScoreEntry tmp = highScoreEntries[i];
                    highScoreEntries[i] = highScoreEntries[j];
                    highScoreEntries[j] = tmp;
                }
            }
        }
    }

    // method for creating new entries using the template
    private void CreateHighScoreEntryTransform(HighScoreEntry highScoreEntry, Transform container, List<Transform> transformList)
    {
        // create a new UI entry using the template
        Transform entryTransform = Instantiate(entry, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        // position entry based on rank so it fits
        entryRectTransform.anchoredPosition = new Vector2(0, (-height * transformList.Count) + offset);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        // determine what the rank is (1st, 2nd, 3rd, 4th and so one)
        string rankEnding = rank switch
        {
            1 => "1st",
            2 => "2nd",
            3 => "3rd",
            _ => rank + "th"
        };

        // set rank, name, and score text fields for each entry
        entryTransform.Find("Rank").GetComponent<TextMeshProUGUI>().text = rankEnding;
        entryTransform.Find("Name").GetComponent<TextMeshProUGUI>().text = highScoreEntry.name;
        entryTransform.Find("Score").GetComponent<TextMeshProUGUI>().text = highScoreEntry.score.ToString();

        transformList.Add(entryTransform); // add the newly created highscoreentrytransform to the list of displayed entries
    }

    // method for refreshing the leaderboard UI
    private void UpdateLeaderboardDisplay()
    {
        if (highScoreEntryTransformList != null)
        {
            // clear the old entries
            foreach (Transform entryTransform in highScoreEntryTransformList)
            {
                Destroy(entryTransform.gameObject);
            }
            highScoreEntryTransformList.Clear();
        }

        // display each of the entries in highscoreentries list
        foreach (HighScoreEntry highScoreEntry in highScoreEntries)
        {
            CreateHighScoreEntryTransform(highScoreEntry, container, highScoreEntryTransformList);
        }
    }
}