using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopPanelEa : MonoBehaviour
{
    public Item item;
    public Image image, bg;
    public TextMeshProUGUI iName, description, price;

    public void PointerEnter()
    {
        GetComponent<Image>().color = new Color(0, 0, 0, 1);
    }
    public void PointerExit()
    {
        GetComponent<Image>().color = new Color(0, 0, 0, 0.603f);
    }
}
