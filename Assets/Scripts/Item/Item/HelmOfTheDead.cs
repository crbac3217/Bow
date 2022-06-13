using System.Collections;
using System;
using UnityEngine;


[CreateAssetMenu(fileName = "HelmOfTheDead", menuName = "Modifier/Items/HelmOfTheDead", order = 110)]
public class HelmOfTheDead : Modifier
{
    public float pushForce, radius;
    public GameObject reviveParticle;

    public override void OnModifierActive(PlayerControl pc)
    {
        pc.psm.ActivateCoroutine(SetPlayerInvincible(pc));
    }

    IEnumerator SetPlayerInvincible(PlayerControl pc)
    {
        float cdtemp = pc.ps.fixedJoystick.defaultCd;
        pc.pa.bodyAnim.SetTrigger("fakeDead");
        pc.ph.SetInvincible();
        pc.ps.fixedJoystick.CancelShooting();
        pc.ps.fixedJoystick.defaultCd = 3;
        pc.ps.fixedJoystick.Avail = false;
        foreach (Skill sk in pc.skills)
        {
            if (sk)
            {
                sk.isSkillAvail = false;
                sk.EndSkill(pc);
            }
        }
        pc.pf.FreezePosIndef();
        int healAmount = Mathf.Max((int)MathF.Min(1, ((float)pc.stats[2].value / 16f)), 1);
        yield return new WaitForSeconds(1);
        pc.Heal(healAmount);
        yield return new WaitForSeconds(1);
        pc.Heal(healAmount);
        yield return new WaitForSeconds(1);
        pc.Heal(healAmount);
        pc.bodyAudio.clip = modifierAudio;
        pc.bodyAudio.Play();
        yield return new WaitForSeconds(1);
        pc.Heal(healAmount);
        foreach (Collider2D col in Physics2D.OverlapCircleAll(pc.transform.position, radius))
        {
            if (col.CompareTag("Enemy"))
            {
                col.GetComponent<AiHandler>().KnockBack(pc.transform.position, pushForce);
            }
        }
        GameObject part = Instantiate(reviveParticle, pc.transform.position, Quaternion.identity);
        pc.pf.UnfreezePos();
        foreach (Skill sk in pc.skills)
        {
            if (sk && sk.skillType != SkillType.Movement)
            {
                sk.isSkillAvail = true;
                sk.sb.UpdateCD();
            }
        }
        pc.pa.bodyAnim.SetTrigger("revive");
        pc.ph.SetVulnerable();
        pc.ps.fixedJoystick.Avail = true;
        pc.ps.fixedJoystick.defaultCd = cdtemp;
        //trigger anim?
    }
}
