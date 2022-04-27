using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParticle : MonoBehaviour
{
    public PlayerControl pc;
    public int damage;
    public bool doBlockHit;
    public bool disappear = false, appear = false;
    private SpriteRenderer spren;
    public GameObject rangeChecker;

    private void Start()
    {
        spren = GetComponent<SpriteRenderer>();
        SetUp();
        if (GetComponent<Animator>())
        {
            StartCoroutine(EndTime());
        }
    }
    public virtual void SetUp()
    {

    }
    private void Update()
    {
        if (disappear)
        {
            spren.color = new Color(1, 1, 1, spren.color.a - 0.02f);
            if (spren.color.a <= 0.05f)
            {
                Destroy(gameObject);
            }
        }
        if (appear)
        {
            spren.color = new Color(1, 1, 1, spren.color.a + 0.02f);
            if (spren.color.a >= 0.6f)
            {
                appear = false;
            }
        }
    }
    public void TriggerDamage()
    {
        if (rangeChecker.GetComponent<EnemyAttackRange>().avail)
        {
            pc.ph.OnPlayerHit(transform.position, damage);
        }
    }
    public void OnAnimFinished()
    {
        Destroy(gameObject);
    }
    public void AnimFinishedStartToningDown()
    {
        disappear = true;
    }
    private IEnumerator EndTime()
    {
        yield return new WaitForSeconds(5);
        damage = 0;
        GetComponent<Animator>().SetTrigger("End");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (doBlockHit && collision.CompareTag("Projectile"))
        {
            collision.GetComponent<Projectile>().OnHit();
        }
    }
}
