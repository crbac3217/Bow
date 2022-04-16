using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Map", menuName = "Database/Maps/Mapdb", order = 101)]
public class MapDatabase : ScriptableObject
{
    public ThemeDatabase[] themes = new ThemeDatabase[] { };
}
public enum MapTheme {Deserts = 0, Raining = 1, Monsters = 2}
public enum EndType {Portal = 0, Shop = 1, Boss = 2 }