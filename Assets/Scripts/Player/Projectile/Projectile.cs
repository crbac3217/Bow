using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool isHit = false, disappear = false, onHit = false, doesHitGround = true, isParticleColored = false;
    public float critChance = 0, liveTime = 6, disapperRate = 0.01f, particleDisplacementValue;
    public ParticleSystem onHitParticle;
    public List<DamageType> damages;
    public AudioClip enemyImpact, groundImpact;
    public List<SpriteRenderer> spren = new List<SpriteRenderer>();
    public Rigidbody2D rb;
    public Animator anim;
    private AudioSource audio;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spren.Add(GetComponent<SpriteRenderer>());
        SetUp();
        StartCoroutine(Fired());
        audio = GetComponent<AudioSource>();
    }
    public virtual void SetUp()
    {

    }

    public IEnumerator Fired()
    {
        yield return new WaitForSeconds(liveTime);
        disappear = true;
    }
    private void FixedUpdate()
    {
        if (!isHit)
        {
            Flying();
        }
        if (disappear)
        {
            foreach (SpriteRenderer spr in spren)
            {
                spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, spr.color.a - disapperRate);
            }
            if (spren[0].color.a < disapperRate)
            {
                Destroy(this.gameObject);
            }
        }
    }
    public virtual void Flying()
    {
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !isHit)
        {
            OnCollisionEnemy(collision.gameObject.GetComponent<EnemyController>());
        }
        else if (collision.CompareTag("Ground") && !isHit)
        {
            if (doesHitGround)
            {
                Color temp = collision.GetComponent<Platform>().color;
                InvokeParticle(temp);
                OnHit();
                audio.clip = groundImpact;
                audio.Play();
            }
        }
        else if (collision.CompareTag("EnemyProjectile") && !isHit)
        {
            collision.GetComponent<EnemyProjectile>().Dest(this);
        }
    }
    public virtual void OnHit()
    {
        isHit = true;
        if (anim != null)
        {
            anim.enabled = false;
        }
        disappear = true;
        Destroy(rb);
        Destroy(GetComponent<Collider2D>());
    }
    public virtual void OnCollisionEnemy(EnemyController ec)
    {
        OnHit();
        DealDamage(ec);
        audio.clip = enemyImpact;
        audio.Play();
        transform.SetParent(ec.transform);
        Color temp = ec.color;
        InvokeParticle(temp);
    }
    public virtual void DealDamage(EnemyController ec)
    {
        ec.CalculateDamage(damages, onHit, critChance);
    }
    public virtual void InvokeParticle(Color col)
    {
        var main = onHitParticle.main;
        if (isParticleColored)
        {
            main.startColor = col;
        }
        main.startSpeed = new ParticleSystem.MinMaxCurve(0, particleDisplacementValue * 6);
        onHitParticle.Play();
    }
    private void OnDestroy()
    {
        DestroyEdit();
    }
    public virtual void DestroyEdit()
    {

    }
    //public float liveSec, damageMultiplier, firePower, fireLevel, xMin, yMin, yMax, totalValue, heldVelo;
    //public bool isHit, disappear = false;
    //public ParticleSystem hitPart;
    //public List<DamageType> damageList = new List<DamageType>();
    //public List<GameObject> attachments = new List<GameObject>();
    //private List<SpriteRenderer> spren = new List<SpriteRenderer>();
    //public Rigidbody2D rb;
    //public PlayerControl pc;
    //public Animator anim;

    //private void Start()
    //{
    //    yMin = this.transform.position.y;
    //    xMin = this.transform.position.x;
    //    pc = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
    //    rb = GetComponent<Rigidbody2D>();
    //    spren.Add(GetComponent<SpriteRenderer>());
    //    foreach (GameObject attat in attachments)
    //    {
    //        spren.Add(attat.GetComponent<SpriteRenderer>());
    //    }
    //    StartCoroutine(Fired());
    //}
    //private void Update()
    //{
    //    if (!isHit)
    //    {
    //        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
    //        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    //        if (this.transform.position.y > yMax)
    //        {
    //            yMax = this.transform.position.y;
    //        }
    //    }
    //    if (disappear)
    //    {
    //        foreach (SpriteRenderer spr in spren)
    //        {
    //            spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, spr.color.a - 0.01f);
    //        }
    //        if (spren[0].color.a < 0.03f)
    //        {
    //            Destroy(this.gameObject);
    //        }
    //    }
    //}
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Enemy") && !isHit)
    //    {
    //        isHit = true;
    //        if (anim != null)
    //        {
    //            anim.enabled = false;
    //        }
    //        CalculateDamage(damageList, damageMultiplier * fireLevel);
    //        collision.GetComponent<EnemyController>().CalculateDamage(damageList, true, pc.stats[3].value);
    //        transform.SetParent(collision.transform);
    //        disappear = true;
    //        Color temp = collision.GetComponent<EnemyController>().color;
    //        var main = hitPart.main;
    //        main.startColor = temp;
    //        main.startSpeed = new ParticleSystem.MinMaxCurve(0, heldVelo * 6);
    //        hitPart.Play();
    //        rb.bodyType = RigidbodyType2D.Static;
    //    }else if (collision.CompareTag("Player") || collision.CompareTag("Projectile"))
    //    {

    //    }
    //    else if (collision.CompareTag("Ground") && !isHit)
    //    {
    //        isHit = true;
    //        if (anim != null)
    //        {
    //            anim.enabled = false;
    //        }
    //        rb.bodyType = RigidbodyType2D.Static;
    //        disappear = true;
    //        Color temp = collision.GetComponent<Platform>().color;
    //        var main = hitPart.main;
    //        main.startColor = temp;
    //        main.startSpeed = new ParticleSystem.MinMaxCurve(0, heldVelo * 6);
    //        hitPart.Play();
    //        Debug.Log("groundhit");
    //    }
    //}

    //IEnumerator Fired()
    //{
    //    yield return new WaitForSeconds(liveSec);
    //    disappear = true;
    //}
    //public void CalculateDamage(List<DamageType> damages, float multiplier)
    //{
    //    float xMax = transform.position.x;
    //    float yThis = transform.position.y;
    //    if (Mathf.Abs(yThis - yMin) > Mathf.Abs(yMax - yMin))
    //    {
    //        yMax = yThis;
    //    }
    //    float accuracy = pc.stats[4].value / 30 * (Mathf.Abs(xMax - xMin) * 0.3f + Mathf.Abs(yMax - yMin));
    //    foreach(DamageType damage in damages)
    //    {
    //        damage.value = Mathf.RoundToInt(damage.value * multiplier + damage.value * accuracy * multiplier);
    //    }
    //}
}
