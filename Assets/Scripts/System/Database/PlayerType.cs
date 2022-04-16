using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character" , menuName = "Database/Create Charcter", order = 100)]

[System.Serializable]
public class PlayerType:ScriptableObject
{
    public string typeName;
    public ItemDatabase itemdb;
    public SetDataBase setdb;
    public EquipmentDataBase equipdb;
    public EquipmentDataBase beginner;
    public List<Stat> defaultStats;
    public List<DamageType> defaultDamages;
    public List<Color> tierColors;
    public Color[] DamageColors, utilColors;
    public Sprite[] damageSkins,critSkins;
    public Crit[] critList;
}
