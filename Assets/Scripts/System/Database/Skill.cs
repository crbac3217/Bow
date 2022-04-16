using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "Skill/Create Skill", order = 105)]
[System.Serializable]
public class Skill : ScriptableObject
{
    public SkillType skillType;
    public CooldownType cdType;
    public string skillName, description;
    public bool isSkillAvail, disableSkill, isPassive, selected, held = false;
    public SkillButton sb;
    public List<Skill> affectedSkills = new List<Skill>();
    public float activeFor, cooldownTime, lastTime;
    public int cdMaxCount, cdCurrentCount;
    public GameObject enhanceObj;
    public MoveMod moveMod;
    public Shoot aimableSkill;
    public Sprite thumbnail;
    public CoolDownCounter tempCounter;

    #region Cooldown
    public virtual void OnSkillAdd(PlayerControl pc)
    {
        if (cdType == CooldownType.landHit)
        {
            AddDamageCounter(pc);
        }
        if (cdType == CooldownType.kill)
        {
            AddKillCounter(pc);
        }
        if (skillType == SkillType.Movement)
        {
            var inst = Instantiate(moveMod);
            pc.pm.moveMods.Add(inst);
            inst.skill = this;
        }
    }
    public virtual void AddDamageCounter(PlayerControl pc)
    {
        CoolDownCounter cdcount = ScriptableObject.CreateInstance<CoolDownCounter>();
        tempCounter = cdcount;
        cdcount.skill = this;
        pc.damageManager.damagedMods.Add(tempCounter);
    }
    public virtual void AddKillCounter(PlayerControl pc)
    {
        CoolDownCounter cdcount = ScriptableObject.CreateInstance<CoolDownCounter>();
        tempCounter = cdcount;
        cdcount.skill = this;
        pc.damageManager.killedMods.Add(tempCounter);
    }
    public virtual void RemoveDamageCounter(PlayerControl pc)
    {
        pc.damageManager.damagedMods.Remove(tempCounter);
    }
    public virtual void RemoveKillCounter(PlayerControl pc)
    {
        pc.damageManager.killedMods.Remove(tempCounter);
    }
    public virtual void OnSkillRemove(PlayerControl pc)
    {
        if (cdType == CooldownType.landHit)
        {
            RemoveDamageCounter(pc);
        }
        else if (cdType == CooldownType.kill)
        {
            RemoveKillCounter(pc);
        }
        if (skillType == SkillType.Movement)
        {
            for (int i = pc.pm.moveMods.Count - 1; i >= 0 ; i--)
            {
                if (pc.pm.moveMods[i].GetType() == moveMod.GetType())
                {
                    pc.pm.moveMods.Remove(pc.pm.moveMods[i]);
                }
            }
        }
    }
    public virtual IEnumerator StartCooldownTimer()
    {
        lastTime = Time.time;
        yield return new WaitForSeconds(cooldownTime);
        isSkillAvail = true;
    }
    #endregion Cooldown
    #region skillDisable
    public void DisableSkill(Skill skill)
    {
        skill.disableSkill = true;
        affectedSkills.Add(skill);
    }
    public void EnableSkill(Skill skill)
    {
        skill.disableSkill = false;
        affectedSkills.Remove(skill);
    }
    public virtual void EndSkill(PlayerControl pc)
    {
        Debug.Log(skillName + "has finished :D");
        cdCurrentCount = 0;
        isSkillAvail = false;
        pc.psm.InvokeEvent();
        for (int i = affectedSkills.Count - 1; i >= 0 ; i--)
        {
            EnableSkill(affectedSkills[i]);
        }
        affectedSkills.Clear();
        if (cdType == CooldownType.time)
        {
            pc.psm.StartCoroutine(StartCooldownTimer());
        }
        if (sb)
        {
            sb.UpdateCD();
        }
    }
    #endregion skillDisable
    #region ButtonPress
    public virtual void OnButtonPress(PlayerControl pc)
    {
        if (!disableSkill)
        {
            CheckSkillTypePress(pc);
        }
    }
    public virtual void CheckSkillTypePress(PlayerControl pc)
    {
        //check if the skill is a passive skill
        if (skillType == SkillType.Passive)
        {
            Debug.Log("This is a Passive Skill");
            //gui manager pop up This is Passive bruv
        }else
        {
            if (!isSkillAvail)
            {
                Debug.Log(skillName + " is still on Cooldown");
            }
            else
            {
                //execute different functions depending on the type of Skill 
                if (skillType == SkillType.replaceShot)
                {
                    ReplaceShotPress(pc);
                }
                else if (skillType == SkillType.Active)
                {
                    ActivePress(pc);
                }
                else if (skillType == SkillType.shotEnhance)
                {
                    ShotEnhancePress(pc);
                }
                else if (skillType == SkillType.Buff)
                {
                    BuffPress(pc);
                }
            }
        } 
    }

    public virtual void ShotSelectedToggleOn(PlayerControl pc)
    {
        selected = true;
        //gui button to indicate that this is now selected
    }
    public virtual void ShotSelectedToggleOff(PlayerControl pc)
    {
        selected = false;
        //gui button to indicate that this is now de-selected
    }
    public virtual void ReplaceShotPress(PlayerControl pc)
    {
        if (selected)
        {
            ShotSelectedToggleOff(pc);
            pc.ps.activeShoot = Instantiate(pc.ps.defaultShoot);
            for (int i = affectedSkills.Count - 1; i >= 0; i--)
            {
                EnableSkill(affectedSkills[i]);
            }
        }
        else
        {
            ShotSelectedToggleOn(pc);
            Shoot temp = Instantiate(this.aimableSkill);
            pc.ps.activeShoot = temp;
            this.aimableSkill.isSkill = true;
            temp.skill = this;
            foreach (Skill skill in pc.skills)
            {
                if (skill)
                {
                    if (skill.skillType == SkillType.shotEnhance)
                    {
                        DisableSkill(skill);
                    }
                }
            }
        }
    }
    public virtual void ActivePress(PlayerControl pc)
    {
        pc.pf.FreezePosIndef();
        held = true;
        pc.pa.bodyAnim.SetTrigger("skillPressed");
    }
    public virtual void ShotEnhancePress(PlayerControl pc)
    {
        if (pc.ps.activeShoot == null)
        {
            ShotSelectedToggleOn(pc);
            AddEnhanceOnShoot temp = ScriptableObject.CreateInstance<AddEnhanceOnShoot>();
            temp.skill = this;
            pc.ps.shootMods.Add(temp);
            foreach (Skill skill in pc.skills)
            {
                if (skill)
                {
                    if (skill.skillType == SkillType.replaceShot)
                    {
                        DisableSkill(skill);
                    }
                }
            }
        }
    }
    public virtual void BuffPress(PlayerControl pc)
    {

    }
    #endregion ButtonPress
    #region ButtonRelease
    public virtual void OnButtonRelease(PlayerControl pc)
    {
        CheckTypeRelease(pc);
    }
    public void CheckTypeRelease(PlayerControl pc)
    {
        if (skillType == SkillType.Active)
        {
            if (held)
            {
                ActiveRelease(pc);
            }
        }
    }
    public virtual void ActiveRelease(PlayerControl pc)
    {
        held = false;
        isSkillAvail = false;
        pc.pa.bodyAnim.SetTrigger("skillReleased");
        foreach (Skill skill in pc.skills)
        {
            if (skill)
            {
                DisableSkill(skill);
            }
        }
        if (activeFor > 0)
        {
            pc.psm.ActivateCoroutine(CoActivate(activeFor, pc));
        }
        else
        {
            pc.pa.bodyAnim.SetTrigger("skillEnded");
            pc.pf.UnfreezePos();
            EndSkill(pc);
        }
    }
    public virtual IEnumerator CoActivate(float time, PlayerControl pc)
    {
        yield return new WaitForSeconds(time);
        pc.pa.bodyAnim.SetTrigger("skillEnded");
        pc.pf.UnfreezePos();
        EndSkill(pc);
    }
    #endregion ButtonRelease


}

public class CoolDownCounter : Modifier
{
    public Skill skill;
    public override void OnEnemyModActive(EnemyArg da)
    {
        base.OnEnemyModActive(da);
        skill.cdCurrentCount++;
        if (skill.cdCurrentCount >= skill.cdMaxCount)
        {
            skill.isSkillAvail = true;
            skill.cdCurrentCount = skill.cdMaxCount;
        }
        skill.sb.UpdateCD();
    }
}
public class AddEnhanceOnShoot : Modifier
{
    public Skill skill;

    public override AttackArgs AttackArgMod(AttackArgs aa)
    {
        GameObject enhancer = Instantiate(skill.enhanceObj, aa.projectile.transform.position, Quaternion.identity);
        enhancer.transform.SetParent(aa.projectile.transform);
        aa.projectile.GetComponent<HwarangDefaultProjectile>().attachments.Add(enhancer.GetComponent<EnhanceObj>());
        skill.EndSkill(aa.apc);
        skill.ShotSelectedToggleOff(aa.apc);
        aa.apc.ps.shootMods.Remove(this);
        return aa;
    }
}
public enum SkillType { replaceShot = 0, shotEnhance = 1, Active = 2, Movement = 3, Passive = 4, Buff = 5}
public enum CooldownType { landHit = 0, kill = 1, time = 2, Passive = 3 }