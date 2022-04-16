using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Items", menuName = "Database/Create Items", order = 101)]
[System.Serializable]
public class ItemDatabase : ScriptableObject
{
    public List<Item> items;
}
[System.Serializable]
public class Item
{
    public string nameCode;
    public string itemName;
    public int itemTier;
    public int cost;
    public int multi;
    public string itemDescription;
    public Sprite itemThumbnail;
    public List<Stat> statMods;
    public List<DamageType> damageMods;
    public List<Modifier> modifiers;
}

