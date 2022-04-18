using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public bool doesHitGround, Targetted, destroyable, doesDestroyUponHit, Accel, Decel;
    private bool destroy;
    public float speed, lifeTime, speedMod, maxSpeed;
    public int damage, hitPoint;
    public Vector2 pos, dir;
    public PlayerControl pc;
    private Rigidbody2D rb;
    private SpriteRenderer spr;
    private Animator anim;

    private void Start()
    {
        if (pos != Vector2.zero)
        {
            dir = (pos - (Vector2)transform.position).normalized;
        }
        destroy = false;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Live());
        spr = GetComponent<SpriteRenderer>();
        SetUp();
    }
    public virtual void SetUp()
    {

    }
    void Update()
    {
        if (destroy)
        {
            rb.velocity = Vector2.zero;
            spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, spr.color.a - 0.015f);
            if (spr.color.a < 0.05f)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (Targetted)
            {
                Vector2 dir = (pc.transform.position - transform.position).normalized;
                rb.velocity = dir * speed;
            }
            else
            {
                rb.velocity = dir * speed;
            }
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        if (Accel)
        {
            speed = Mathf.Clamp(speed + speedMod, 0, maxSpeed);
        }
        if (Decel)
        {
            speed = Mathf.Clamp(speed - speedMod, 0, maxSpeed);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            if (doesHitGround && !destroy)
            {
                destroy = true;
                anim.SetTrigger("Hit");
            }
        }else if (collision.CompareTag("Projectile"))
        {
            if (destroyable && !destroy)
            {
                hitPoint--;
                if (hitPoint <= 0)
                {
                    destroy = true;
                    anim.SetTrigger("Hit");
                    collision.GetComponent<Projectile>().OnHit();
                }
            }
        }else if (collision.CompareTag("Player") && !destroy)
        {
            if (doesDestroyUponHit)
            {
                destroy = true;
            }
            collision.GetComponent<PlayerHit>().OnPlayerHit(transform.position, damage);
            anim.SetTrigger("Hit");
        }
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    private IEnumerator Live()
    {
        yield return new WaitForSeconds(lifeTime);
        destroy = true;
    }
}
