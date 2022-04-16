using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LeadArrowhead", menuName = "Modifier/Items/LeadArrowhead", order = 110)]
public class LeadArrowhead : Modifier
{
    public float radius, pushforce;
    public int chanceMax;

    public override void OnEnemyModActive(EnemyArg da)
    {
        base.OnEnemyModActive(da);
        int chance = Random.Range(0, chanceMax);
        if (chance == 1)
        {
            CreateKnockBack(da.hitObj);
        }
    }
    public void CreateKnockBack(GameObject obj)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(obj.transform.position, radius);
        foreach (Collider2D enemy in hitEnemies)
        { 
            if (enemy.gameObject.CompareTag("Enemy") && enemy.gameObject != obj)
            {
                enemy.GetComponent<EnemyController>().aiHandler.KnockBack(obj.transform.position, pushforce);
            }
        }
    }
}
