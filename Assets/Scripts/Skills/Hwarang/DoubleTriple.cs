using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleTriple : MonoBehaviour
{
    public List<DamageType> damages = new List<DamageType>();
    public DamageElement delem;
    public Vector2 dir;
    public GameObject[] projectiles = new GameObject[] { };
    public int tripleCounter;

    public void Start()
    {
        tripleCounter = 0;
        if (delem != DamageElement.None)
        {
            Animator anim = GetComponent<Animator>();
            anim.SetTrigger(delem.ToString());
            foreach (DamageType damage in damages)
            {
                if (damage.damageElement == delem)
                {
                    damage.value *= 2;
                }
            }
        }
    }
    public void OnFireDouble()
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        GameObject arrow = Instantiate(projectiles[(int)delem], transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
        var arrowRigid = arrow.GetComponent<Rigidbody2D>();
        var arrowProj = arrow.GetComponent<Projectile>();
        arrowProj.damages = damages;
        arrowRigid.velocity = dir * Random.Range(5, 10);
        arrowProj.onHit = true;
        arrowProj.particleDisplacementValue = 1;
    }
    public void OnFireTriple()
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        GameObject arrow = Instantiate(projectiles[(int)delem], transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
        var arrowRigid = arrow.GetComponent<Rigidbody2D>();
        var arrowProj = arrow.GetComponent<TripleShotProjectile>();
        arrowProj.damages = damages;
        arrowRigid.velocity = dir * Random.Range(3, 7);
        arrowProj.onHit = true;
        arrowProj.particleDisplacementValue = 1;
        tripleCounter++;
        if (tripleCounter >= 3)
        {
            arrowProj.doCrit = true;
            arrowProj.delem = delem;
        }
    }
    public void AnimEnded()
    {
        Destroy(this.gameObject);
    }
}
