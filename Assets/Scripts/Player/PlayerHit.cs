using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerHit : MonoBehaviour
{
    public int invincibleStack = 0;
    public GameObject hitPart;
    public float invSec = 0.25f, knockAmount;
    public List<Modifier> hitList = new List<Modifier>();
    public List<Modifier> deadList = new List<Modifier>();
    public AudioClip hitClip, deadClip;
    public bool invincible = false;
    private PlayerControl pc;
    public float knockback;

    public void Begin()
    {
        pc = GetComponent<PlayerControl>();
    }
    public void HitPlayerNoDir(int amount)
    {
        OnPlayerHit(Vector2.right, amount);
    }
    public void OnPlayerHit(Vector2 origin, int amount)
    {
        if (invincible)
        {
            Debug.Log("invincible right now!");
        }
        else
        {
            pc.bodyAudio.clip = hitClip;
            pc.bodyAudio.Play();
            var inst = Instantiate(hitPart, transform.position, Quaternion.identity);
            inst.GetComponent<ParticleSystem>().Play();
            pc.currentHp -= amount;
            pc.UpdateHP(amount);
            if (pc.currentHp <= 0)
            {
                DeadCheck();
            }
            else
            {
                HitCheck(origin, amount);
            }
        }
    }
    public void SetInvincible()
    {
        invincible = true;
        invincibleStack++;
        pc.CheckInvincible();
    }
    public void SetVulnerable()
    {
        invincibleStack--;
        if (invincibleStack <= 0)
        {
            invincible = false;
        }
        pc.CheckInvincible();
    }
    private void HitCheck(Vector2 origin, int amount)
    {
        foreach (Modifier hitmod in hitList)
        {
            hitmod.OnModifierActive(pc);
            hitmod.NumberMod(pc, amount);
        }
        pc.campar.StartCoroutine(pc.campar.CamShake(new Vector2(0.01f, 0), 0.1f));
        StartCoroutine(setInvForSec(origin, invSec));
    }
    private IEnumerator setInvForSec(Vector2 origin, float t)
    {
        SetInvincible();
        CancelAllAnim();
        pc.pf.DisableMove();
        pc.pa.body.GetComponent<SpriteRenderer>().color = Color.red;
        Vector2 knockdirection = ((Vector2)transform.position - origin + Vector2.up).normalized;
        pc.GetComponent<Rigidbody2D>().velocity = knockdirection * knockAmount;
        pc.pa.bodyAnim.SetTrigger("playerHit");
        yield return new WaitForSeconds(t);
        pc.pj.curJumpCount++;
        pc.pa.body.GetComponent<SpriteRenderer>().color = Color.white;
        pc.pf.EnableMove();
        SetVulnerable();
    }
    public void CancelAllAnim()
    {
        pc.pf.UnfreezePos();
        pc.ps.CancelShooting();
    }
    private void DeadCheck()
    {
        if (deadList.Count > 0)
        {
            deadList[0].OnModifierActive(pc);
            deadList.Remove(deadList[0]);
        }
        else
        {
            Dead();
        }
    }
    public void Dead()
    {
        SetInvincible();
        CancelAllAnim();
        pc.bodyAudio.clip = deadClip;
        pc.bodyAudio.Play();
        pc.pf.DisableMove();
        Destroy(pc.healthBar.gameObject);
        pc.pa.bodyAnim.SetTrigger("playerDead");
    }
}