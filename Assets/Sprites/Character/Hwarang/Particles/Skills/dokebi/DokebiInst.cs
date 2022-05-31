using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DokebiInst : MonoBehaviour
{
    public List<DamageType> damages = new List<DamageType>();
    public ParticleSystem impactParticle;
    public SpriteRenderer spren;
    public Animator anim;
    public Crit prankcrit;
    public bool disappear = false;
    public LayerMask mask;
    public float range;
    public CameraParent campar;
    public GameObject affectedParticle;
    public AudioClip grab, impact;
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = grab;
        audioSource.Play();
    }
    public void OnImpact()
    {
        audioSource.clip = impact;
        audioSource.Play();
        campar.StartCoroutine(campar.CamShake(new Vector2(0f, 0.2f), 0.1f));
        impactParticle.Play();
        foreach (Collider2D col in Physics2D.OverlapCircleAll(transform.position, range, mask))
        {
            if (col.CompareTag("Enemy"))
            {
                Invoke(col.gameObject);
            }
        }
        spren = anim.gameObject.GetComponent<SpriteRenderer>();
        disappear = true;
    }
    private void Invoke(GameObject go)
    {
        go.GetComponent<EnemyController>().CritEffect(1, prankcrit);
        go.GetComponent<EnemyController>().CalculateDamage(damages, false, 0);
        var part = Instantiate(affectedParticle, go.transform.position, Quaternion.identity);
    }
    private void FixedUpdate()
    {
        if (disappear)
        {
            spren.color = new Color(spren.color.r, spren.color.g, spren.color.b, anim.gameObject.GetComponent<SpriteRenderer>().color.a - 0.003f);
            if (anim.gameObject.GetComponent<SpriteRenderer>().color.a < 0.05f)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
