using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeedometerShail : MonoBehaviour
{ 
public PlayercontrolerShail RR; // Reference to player controller script
public TextMeshProUGUI speedText;

private float startposition = 219.28f, endposition = -40f;
private float desiredposition;

[Header("Vehicle Speed Settings")]
public float vehicleSpeed; // To be set in Inspector
public float maxSpeed = 60f; // Set the maximum speed in KPH

// Start is called before the first frame update

private void FixedUpdate()
{
    // Get the vehicle speed from player controller shail
    vehicleSpeed = RR.KPH;

    // Update the needle's position based on the speed
    updateneedle();

    // Update the digital speed readout
    if (speedText != null)
    {
        speedText.text = Mathf.RoundToInt(vehicleSpeed).ToString() + " km/h";
    }
}

public void updateneedle()
{
    // Calculate the position of the needle based on the speed
    desiredposition = startposition - endposition;
    float temp = vehicleSpeed / maxSpeed; // Normalize the speed based on maxSpeed

}
}
