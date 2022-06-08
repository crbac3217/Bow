using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LeadArrowhead", menuName = "Modifier/Items/LeadArrowhead", order = 110)]
public class LeadArrowhead : Modifier
{
    public float radius;
    public int chanceMax;

    public override void OnEnemyModActive(EnemyArg da)
    {
        int chance = Random.Range(0, chanceMax);
        if (chance == 1)
        {
            CreateKnockBack(da.hitObj);
            base.OnEnemyModActive(da);
        }
    }
    public void CreateKnockBack(GameObject obj)
    {
        if (obj)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(obj.transform.position, radius);
            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy.gameObject.CompareTag("Enemy") && enemy.gameObject != obj)
                {
                    float pushforce = Vector2.Distance(enemy.transform.position, obj.transform.position) * 2;
                    enemy.GetComponent<EnemyController>().aiHandler.KnockBack(obj.transform.position, pushforce);
                }
            }
        }
    }
}
