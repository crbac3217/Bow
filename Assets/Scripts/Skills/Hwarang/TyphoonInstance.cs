using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TyphoonInstance : MonoBehaviour
{
    public GameObject projectile;
    public List<DamageType> damages;
    public float rate, projspeed;
    public int numberOfProjectiles;
    public SpriteRenderer spren;
    public bool fadein, fadeout;

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
    public void TyphoonShoot(bool tilt)
    {
        for (int i = 0; i < numberOfProjectiles; i++)
        {
            float tempangle = 180 + (i * (180 / (numberOfProjectiles - 1)));
            if (tilt)
            {
                tempangle += 8;
            }
            var temp = Instantiate(projectile, this.transform.position, Quaternion.AngleAxis(tempangle, Vector3.forward));
            temp.GetComponent<Rigidbody2D>().velocity = Quaternion.AngleAxis(tempangle, Vector3.forward) * Vector2.right * projspeed;
            temp.GetComponent<UnityEngine.Rendering.Universal.Light2D>().color = GetComponent<UnityEngine.Rendering.Universal.Light2D>().color;
            var tempProj = temp.GetComponent<Projectile>();
            tempProj.damages = this.damages;

        }
    }
}
