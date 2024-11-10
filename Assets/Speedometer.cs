using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;  // Adding this as using TextMeshPro

public class Speedometer : MonoBehaviour
{
    public PlayercontrolerShail RR; // reference to player controller script
    public TextMeshProUGUI speedText;

    [Header("Vehicle Speed Settings")]
    public float vehicleSpeed; //  set in Inspector
    public float maxSpeed = 60f; // set maximum speed in KPH

    // Start is called before the first frame update

    private void FixedUpdate()
    {
        // Input validity check using TryParse for vehicle speed
        vehicleSpeed = RR.KPH;
        string speedStr = vehicleSpeed.ToString();

        if (float.TryParse(speedStr, out _))
        {
            // Update digital speedo
            if (speedText != null)
            {
                speedText.text = Mathf.RoundToInt(vehicleSpeed).ToString() + " km/h";
            }
        }
    }
}
