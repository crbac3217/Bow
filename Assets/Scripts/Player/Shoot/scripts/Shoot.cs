using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shoot", menuName = "Database/CreateShoot", order = 110)]
[System.Serializable]
public class Shoot : ScriptableObject
{
    public bool isDefault;
    public int levelsOfShooting;
    public AudioClip shootClip;
    public GameObject projPrefab;
    public Projectory projectory;
    public bool isSkill;
    public Skill skill;

    public virtual void InvokeShoot(AttackArgs aa)
    {

    }
}
