using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInstantiationScript : MonoBehaviour
{
    public GameObject[] Cars;
    public Transform spawnPoint; // Predefined spawn point for the car

    void Start()
    {
        int index = PlayerPrefs.GetInt("carIndex", 0); // Load saved car index

        if (Cars == null || Cars.Length == 0)
        {
            Debug.LogError("Cars array is empty or not assigned!");
            return;
        }

        if (index < 0 || index >= Cars.Length)
        {
            Debug.LogError("Car index out of range! Resetting to 0.");
            index = 0; // Reset to a safe default index
            PlayerPrefs.SetInt("carIndex", index);
            PlayerPrefs.Save();
        }

        // Instantiate and activate the selected car at the spawn point
        GameObject selectedCar = Instantiate(Cars[index], spawnPoint.position, spawnPoint.rotation);
        selectedCar.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f); // Set car scale
        selectedCar.SetActive(true);
    }
}
