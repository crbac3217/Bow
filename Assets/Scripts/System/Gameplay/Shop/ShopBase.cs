using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopBase : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public Button close;
    public Outline outline;
    public TextMeshProUGUI goldAmount;
    public GameObject shopList;
}
