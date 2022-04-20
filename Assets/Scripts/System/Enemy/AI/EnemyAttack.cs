using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyAttack", menuName = "EnemyAttack/Default", order = 105)]
public class EnemyAttack : ScriptableObject
{
    public AttackType type;
    public bool avail, whileMove, cantCutoff;
    public string AttackName;
    public float coolDown, duration, damageMult, dashDist;
    public GameObject attackRangePrefab, projectilePrefab;
    private GameObject rangeInst;
    public EnemyAttackRange range;
    public AiHandler aiHandler;

    public virtual void SetUp()
    {
        rangeInst = Instantiate(attackRangePrefab, aiHandler.visuals.transform.position, Quaternion.identity) as GameObject;
        rangeInst.transform.localScale = rangeInst.transform.localScale * (aiHandler.transform.localScale.x / Mathf.Abs(aiHandler.transform.localScale.x));
        rangeInst.transform.SetParent(aiHandler.visuals.transform);
        range = rangeInst.GetComponent<EnemyAttackRange>();
    }
    public virtual void Activate()
    {
        if (!cantCutoff)
        {
            aiHandler.TriggerAnimation(AttackName, whileMove, duration);
        }
        else
        {
            aiHandler.TriggerAnimationNoDisrupt(AttackName, whileMove, duration);
        }
        
    }
    public virtual void DealDamage(PlayerControl pc)
    {
        if (range.avail)
        {
            float amount = aiHandler.damage * damageMult;
            Debug.Log("attack induced with " + amount);
            pc.ph.OnPlayerHit(aiHandler.visuals.transform.position, (int)amount);
        }
    }
    public virtual void ShootProjectile(PlayerControl pc)
    {
        var inst = Instantiate(projectilePrefab, aiHandler.visuals.transform.position, Quaternion.identity);
        EnemyProjectile ep = inst.GetComponent<EnemyProjectile>();
        if (ep.Targetted)
        {
            ep.pc = pc;
        }
        else
        {
            ep.pos = pc.transform.position;
        }
        ep.damage = Mathf.RoundToInt(aiHandler.damage * damageMult);

    }
    public virtual void AttackEtc(PlayerControl pc)
    {
    }
    public IEnumerator CoolDown()
    {
        avail = false;
        yield return new WaitForSeconds(coolDown);
        avail = true;
    }
    public virtual void AdditionalTrigger()
    {

    }
}
public enum AttackType { Melee = 0, Shoot = 1, AoE = 2, Buff = 3, Charge = 4, etc = 5 }
