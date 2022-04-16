using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DeathStep", menuName = "Modifier/Hwarang/DeathStep", order = 110)]
public class DeathStep : MoveMod
{
    private Vector2 startpos, endpos;

    public override IEnumerator Dashing(PlayerControl pc)
    {
        pc.pf.DisableMove();
        pc.pm.dashing = true;
        startpos = pc.transform.position;
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
        endpos = pc.transform.position;
        DealDamageInBetween(startpos, endpos, pc);
        pc.pf.EnableMove();
        pc.pm.dashing = false;
    }
    private void DealDamageInBetween(Vector2 start, Vector2 end, PlayerControl pc)
    {
        foreach (Collider2D col in Physics2D.OverlapAreaAll(new Vector2(start.x, start.y + 0.4f), new Vector2(end.x, end.y - 0.4f)))
        {
            if (col.CompareTag("Enemy"))
            {
                col.GetComponent<EnemyController>().CalculateDamage(pc.damageTypes, false, 0);
            }
        }
    }
}
