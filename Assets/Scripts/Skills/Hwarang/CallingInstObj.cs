using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallingInstObj : MonoBehaviour
{
    public List<DamageType> damages = new List<DamageType>();
    public Crit effect;
    public float range = 2;
    public AudioClip spawn, impact;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = spawn;
        audioSource.Play();
    }
    public void OnOrbBreak()
    {
        audioSource.clip = impact;
        audioSource.Play();
        foreach (Collider2D col in Physics2D.OverlapCircleAll(transform.position, range))
        {
            if (col.CompareTag("Enemy"))
            {
                col.GetComponent<EnemyController>().CalculateDamage(damages, false, 0);
                if (col)
                {
                    col.GetComponent<EnemyController>().CritEffect(1, effect);
                }
                if (col.CompareTag("EnemyProjectile"))
                {
                    col.GetComponent<EnemyProjectile>().Dest(null);
                }
            }
        }
    }
    public void OnAnimEnd()
    {
        Destroy(this.gameObject);
    }
}
