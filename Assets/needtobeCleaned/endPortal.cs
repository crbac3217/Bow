using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endPortal : MonoBehaviour
{
    public LevelManager lm;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            lm.gm.LoadNextLevel(collision.gameObject);
        }
    }
}
