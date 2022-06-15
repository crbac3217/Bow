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
#if UNITY_ANDROID
    var joystick = pc.ps.fixedJoystick as FixedJoystick;
#elif UNITY_STANDALONE_WIN
        var joystick = pc.ps.dynamicJoystick as DynamicJoystick;
#endif
        float cdtemp = joystick.defaultCd;
        pc.pa.bodyAnim.SetTrigger("fakeDead");
        pc.ph.SetInvincible();
        joystick.CancelShooting();
        joystick.defaultCd = 3;
        joystick.Avail = false;
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
        joystick.Avail = true;
        joystick.defaultCd = cdtemp;
        //trigger anim?
    }
}
