using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    private static DamageManager _instance;
    public PlayerControl pc;
    public List<Modifier> damagedMods = new List<Modifier>();
    public List<Modifier> killedMods = new List<Modifier>();
    public GameObject damagePanel;
    public float distanceBetween;
    public int killCount;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    public void SetPlayer()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
    }
    public void DamageInflicted (int damage, GameObject obj, bool onHit, bool didCrit)
    {
        if (onHit)
        {
            EnemyArg damagedArgs = new EnemyArg
            {
                hitObj = obj,
                damageAmount = damage,
                epc = pc,
            };
            foreach (Modifier dMod in damagedMods)
            {
                dMod.OnEnemyModActive(damagedArgs);
            }
        }
        obj.GetComponent<EnemyController>().ConfirmDamage(damage);
    }
    public void DisplayDamage(GameObject panel, DamageElement delem, int amount)
    {
        var damageP = Instantiate(damagePanel, panel.transform);
        TextMeshProUGUI inside = damageP.GetComponent<TextMeshProUGUI>();
        inside.text = amount.ToString();
        inside.color = pc.playerType.DamageColors[(int)delem];

    }
    public void OnKill(GameObject obj)
    {
        EnemyArg killedArgs = new EnemyArg
        {
            hitObj = obj,
            epc = pc
        };
        foreach (Modifier kMod in killedMods)
        {
            kMod.OnEnemyModActive(killedArgs);
        }
        killCount++;
    }
}
public class EnemyArg
{
    public GameObject hitObj;
    public float damageAmount;
    public PlayerControl epc;
}