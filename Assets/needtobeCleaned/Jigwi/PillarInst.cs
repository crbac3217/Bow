using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarInst : MonoBehaviour
{
    private bool destroy = false, avail = false;
    private SpriteRenderer spren;
    public int damage;
    private PlayerControl pc;

    private void Start()
    {
        spren = GetComponent<SpriteRenderer>();
        StartCoroutine(PillarExp());
    }
    public void TriggerDamage()
    {
        if (avail && pc)
        {
            pc.ph.OnPlayerHit(transform.position, damage);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            avail = true;
            pc = collision.GetComponent<PlayerControl>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            avail = false;
        }
    }
    private IEnumerator PillarExp()
    {
        yield return new WaitForSeconds(5);
        damage = 0;
        GetComponent<Animator>().SetTrigger("Trigger");
        transform.position = new Vector2(transform.position.x, transform.position.y + 0.3f);
    }
    public void AnimFinished()
    {
        destroy = true;
    }
    private void Update()
    {
        if (destroy)
        {
            spren.color = new Color(1, 1, 1, spren.color.a - 0.02f);
            if (spren.color.a <= 0.02f)
            {
                Destroy(gameObject);
            }
        }
    }
}
