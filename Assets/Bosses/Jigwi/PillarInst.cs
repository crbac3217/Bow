using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarInst : MonoBehaviour
{
    private bool destroy = false, avail = false;
    private SpriteRenderer spren;
    public int damage;
    private PlayerControl pc;
    private AudioSource audioS;
    public AudioClip prep, exp;

    private void Start()
    {
        spren = GetComponent<SpriteRenderer>();
        StartCoroutine(PillarExp());
        audioS = GetComponent<AudioSource>();
        audioS.loop = true;
        audioS.clip = prep;
        audioS.Play();
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
        audioS.loop = false;
        audioS.clip = exp;
        audioS.Play();
        damage = 0;
        GetComponent<Animator>().SetTrigger("Trigger");
        transform.position = new Vector2(transform.position.x, transform.position.y + 0.3f);
    }
    public void AnimFinished()
    {
        destroy = true;
    }
    private void FixedUpdate()
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
