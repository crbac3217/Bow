using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMod : Modifier
{
    public GameObject movePrefab;
    public float doubleTapCd;
    public float invinDuration, dashSpeed, dashDuration;
    public Skill skill;
    private float lastTap;
    private bool wasRight;
    public override void OnModifierActive(PlayerControl pc)
    {
        base.OnModifierActive(pc);
        if (wasRight == pc.pm.rightPressed)
        {
            if (((Time.time - lastTap) < doubleTapCd) && skill.isSkillAvail)
            {
                pc.psm.StartCoroutine(Dashing(pc));
                pc.psm.StartCoroutine(Invincible(pc));
                skill.EndSkill(pc);
            }
            else
            {
                lastTap = Time.time;
            }
        }
        wasRight = pc.pm.rightPressed;
    }
    public virtual IEnumerator Dashing(PlayerControl pc)
    {
        pc.pf.DisableMove();
        pc.pm.dashing = true;
        if (pc.pm.rightPressed)
        {
            var inst = Instantiate(movePrefab, (Vector2)pc.transform.position + (Vector2.right * dashSpeed * dashDuration), Quaternion.identity);
            inst.transform.localScale = new Vector2(inst.transform.localScale.x * pc.pm.body.transform.localScale.x, inst.transform.localScale.y);
            pc.pm.modifiedVelocity = Vector2.right * dashSpeed;
        }
        else
        {
            var inst = Instantiate(movePrefab, (Vector2)pc.transform.position + (Vector2.left * dashSpeed * dashDuration), Quaternion.identity);
            inst.transform.localScale = new Vector2(inst.transform.localScale.x * pc.pm.body.transform.localScale.x, inst.transform.localScale.y);
            pc.pm.modifiedVelocity = Vector2.left * dashSpeed;
        }
        yield return new WaitForSeconds(dashDuration);
        pc.pf.EnableMove();
        pc.pm.dashing = false;
    }
    public IEnumerator Invincible(PlayerControl pc)
    {
        pc.ph.SetInvincible();
        yield return new WaitForSeconds(invinDuration);
        pc.ph.SetVulnerable();
    }
}
