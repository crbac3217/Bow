using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Theme", menuName = "Database/Maps/Theme", order = 101)]
public class ThemeDatabase : ScriptableObject
{
    public MapTheme mapTheme;
    public List<GameObject> startAreas, middleAreas, endAreas = new List<GameObject>();
    public List<Sprite> backGround, midGround = new List<Sprite>();
    public GameObject effect;
    public List<Color> themeColor = new List<Color>();
}