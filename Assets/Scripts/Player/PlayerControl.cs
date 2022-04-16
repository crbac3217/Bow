using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerControl : MonoBehaviour
{
    public PlayerType playerType;
    public List<Equipment> equipments;
    public List<Item> items;
    public List<Set> sets;
    public List<DamageType> damageTypes;
    public Stat[] stats = new Stat[] { };
    public List<Modifier> onAddList = new List<Modifier>();
    public Skill[] skills = new Skill[] { };
    public EquipmentDataBase beginnerEqDb;
    public int currentHp, gold, levelsOfShooting;
    public CameraParent campar;
    public Transform bottompoint;
    public PlayerAnim pa;
    public PlayerShoot ps;
    public PlayerMove pm;
    public PlayerJump pj;
    public PlayerFreeze pf;
    public PlayerSkillManager psm;
    public PlayerHit ph;
    public GUIManager guiManager;
    public ItemManager itemManager;
    public LevelManager levelManager;
    public DamageManager damageManager;
    public GameObject HealParticle, panel, eachPanel;

    private void Start()
    {
        pa = GetComponent<PlayerAnim>();
        ps = GetComponent<PlayerShoot>();
        pm = GetComponent<PlayerMove>();
        pj = GetComponent<PlayerJump>();
        pf = GetComponent<PlayerFreeze>();
        ph = GetComponent<PlayerHit>();
        psm = GetComponent<PlayerSkillManager>();
        skills = new Skill[4] {null, null, null, null};
        InitializeStats();
        SetPlayer();
    }
        
    public void SetPlayer()
    {
        guiManager.SetPlayer();
        itemManager.SetPlayer();
        psm.Begin();
        pm.Begin();
        pj.Begin();
        ps.Begin();
        ph.Begin();
        pf.Begin();
        pa.Begin();
        ReloadStats();
    }
    public void ReloadStats()
    {
        pm.ResetStat();
        pj.ResetStat();
    }
    public void InitializeStats()
    {
        stats = new Stat[playerType.defaultStats.Count];
        foreach (Stat stat in playerType.defaultStats)
        {
            Stat temp = new Stat()
            {
                statType = stat.statType,
                value = stat.value
            };
            stats[(int)stat.statType] = temp;
        }
    }

    public void BeginnerEquips()
    {
        if (items.Count < Enum.GetNames(typeof(Equipment.EquipmentType)).Length)
        {
            foreach (Equipment eq in playerType.beginner.equipments)
            {
                equipments.Add(eq);
            }
        }
    }
    public void AddSkill(Equipment eq)
    {
        int eqTypeInt = (int)eq.eType;
        Skill skillinst = Instantiate(eq.skill);
        skills[eqTypeInt] = skillinst;
        guiManager.AddSkill(eqTypeInt, skills[eqTypeInt]);
        skills[eqTypeInt].OnSkillAdd(this);
    }
    public void RemoveSkill(Equipment eq)
    {
        if (eq.skill.skillType != SkillType.Movement)
        {
            int eqTypeInt = (int)eq.eType;
            Skill temp = skills[eqTypeInt];
            skills[eqTypeInt] = null;
            guiManager.RemoveSkill(eqTypeInt, temp);
            temp.OnSkillRemove(this);
        }
    }
    public void ReplaceSkill(string skillName, Skill skillToReplace)
    {
        for (int i = 0; i < skills.Length; i++)
        {
            if (skills[i] != null)
            {
                if (skillName == skills[i].skillName)
                {
                    Debug.Log(skills[i].skillName);
                    Debug.Log(skillToReplace.skillName);
                    guiManager.RemoveSkill(i, skills[i]);
                    skills[i].OnSkillRemove(this);
                    skills[i] = Instantiate(skillToReplace);
                    guiManager.AddSkill(i, skills[i]);
                    skills[i].OnSkillAdd(this);
                }
            }
        }
    }
    public void Heal (int amount)
    {
        currentHp += amount;
        var inst = Instantiate(HealParticle, transform.position, Quaternion.identity);
        inst.transform.SetParent(transform);
        if (currentHp > stats[2].value)
        {
            currentHp = stats[2].value;
        }
        var pan = Instantiate(eachPanel, panel.transform);
        pan.GetComponent<TextMeshProUGUI>().text = "+" + amount;
        pan.GetComponent<TextMeshProUGUI>().color = playerType.utilColors[0];
    }
    public void GainGold(int amount)
    {
        gold += amount;
        var pan = Instantiate(eachPanel, panel.transform);
        pan.GetComponent<TextMeshProUGUI>().text = "+" + amount;
        pan.GetComponent<TextMeshProUGUI>().color = playerType.utilColors[1];
    }
    public void UpdateHP(int amount)
    {
        var pan = Instantiate(eachPanel, panel.transform);
        pan.GetComponent<TextMeshProUGUI>().text = amount + "!";
        pan.GetComponent<TextMeshProUGUI>().color = playerType.utilColors[2];
    }
}