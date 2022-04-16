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
        base.OnModifierActive(pc);
        pc.psm.ActivateCoroutine(SetPlayerInvincible(pc));
    }

    IEnumerator SetPlayerInvincible(PlayerControl pc)
    {
        float cdtemp = pc.ps.fixedJoystick.defaultCd;
        pc.pa.bodyAnim.enabled = false;
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
        yield return new WaitForSeconds(1);
        pc.currentHp = (int) (pc.stats[2].value / 16);
        yield return new WaitForSeconds(1);
        pc.currentHp = (int)(pc.stats[2].value / 8);
        yield return new WaitForSeconds(1);
        pc.currentHp = (int)(pc.stats[2].value / 4);
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
            if (sk)
            {
                sk.isSkillAvail = true;
                sk.sb.UpdateCD();
            }
        }
        pc.ph.SetVulnerable();
        pc.ps.fixedJoystick.Avail = true;
        pc.ps.fixedJoystick.defaultCd = cdtemp;
        pc.pa.bodyAnim.enabled = true;
        //trigger anim?
    }
}
