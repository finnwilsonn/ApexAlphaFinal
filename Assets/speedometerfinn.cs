using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;  // Adding this as using TextMeshPro

public class speedometerfinn : MonoBehaviour
{
    public PlayerControllerFinn RR; // Reference to your player controller script
    public TextMeshProUGUI speedText; 

    // Reference to the digital speed readout (using TextMeshPro)
    // If using the standard UI Text component, use:
    // public Text speedText;

    private float startposition = 219.28f, endposition = -40f;
    private float desiredposition;

    [Header("Vehicle Speed Settings")]
    public float vehiclespeed; // This will be visible in the Inspector
    public float maxSpeed = 100f; // Set the maximum speed in KPH

    // Start is called before the first frame update
    void Start()
    {
        // Optionally initialize things here
    }

    private void FixedUpdate()
    {
        // Get the vehicle speed from the player controller
        vehiclespeed = RR.KPH;

        // Update the needle's position based on the speed
        updateneedle();

        // Update the digital speed readout
        if (speedText != null)
        {
            speedText.text = Mathf.RoundToInt(vehiclespeed).ToString() + " km/h";
        }
    }

    public void updateneedle()
    {
        // Calculate the position of the needle based on the speed
        desiredposition = startposition - endposition;
        float temp = vehiclespeed / maxSpeed; // Normalize the speed based on maxSpeed

    }
}
