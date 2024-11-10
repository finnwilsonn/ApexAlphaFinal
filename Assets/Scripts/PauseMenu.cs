using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // public variables 
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    public void Start()
    {
        GameIsPaused = false;
        pauseMenuUI.SetActive(false); // set pause menu UI to inactive
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // if the 'esc' key is pressed..
        {
            if (GameIsPaused)
            {
                Resume(); // if it's pressed while the game is paused, resume it
            }
            else
            {
                Pause(); // if not pause the game
            }
        }
    }

    public void Resume ()
    {
        pauseMenuUI.SetActive(false); // set UI to inactive
        Time.timeScale = 1f; // make time normal
        GameIsPaused = false; // the game is not paused
    }


    private void Pause ()
    {
        pauseMenuUI.SetActive(true); // set UI to active
        Time.timeScale = 0f; // freeze time (stop timer)
        GameIsPaused = true; // the game is now paused
    }

    // if plyaer pushes the 'back to menu' button
    public void LoadMenu()
    {
        Time.timeScale = 1f; // set time back to normal
        SceneManager.LoadScene(0); // load main menu scene
    }
}
