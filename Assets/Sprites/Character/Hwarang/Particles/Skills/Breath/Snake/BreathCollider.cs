using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathCollider : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemies.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemies.Remove(collision.gameObject);
        }
    }
}
