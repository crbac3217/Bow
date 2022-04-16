using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DokebiInst : MonoBehaviour
{
    public List<DamageType> damages = new List<DamageType>();
    public ParticleSystem impactParticle;
    public Animator anim;
    public Crit prankcrit;
    public bool disappear = false;
    public LayerMask mask;
    public float range;
    public CameraParent campar;
    public GameObject affectedParticle;
    public void OnImpact()
    {
        campar.StartCoroutine(campar.CamShake(new Vector2(0f, 0.2f), 0.1f));
        impactParticle.Play();
        foreach (Collider2D col in Physics2D.OverlapCircleAll(transform.position, range, mask))
        {
            if (col.CompareTag("Enemy"))
            {
                Invoke(col.gameObject);
            }
        }
    }
    private void Invoke(GameObject go)
    {
        go.GetComponent<EnemyController>().CritEffect(1, prankcrit);
        go.GetComponent<EnemyController>().CalculateDamage(damages, false, 0);
        var part = Instantiate(affectedParticle, go.transform.position, Quaternion.identity);
    }
    private void Update()
    {
        if (disappear)
        {
            anim.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, anim.gameObject.GetComponent<SpriteRenderer>().color.a - 0.01f);
            if (anim.gameObject.GetComponent<SpriteRenderer>().color.a < 0.05f)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
