using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterFlyCont : MonoBehaviour
{
    public float minX, minY, maxX, maxY, speed;
    public GameObject projectilePrefab;
    public PlayerControl pc;
    public bool disappear;
    private bool stand;
    private Animator anim;
    private SpriteRenderer spren;
    private Rigidbody2D rb;
    private Vector2 dest, dir;

    private void Start()
    {
        anim = GetComponent<Animator>();
        spren = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        dest = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        dir = (dest - (Vector2)transform.position).normalized;
        stand = false;
        if (dir.x > 0)
        {
            spren.flipY = false;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            spren.flipY = true;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        anim.SetBool("Stand", false);
    }
    private void FixedUpdate()
    {
        if (disappear)
        {
            spren.color = new Color(1, 1, 1, spren.color.a - 0.01f);
            if (spren.color.a < 0.05f)
            {
                Destroy(gameObject);
            }
        }
        else if (!stand)
        {
            Move();
        }
    }
    private void Move()
    {
        rb.velocity = dir * speed;
        if (Vector2.Distance(transform.position,dest) < 0.05f)
        {
            Stand();
        }
    }
    private void Stand()
    {
        stand = true;
        rb.velocity = Vector2.zero;
        if (pc.transform.position.x > transform.position.x)
        {
            spren.flipY = false;
            transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
        }
        else
        {
            spren.flipY = true;
            transform.rotation = Quaternion.AngleAxis(180, Vector3.forward);
        }
        anim.SetBool("Stand", true);
    }
    public void Shoot()
    {
        if (stand && !disappear)
        {
            anim.SetTrigger("Shoot");
        }
    }
    public void ShootTriggered()
    {
        var inst = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        EnemyProjectile ep = inst.GetComponent<EnemyProjectile>();
        ep.pos = pc.transform.position;
    }
    public void ShootFinished()
    {
        dest = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        dir = (dest - (Vector2)transform.position).normalized;
        stand = false;
        if (dir.x > 0)
        {
            spren.flipY = false;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            spren.flipY = true;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        anim.SetBool("Stand", false);
    }
}
