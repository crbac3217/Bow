using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public PlayerControl pc;
    public Rigidbody2D playerRigid;
    public float jumpMultiplyer,jumpStatic, reductionMultiplyer, defaultGravity, modifiedGravity, jumpForce, reJumpThreshHold;
    public bool isGrounded, isJumping, isPressed;
    public int curJumpCount;

    public void Begin()
    {
        pc = GetComponent<PlayerControl>();
        playerRigid = GetComponent<Rigidbody2D>();
    }
    public void ResetStat()
    {
        defaultGravity = playerRigid.gravityScale;
        curJumpCount = Convert.ToInt32(pc.stats[5].value);
    }
    private void Update()
    {
        if (isJumping)
        {
            jumpForce -= (reductionMultiplyer * Time.deltaTime);
            playerRigid.velocity = new Vector2(playerRigid.velocity.x, jumpForce);
            if (playerRigid.velocity.y <= reJumpThreshHold)
            {
                isJumping = false;
            }
        }
    }
    public void Jump()
    {
        if (!pc.pf.compFrozen)
        {
            isPressed = true;
            if (curJumpCount > 0)
            {
                pc.pa.bodyAnim.SetBool("isGrounded", false);
                pc.pa.headAnim.SetBool("isGrounded", false);
                pc.pa.bodyAnim.SetTrigger("jump");
                playerRigid.gravityScale = defaultGravity;
                isJumping = true;
                isGrounded = false;
                curJumpCount--;
                jumpForce = Mathf.Log(pc.stats[1].value) * jumpMultiplyer + jumpStatic;
                pc.pm.SetNode();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            pc.pa.bodyAnim.SetBool("isGrounded", true);
            pc.pa.headAnim.SetBool("isGrounded", true);
            isGrounded = true;
            curJumpCount = Convert.ToInt32(pc.stats[5].value);
            pc.pm.SetNode();
        }
    }
    public void OnLetGo()
    {
        isPressed = false;
        playerRigid.gravityScale = defaultGravity;
    }
}
