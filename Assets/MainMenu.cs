using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayNormal()
    {
        SceneManager.LoadScene(1);
    }

    public void PlayHard()
    {
        SceneManager.LoadScene(2);
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f; // make time normal
        SceneManager.LoadScene(0); // load main menu scene
    }
    public void QuitGame () 
    {
        Application.Quit(); 
    }
}
