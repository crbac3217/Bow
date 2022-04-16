using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLinearProj : MonoBehaviour
{
    public bool isRight, destroy;
    public int damage;
    public float speed, lifeTime;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spr;

    void Start()
    {
        if (!isRight)
        {
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y);
        }
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        StartCoroutine(AliveFor());
    }

    // Update is called once per frame
    void Update()
    {
        if (destroy)
        {
            spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, spr.color.a - 0.015f);
            if (spr.color.a < 0.05f)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (isRight)
            {
                rb.velocity = Vector2.right * speed;
            }
            else
            {
                rb.velocity = Vector2.left * speed;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHit>().OnPlayerHit(transform.position, damage);
            StartDest();
        }
    }
    private IEnumerator AliveFor()
    {
        yield return new WaitForSeconds(lifeTime);
        StartDest();
    }
    private void StartDest()
    {
        anim.SetTrigger("Hit");
        destroy = true;
        rb.velocity = Vector2.zero;
        Destroy(GetComponent<Collider2D>());
    }
}
