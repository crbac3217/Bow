using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "JigwiPillar", menuName = "EnemyAttack/Boss/Jigwi/JigwiPillar", order = 105)]
public class JigwiPillar : EnemyAttack
{
    public List<GameObject> pillars = new List<GameObject>();
    public float maxDuration;
    public LayerMask groundCheck;

    public override void Activate()
    {
        int rand = Random.Range((int)duration, (int)maxDuration);
        aiHandler.TriggerAnimation(AttackName, whileMove, rand);
        aiHandler.StartCoroutine(PillarsSetup(rand));
    }
    public IEnumerator PillarsSetup(int rand)
    {
        pillars.Clear();
        for (int i = 0; i < rand; i++)
        {
            yield return new WaitForSeconds(1);
            SpawnPillar();
        }
        aiHandler.TriggerAdditionalAnimation("JigwiPillar", false, 1);
    }
    public void SpawnPillar()
    {
        RaycastHit2D hit = Physics2D.Raycast(((Vector2)aiHandler.transform.position + Vector2.up), Vector2.down, groundCheck);
        Vector2 pcPoint = new Vector2(aiHandler.pc.transform.position.x + (aiHandler.pc.pj.playerRigid.velocity.x * 0.33f), hit.point.y);
        var pillar = Instantiate(projectilePrefab, pcPoint, Quaternion.identity);
        pillar.GetComponent<PillarInst>().damage = (int)(aiHandler.damage * damageMult);
        pillars.Add(pillar);
    }
    public override void AdditionalTrigger()
    {
        foreach (GameObject go in pillars)
        {
            go.GetComponent<Animator>().SetTrigger("Trigger");
        }
        aiHandler.GetComponent<BossAi>().campar.StartCoroutine(aiHandler.GetComponent<BossAi>().campar.CamShake(Vector2.up * 0.05f, 0.5f));
    }
}
