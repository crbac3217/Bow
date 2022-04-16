using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezingAura : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyProjectile"))
        {
            collision.GetComponent<EnemyProjectile>().speed *= 0.5f;
        }
    }
}
