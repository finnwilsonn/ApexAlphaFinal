using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DesertLeaderboard : MonoBehaviour
{
    // variables
    [SerializeField] private Transform container;
    [SerializeField] private Transform entry;
    [SerializeField] private GameObject Leaderboardcanvas;
    [SerializeField] private TextMeshProUGUI finalTimeText;
    private float height = 150f;
    private float offset = 360f;

    // lists
    private List<HighScoreEntry> highScoreEntries;
    private List<Transform> highScoreEntryTransformList;
    // varaibles for the buttons and input fields
    [SerializeField] private Button submitButton;
    [SerializeField] private TMP_InputField nameInputField;

    private bool hasSubmittedEntry; // keeps track of whether an entry was sent

    public void SetFinalTime(float finalTime)
    {
        finalTimeText.text = $"{finalTime:F2}";
    }

    private void Awake()
    {
        Leaderboardcanvas.SetActive(false); // hide the leaderboard at the start
        entry.gameObject.SetActive(false); // hide the entry template at the start

        highScoreEntries = new List<HighScoreEntry>(); // make a list for high scores
        highScoreEntryTransformList = new List<Transform>(); // make a list for entry display

        hasSubmittedEntry = false; // reset the submission tracker

        LoadHighscores(); // load scores that are saved
        UpdateLeaderboardDisplay(); // show the leaderboard
    }

    private void LoadHighscores()
    {
        string jsonString = PlayerPrefs.GetString("DeserthighscoreTable", "{}");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores.highscoreEntryList == null)
        {
            highscores.highscoreEntryList = new List<HighScoreEntry>(); // set up the list if it’s empty
        }

        highScoreEntries = highscores.highscoreEntryList;
    }

    private void SaveHighscores()
    {
        Highscores highscores = new Highscores { highscoreEntryList = highScoreEntries };
        string jsonString = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("DeserthighscoreTable", jsonString);
        PlayerPrefs.Save();
    }

    private void SortLeaderboard()
    {
        highScoreEntries.Sort((x, y) => x.score.CompareTo(y.score)); // sort scores from low to high
    }

    private void CreateHighScoreEntryTransform(HighScoreEntry highScoreEntry, Transform container, List<Transform> transformList)
    {
        Transform entryTransform = Instantiate(entry, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, (-height * transformList.Count) + offset);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankEnding = rank switch
        {
            1 => "1st",
            2 => "2nd",
            3 => "3rd",
            _ => rank + "th"
        };

        entryTransform.Find("Rank").GetComponent<TextMeshProUGUI>().text = rankEnding; // set rank text
        entryTransform.Find("Name").GetComponent<TextMeshProUGUI>().text = highScoreEntry.name; // set name text
        entryTransform.Find("Score").GetComponent<TextMeshProUGUI>().text = highScoreEntry.score.ToString(); // set score text

        transformList.Add(entryTransform); // add entry to the list of entries
    }

    // add a new entry to the list
    public void SubmitEntry()
    {
        if (hasSubmittedEntry)
        {
            return;
        }

        string playerName = nameInputField.text;
        float playerScore;

        if (float.TryParse(finalTimeText.text, out playerScore))
        {
            if (highScoreEntries.Count < 6 || playerScore < highScoreEntries[5].score) // only add if it’s high enough
            {
                if (highScoreEntries.Count == 6)
                {
                    highScoreEntries.RemoveAt(5); // remove the lowest score if there are too many
                }

                HighScoreEntry newEntry = new HighScoreEntry { name = playerName, score = playerScore };
                highScoreEntries.Add(newEntry);

                SortLeaderboard(); // sort the list
                UpdateLeaderboardDisplay(); // update the display
                SaveHighscores(); // save the updated list

                hasSubmittedEntry = true; // mark that an entry has been sent
            }
            else
            {

            }
        }
    }

    // updates the leaderboard
    private void UpdateLeaderboardDisplay()
    {
        if (highScoreEntryTransformList != null) // check if the list is ready
        {
            foreach (Transform entryTransform in highScoreEntryTransformList)
            {
                Destroy(entryTransform.gameObject); // remove the old entries
            }

            highScoreEntryTransformList.Clear();
        }

        foreach (HighScoreEntry highScoreEntry in highScoreEntries)
        {
            CreateHighScoreEntryTransform(highScoreEntry, container, highScoreEntryTransformList); // make a new display entry
        }
    }

    // reset the submission tracker when player pushes a certain button
    public void ResetEntrySubmission()
    {
        hasSubmittedEntry = false;
    }
}

[System.Serializable]
public class DesertHighscores
{
    public List<HighScoreEntry> highscoreEntryList;
}

[System.Serializable]
public class DesertighScoreEntry
{
    public string name;
    public float score;
}
