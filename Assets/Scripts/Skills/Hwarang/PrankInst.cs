using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrankInst : MonoBehaviour
{
    private bool disapper = false, appear = true;
    public float aoe;
    public List<DamageType> damages;
    private SpriteRenderer sprite;
    private Animator anim;
    public Crit prankCrit;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();

    }
    public void SkillHit()
    {
        foreach (Collider2D col in Physics2D.OverlapCircleAll(sprite.gameObject.transform.position, sprite.gameObject.transform.localScale.x * aoe))
        {
            if (col.gameObject.CompareTag("Enemy"))
            {
                col.GetComponent<EnemyController>().CritEffect(1, prankCrit);
                col.GetComponent<EnemyController>().CalculateDamage(damages, false, 0);
            }
        }
        disapper = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (disapper)
        {
            sprite.color = new Color(1, 1, 1, sprite.color.a - 0.02f);
            if (sprite.color.a <= 0.05f)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            if (appear)
            {
                sprite.color = new Color(1, 1, 1, sprite.color.a + 0.02f);
                if (sprite.color.a >= 0.95f)
                {
                    sprite.color = new Color(1, 1, 1, 1);
                    appear = false;
                }
            }
        }
    }
}
