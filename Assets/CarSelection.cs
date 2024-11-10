using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarSelection : MonoBehaviour
{
    public GameObject[] Cars;  // Array of car GameObjects in the scene
    public Button next;
    public Button previous;
    public Button race;
    public int index;

    void Start()
    {
        index = PlayerPrefs.GetInt("carIndex"); // Load the saved car index, default to 0

        for (int i = 0; i < Cars.Length; i++)
        {
            Cars[i].SetActive(false);
            Cars[index].SetActive(true);
        }
        
    }

    void Update()
    {
        if (index >= 1)
        {
            next.interactable = false;
        }
        else
        {
            next.interactable = true;
        }
        if (index <= 0)
        {
            previous.interactable = false;
        }
        else
        {
            previous.interactable= true;
        }

    }

    public void Next()
    {
        index++;

        for (int i = 0; i < Cars.Length; i++)
        {
            Cars[i].SetActive(false);
            Cars[index].SetActive(true);
        }

        PlayerPrefs.SetInt("carIndex", index);
        PlayerPrefs.Save();
    }

    public void Previous()
    {
        index--;

        for (int i = 0; i < Cars.Length; i++)
        {
            Cars[i].SetActive(false);
            Cars[index].SetActive(true);
        }

        PlayerPrefs.SetInt("carIndex", index);
        PlayerPrefs.Save();
    }

    public void Race()
    {
        SceneManager.LoadScene(2); 
    }







    }



  