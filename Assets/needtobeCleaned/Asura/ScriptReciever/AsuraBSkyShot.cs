using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AsuraBSkyShot", menuName = "EnemyAttack/Boss/Asura/AsuraBSkyShot", order = 105)]
public class AsuraBSkyShot : EnemyAttack
{
    public GameObject chargeParticle, expParticle, marker;
    private GameObject chargeInst;
    private int rand;
    private Vector2 playerpos;
    public LayerMask ground;
    public override void Activate()
    {
        base.Activate();
        rand = Random.Range(3, 8);
        chargeInst = Instantiate(chargeParticle, aiHandler.visuals.transform);
        playerpos = aiHandler.pc.transform.position;
    }
    public override void AttackEtc(PlayerControl pc)
    {
        Destroy(chargeInst);
        var explodePart = Instantiate(expParticle, aiHandler.visuals.transform);
        aiHandler.StartCoroutine(SkyShotPar());
        base.AttackEtc(pc);
    }
    public IEnumerator SkyShotPar()
    {
        if (rand > 0)
        {
            float interval = Random.Range(0.2f, 0.5f);
            rand--;
            aiHandler.StartCoroutine(SkyShot());
            yield return new WaitForSeconds(interval);
            aiHandler.StartCoroutine(SkyShotPar());
        }
        else
        {
            yield return null;
        }
    }
    public IEnumerator SkyShot()
    {
        Vector2 raypos = new Vector2(playerpos.x + Random.Range(-1, 1), aiHandler.visuals.transform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(raypos, Vector2.down, ground);
        Vector2 markPos = new Vector2();
        if (hit)
        {
            markPos = hit.point;
        }
        else
        {
            markPos = playerpos;
        }
        var mark = Instantiate(marker, markPos, Quaternion.identity);
        yield return new WaitForSeconds(1);
        var inst = Instantiate(projectilePrefab, new Vector2(markPos.x, markPos.y + 5), Quaternion.identity);
        EnemyProjectile ep = inst.GetComponent<EnemyProjectile>();
        ep.damage = Mathf.RoundToInt(aiHandler.damage * damageMult);
        ep.dir = Vector2.down;
    }
}
