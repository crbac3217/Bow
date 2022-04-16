using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonBreathInstance : MonoBehaviour
{
    public float rate;
    public SpriteRenderer spren;
    public bool fadein, fadeout;
    public List<DamageType> damagetype;
    private void Start()
    {
        spren = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (fadein)
        {
            spren.color = new Color(spren.color.r, spren.color.g, spren.color.b, spren.color.a + rate);
        }
        if (spren.color.a >= 0.75f && fadein)
        {
            fadein = false;
        }
        if (fadeout)
        {
            spren.color = new Color(spren.color.r, spren.color.g, spren.color.b, spren.color.a - rate);
        }
        if (spren.color.a <= 0 && fadeout)
        {
            fadeout = false;
            Destroy(this.gameObject);
        }
    }
    void dealDamage()
    {

    }
}
