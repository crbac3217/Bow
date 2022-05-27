using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FlurryInstance : MonoBehaviour
{
    public GameObject projectile;
    public List<DamageType> damages;
    public float rate;
    public int numberOfProjectiles;
    public SpriteRenderer spren;
    public bool fadein, fadeout;

    private void Start()
    {
        spren = GetComponent<SpriteRenderer>();
    }
    void FixedUpdate()
    {
        if (fadein)
        {
            spren.color = new Color(spren.color.r, spren.color.g, spren.color.b, spren.color.a + rate);
        }
        if (spren.color.a >= 0.98f && fadein)
        {
            fadein = false;
            fadeout = true;
            FlurryShoot();
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
    public void FlurryShoot()
    {
        for (int i = 0; i < numberOfProjectiles; i++)
        {
            var temp = Instantiate(projectile, this.transform.position, Quaternion.identity);
            var tempProj = temp.GetComponent<TargettedProjectile>();
            tempProj.speed = 2 + (i * 0.8f);
            tempProj.disp = i * 0.5f + 0.5f;
            tempProj.damages = this.damages;
        }
        fadeout = true;
    }
}
