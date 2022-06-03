using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class EquipmentButtonPrefab : MonoBehaviour
{
    public bool isAvail;
    public Image image, bg, skillImage;
    public EventTrigger activeTrigger, skillTrigger;
    public GameObject skillDescriptionObj;
    public TextMeshProUGUI eName, description, skillName, skillDescription;

    public void PointerEnter()
    {
        if (isAvail)
        {
            GetComponent<Image>().color = new Color(0, 0, 0, 1);
        }
    }
    public void PointerExit()
    {
        if (isAvail)
        {
            GetComponent<Image>().color = new Color(0, 0, 0, 0.603f);
        }
    }
    public void DescEnter()
    {
        if (isAvail)
        {
            skillDescriptionObj.SetActive(true);
        }
    }
    public void DescExit()
    {
        if (isAvail)
        {
            skillDescriptionObj.SetActive(false);
        }
    }
}
