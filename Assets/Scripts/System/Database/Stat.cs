using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    //class for stats in form of enum and int 
    public StatType statType;
    public int value;
}
[System.Serializable]
public class DamageType
{
    public DamageElement damageElement;
    public int value;
}
public enum DamageElement { None = 0, Water = 1, Nature = 2, Fire = 3, Wind = 4, Thunder = 5 }
public enum AttachTo { Player = 0, Arrow = 1, MoveButton = 2, JumpButton = 3, ShootButton = 4, None = 5 }
public enum StatType { moveSpeed = 0, jumpStr = 1, hp = 2, critical = 3, accuracy = 4, jumpCount = 5 }


