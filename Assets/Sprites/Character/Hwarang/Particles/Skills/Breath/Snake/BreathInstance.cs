using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathInstance : MonoBehaviour
{
    public List<DamageType> damages = new List<DamageType>();
    public GameObject affectedParticle;
    public SkillBreathInst parent;
    public BreathCollider bc;
    public DamageElement delem;

    public void TriggerHit()
    {
        foreach (GameObject enemy in bc.enemies)
        {
            enemy.GetComponent<EnemyController>().CalculateDamage(damages, false, 0);
            var part = Instantiate(affectedParticle, enemy.transform.position, Quaternion.identity);
            if (delem != DamageElement.None && enemy)
            {
                foreach (DamageType dt in damages)
                {
                    if (dt.damageElement == delem)
                    {
                        enemy.GetComponent<EnemyController>().CritEffect(dt.value, enemy.GetComponent<EnemyController>().damageCrits[(int)delem - 1]);
                    }
                }
            }
        }
    }
    public void EndAnim()
    {
        parent.GetComponent<Animator>().SetTrigger("skillEnded");
        parent.disappear = true;
        Destroy(this.gameObject);
    }
}
