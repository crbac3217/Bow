using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipmentButtonPrefab : MonoBehaviour
{
    public bool isAvail;
    public Image image, bg, skillImage;
    public TextMeshProUGUI eName, description, skillName;

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
}
