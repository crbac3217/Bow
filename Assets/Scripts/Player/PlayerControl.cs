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
    public Image healthBar, moveGem, moveCDGem;
    public TextMeshProUGUI goldUI, healthUI;
    public Stat[] stats = new Stat[] { };
    public List<Modifier> onAddList = new List<Modifier>();
    public Skill[] skills = new Skill[] { };
    public EquipmentDataBase beginnerEqDb;
    public int currentHp, gold, levelsOfShooting;
    public CameraParent campar;
    public AudioSource bodyAudio, shootAudio;
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
    public AudioClip gain, healclip;
    public GameObject HealParticle, panel, eachPanel;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        if (!levelManager.gm.instPlayer)
        {
            InitializeStats();
            levelManager.gm.instPlayer = this.gameObject;
            pa = GetComponent<PlayerAnim>();
            ps = GetComponent<PlayerShoot>();
            pm = GetComponent<PlayerMove>();
            pj = GetComponent<PlayerJump>();
            pf = GetComponent<PlayerFreeze>();
            ph = GetComponent<PlayerHit>();
            psm = GetComponent<PlayerSkillManager>();
            SetPlayer();
        }
    }
        
    public void SetPlayer()
    {
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
        HPBarUpdate();
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
        damageTypes = new List<DamageType>(playerType.defaultDamages);
        currentHp = stats[2].value;
        skills = new Skill[4] { null, null, null, null };
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
        bodyAudio.clip = healclip;
        bodyAudio.Play();
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
        HPBarUpdate();
        StartCoroutine(HealEffect());
    }
    public void HPBarUpdate()
    {
        healthBar.fillAmount = (float)currentHp / stats[2].value;
        healthUI.text = currentHp + " / " + stats[2].value;
        CheckInvincible();
    }
    public void GoldUpdate()
    {
        goldUI.text = "$" + gold;
    }
    public void CheckInvincible()
    {
        if (ph.invincible == true)
        {
            healthBar.color = Color.blue;
        }
        else
        {
            healthBar.color = Color.white;
        }
        
    }
    public void GainGold(int amount)
    {
        bodyAudio.clip = gain;
        bodyAudio.Play();
        gold += amount;
        var pan = Instantiate(eachPanel, panel.transform);
        pan.GetComponent<TextMeshProUGUI>().text = "+" + amount;
        pan.GetComponent<TextMeshProUGUI>().color = playerType.utilColors[1];
        GoldUpdate();
    }
    public void UpdateHP(int amount)
    {
        var pan = Instantiate(eachPanel, panel.transform);
        pan.GetComponent<TextMeshProUGUI>().text = amount + "!";
        pan.GetComponent<TextMeshProUGUI>().color = playerType.utilColors[2];
        HPBarUpdate();
    }
    public void DodgeVis()
    {
        var pan = Instantiate(eachPanel, panel.transform);
        pan.GetComponent<TextMeshProUGUI>().text = "Dodged!";
        pan.GetComponent<TextMeshProUGUI>().color = Color.blue;
    }
    private IEnumerator HealEffect()
    {
        Color temp = Color.white;
        if (healthBar.color != Color.green)
        {
            temp = healthBar.color;
        }
        healthBar.color = Color.green;
        yield return new WaitForSeconds(0.5f);
        healthBar.color = temp;
    }
    public void OnSkillPress(int num)
    {
        if (skills[num] != null)
        {
            skills[num].OnButtonPress(this);
        }
    }
    public void OnSkillRelease(int num)
    {
        if (skills[num] != null)
        {
            skills[num].OnButtonRelease(this);
        }
    }
}