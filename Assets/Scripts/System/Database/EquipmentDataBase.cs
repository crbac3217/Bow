using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Equipments", menuName = "Database/Create Equipments", order = 103)]
[System.Serializable]
public class EquipmentDataBase : ScriptableObject
{
    public List<Equipment> equipments;
}
[System.Serializable]
public class Equipment
{
    public string nameCode;
    public string name;
    public enum EquipmentType { Bow = 0, Quiver = 1, String = 2, Boots = 3 }
    public EquipmentType eType;
    public string description;
    public Sprite thumbnail;
    public Color color;
    public Skill skill;
    public List<Stat> statMods;
    public List<DamageType> damageMods;
    public List<Modifier> modifiers;
}
