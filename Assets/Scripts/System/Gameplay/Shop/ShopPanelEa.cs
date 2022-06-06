using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopPanelEa : MonoBehaviour
{
    public Item item;
    public Image image, bg, panel;
    public TextMeshProUGUI iName, description, price;

    private void Start()
    {
        panel = GetComponent<Image>();
    }
    public void PointerEnter()
    {
        panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, 1);
    }
    public void PointerExit()
    {
        panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, 0.603f);
    }
}
