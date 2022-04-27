
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelDatabase", menuName = "Database/LevelDatabase", order = 105)]
[System.Serializable]
public class LevelDataBase : ScriptableObject
{
    public List<PerLevels> levels;
}
[System.Serializable]
public class PerLevels
{
    public List<Level> levels;
}
[System.Serializable]
public class Level
{
    public ThemeDatabase theme;
    public EndType endType;
    public GameObject boss;
    public int level;
}