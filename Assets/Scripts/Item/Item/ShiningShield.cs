using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShiningShield", menuName = "Modifier/Items/ShiningShield", order = 110)]
public class ShiningShield : Modifier
{
    public GameObject shineEffectPrefab;
    public Crit stun;
    public int chanceMax = 1, stunDuration;
    public float radius;

    public override void OnModifierActive(PlayerControl pc)
    {
        base.OnModifierActive(pc);
        int chance = Random.Range(0, chanceMax);
        if (chance == 1)
        {
            var part = Instantiate(shineEffectPrefab, pc.transform.position, Quaternion.identity);
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(pc.transform.position, radius);
            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy.gameObject.CompareTag("Enemy"))
                {
                    enemy.GetComponent<EnemyController>().CritEffect(stunDuration, stun);
                }
            }
        }
    }
}
