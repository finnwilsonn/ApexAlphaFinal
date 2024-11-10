using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SettingsMenu : MonoBehaviour {

    // public variables
    public TMPro.TMP_Dropdown resolutionDropdown;
    public AudioMixer audioMixer;

    Resolution[] resolutions; // array of screen resolutions

    private void Start()
    {
        resolutions = Screen.resolutions; // find all the screen resolutions

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>(); // create a list for these resolutions to be displayed

        int currentResolutionIndex = 0; // track current screen resolution
        for (int i = 0; i < resolutions.Length; i++)
        {
            // create resolution options as a string
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            // this is to check if the added option is the current screen resolution
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
             
        resolutionDropdown.AddOptions(options); // add resolution options to dropdown list
        resolutionDropdown.value = currentResolutionIndex; // set dropdown to the current resolution
        resolutionDropdown.RefreshShownValue(); // refresh the list
    }

    // change the screen resolution based on what the player selected from the dropdown list
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    // activate full screen mode
    public void SetFullScreen (bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    // change the game volume
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }

}
