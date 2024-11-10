using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // public variables
    public GameObject descText;

    void Start()
    {
        descText.SetActive(false); // when game starts hide text
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        descText.SetActive(true); // if mouse pointer is over the button show text
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        descText.SetActive(false); // when the mouse pointer leaves the button hide text
    }
}