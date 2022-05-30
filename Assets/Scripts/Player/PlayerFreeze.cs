using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeze : MonoBehaviour
{
    public bool isFrozen = false;
    public bool compFrozen = false;
    public PlayerControl pc;
    private float startFixedDelta;

    public void Begin()
    {
        pc = GetComponent<PlayerControl>();
        startFixedDelta = Time.fixedDeltaTime;
    }
        public void Freeze()
    {
        isFrozen = true;
        Time.timeScale = 0.5f;
        Time.fixedDeltaTime = 0.5f * startFixedDelta;
        pc.levelManager.bgmManager.GetComponent<AudioChorusFilter>().dryMix = 0.4f;
        pc.pj.playerRigid.gravityScale = pc.pj.modifiedGravity;
    }
    public void UnFreeze()
    {
        isFrozen = false;
        Time.timeScale = 1;
        Time.fixedDeltaTime = startFixedDelta;
        pc.levelManager.bgmManager.GetComponent<AudioChorusFilter>().dryMix = 0.5f;
        pc.pj.playerRigid.gravityScale = pc.pj.defaultGravity;
    }
    public void FreezePosIndef()
    {
        compFrozen = true;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }
    public void UnfreezePos()
    {
        compFrozen = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }
    public void FreezePosTime(float time)
    {
        StartCoroutine(CoFreezePos(time));
    }
    public IEnumerator CoFreezePos(float time)
    {
        FreezePosIndef();
        yield return new WaitForSeconds(time);
        UnfreezePos();
    }
    public void DisableMove()
    {
        compFrozen = true;
    }
    public void EnableMove()
    {
        compFrozen = false;
    }
}
