using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiteObj : MonoBehaviour
{
    public DamageElement delem;
    public List<DamageType> damages = new List<DamageType>();
    public ParticleSystem impact;
    public EnemyController enemy;
    private SpriteRenderer spren;
    private void Start()
    {
        spren = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        spren.color = new Color(1, 1, 1,spren.color.a + 0.01f);
    }
    public void OnHit()
    {
        enemy.CalculateDamage(damages, false, 0);
        if (enemy)
        {
            if (delem != DamageElement.None)
            {
                foreach (DamageType dt in damages)
                {
                    if (dt.damageElement == delem)
                    {
                        enemy.CritEffect(dt.value, enemy.damageCrits[(int)delem - 1]);
                    }
                }
            }
        }
        impact.Play();
    }
    public void OnEnd()
    {
        Destroy(this.gameObject);
    }
}
