using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonInstance : MonoBehaviour
{
    public bool isright;
    public float liveTime, pulse, speed, range;
    public List<DamageType> damages, halfDamages = new List<DamageType>();
    private bool isGrounded = false, disappear = false;
    public CameraParent campar;
    public GameObject impactParticle;
    private Rigidbody2D rb;

    private void Start()
    {
        StartCoroutine(AliveFor());
        rb = GetComponent<Rigidbody2D>();
        foreach (DamageType dt in damages)
        {
            float val = dt.value / 2;
            DamageType half = new DamageType
            {
                damageElement = dt.damageElement,
                value = (int)val
            };
            halfDamages.Add(half);
        }
    }
    private IEnumerator AliveFor()
    {
        yield return new WaitForSeconds(liveTime);
        Destroy(this.gameObject);
    }
    private void Update()
    {
        if (disappear)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, GetComponent<SpriteRenderer>().color.a - 0.01f);
            if (GetComponent<SpriteRenderer>().color.a <= 0.05f)
            {
                Destroy(gameObject);
            }
        }
        if (isGrounded)
        {
            if (isright)
            {
                rb.velocity = new Vector2 (1 * speed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-1 * speed, rb.velocity.y);
            }
        }
        else
        {
            if (isright)
            {
                rb.velocity = new Vector2(2 * speed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-2 * speed, rb.velocity.y);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !isGrounded)
        {
            isGrounded = true;
            GetComponent<Collider2D>().isTrigger = false;
            Impact();
        }
    }
    private void Impact()
    {
        campar.StartCoroutine(campar.CamShake(new Vector2(0f, 0.2f), 0.1f));
        foreach (Collider2D col in Physics2D.OverlapCircleAll(transform.position, range))
        {
            if (col.CompareTag("Enemy"))
            {
                col.GetComponent<EnemyController>().CalculateDamage(damages, false, 0);
            }
        }
        var particle = Instantiate(impactParticle, GroundSpot(), Quaternion.identity);
        StartCoroutine(Pulse());
    }
    private Vector2 GroundSpot()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector2.down, LayerMask.NameToLayer("Ground"));
        if (ray)
        {
            return ray.point;
        }
        else
        {
            return transform.position;
        }
    }
    private IEnumerator Pulse()
    {
        yield return new WaitForSeconds(pulse);
        foreach (Collider2D col in Physics2D.OverlapCircleAll(transform.position, range/2))
        {
            if (col.CompareTag("Enemy"))
            {
                col.GetComponent<EnemyController>().CalculateDamage(halfDamages, false, 0);
            }
        }
        StartCoroutine(Pulse());
    }
}
