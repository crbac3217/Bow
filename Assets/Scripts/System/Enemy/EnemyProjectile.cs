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
    private AudioSource audioSource;
    public AudioClip playerHit, groundHit;

    private void Start()
    {
        if (GetComponent<AudioSource>())
        {
            audioSource = GetComponent<AudioSource>();
        }
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
    void FixedUpdate()
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
                if (groundHit)
                {
                    audioSource.clip = groundHit;
                    audioSource.Play();
                }
            }
        }
        else if (collision.CompareTag("Player") && !destroy)
        {
            if (doesDestroyUponHit)
            {
                destroy = true;
            }
            if (playerHit)
            {
                audioSource.clip = playerHit;
                audioSource.Play();
            }
            collision.GetComponent<PlayerHit>().OnPlayerHit(transform.position, damage);
            anim.SetTrigger("Hit");
        }
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    public void Dest(Projectile source)
    {
        if (destroyable && !destroy)
        {
            hitPoint--;
            if (hitPoint <= 0)
            {
                destroy = true;
                anim.SetTrigger("Hit");
                if (source)
                {
                    source.OnHit();
                }
            }
        }
    }
    private IEnumerator Live()
    {
        yield return new WaitForSeconds(lifeTime);
        destroy = true;
    }
}
