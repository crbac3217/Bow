using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SunMoonInst : MonoBehaviour
{
    public bool isSun;
    public Crit burn;
    public float livetime, pulseInterval, speed;
    public List<DamageType> damages;
    private Rigidbody2D rb;
    private bool isGrounded = false, pulseUp, disappear = false, impacted = false;
    private UnityEngine.Rendering.Universal.Light2D lightt;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        lightt = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        StartCoroutine(Live());
        if (isSun)
        {
            List<DamageType> templist = new List<DamageType>() { };
            foreach (DamageType dtype in damages)
            {
                float mult = 1f;
                if (dtype.damageElement == DamageElement.Fire)
                {
                    mult = 1.5f;
                }
                DamageType temp = new DamageType
                {
                    damageElement = dtype.damageElement,
                    value = (int)(dtype.value * mult)
                };
                templist.Add(temp);
            }
            damages.Clear();
            damages = templist;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded)
        {
            transform.Rotate(0, 0, transform.localScale.x / Mathf.Abs(transform.localScale.x) *-1, Space.Self);
            rb.velocity = new Vector3(transform.localScale.x/ Mathf.Abs(transform.localScale.x) * speed, 0, 0);
        }
        else
        {
            rb.velocity = new Vector3(transform.localScale.x / Mathf.Abs(transform.localScale.x) * 7, rb.velocity.y, 0);
        }
        if (disappear)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, GetComponent<SpriteRenderer>().color.a - 0.05f);
            if (GetComponent<SpriteRenderer>().color.a <= 0.05f)
            {
                Destroy(gameObject);
            }
        }
        if (pulseUp)
        {
            lightt.intensity = Mathf.Clamp(lightt.intensity += pulseInterval/100, 0.3f, 0.75f);
        }
        else
        {
            lightt.intensity = Mathf.Clamp(lightt.intensity -= pulseInterval/100, 0.3f, 0.75f);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !impacted)
        {
            StartCoroutine(Pulse());
            isGrounded = true;
            ImpactDamage(collision.gameObject);
            impacted = true;
            anim.SetTrigger("GroundHit");
        }
        if (!isGrounded && collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("hit");
            List<DamageType> hitdmg = new List<DamageType>();
            foreach (DamageType dType in damages)
            {
                DamageType temp = new DamageType
                {
                    damageElement = dType.damageElement,
                    value = dType.value*5
                };
                hitdmg.Add(temp);
            }
            collision.gameObject.GetComponent<EnemyController>().CalculateDamage(hitdmg, false, 0);
            Physics2D.IgnoreCollision(collision.collider, this.GetComponent<Collider2D>());
        };
    }
    private IEnumerator Live()
    {
        yield return new WaitForSeconds(livetime);
        disappear = true;
    }
    private IEnumerator Pulse()
    {
        yield return new WaitForSeconds(pulseInterval / 2);
        pulseUp = !pulseUp;
        if (pulseUp)
        {
            tickDamage();
            Debug.Log("pulsed");
        }
        StartCoroutine(Pulse());
    }
    private void tickDamage()
    {
        float temp = 10;
        foreach (DamageType dtype in damages)
        {
            if (dtype.damageElement == DamageElement.Fire)
            {
                temp += dtype.value;
            }
        }
        foreach(Collider2D col in Physics2D.OverlapCircleAll(transform.position, transform.localScale.y * 0.75f))
        {
            if (col.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("enemy!");
                col.gameObject.GetComponent<EnemyController>().CalculateDamage(damages, false, 0);
                if (isSun)
                {
                    col.gameObject.GetComponent<EnemyController>().CritEffect((int)temp, burn);
                }
            }
        }
    }
    private void ImpactDamage(GameObject ground)
    {
        List<DamageType> tripleType = new List<DamageType>();
        foreach (DamageType dType in damages)
        {
            DamageType temp = new DamageType
            {
                value = dType.value * 3,
                damageElement = dType.damageElement
            };
            tripleType.Add(temp);
        };
        //foreach (GameObject go in ground.GetComponent<Platform>().enemies)
        //{
        //    go.GetComponent<EnemyController>().CalculateDamage(tripleType, false, 0);
        //}
    }
}
