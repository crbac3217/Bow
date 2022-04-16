using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmpleShotPlayerside : MonoBehaviour
{
    public List<DamageType> damages = new List<DamageType>();
    public Vector2 dir;
    public GameObject projectile;

    public void OnFire()
    {
        GameObject arrow = Instantiate(projectile, transform.position, Quaternion.identity);
        var arrowRigid = arrow.GetComponent<Rigidbody2D>();
        var arrowProj = arrow.GetComponent<Projectile>();
        arrowProj.damages = damages;
        arrowRigid.velocity = dir * Random.Range(5, 10);
        arrowProj.onHit = false;
        arrowProj.particleDisplacementValue = 1;
    }
    public void AnimEnded()
    {
        Destroy(this.gameObject);
    }
}
