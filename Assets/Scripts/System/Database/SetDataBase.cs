using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sets", menuName = "Database/Create Sets", order = 102)]
[System.Serializable]
public class SetDataBase : ScriptableObject
{
    public List<Set> sets = new List<Set>();
}
[System.Serializable]
public class Set
{
    public string setName;
    public int currentCount;
    public int maxCount;
    public List<string> setCodes;
    public List<string> collected;
    public List<SetEffect> setEffects;
}

[System.Serializable]
public class SetEffect
{
    public string effectName;
    public Sprite effectThumb;
    public int setCount;
    public bool currentlyActive = false;
    public DamageElement elem;
    public List<Stat> statMods;
    public List<DamageType> damageMods;
    public List<Modifier> modifiers;
}