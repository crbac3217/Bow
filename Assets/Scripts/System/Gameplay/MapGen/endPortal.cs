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
            collision.GetComponent<PlayerShoot>().fixedJoystick.CancelShooting();
            collision.GetComponent<PlayerMove>().LetGoLeft();
            collision.GetComponent<PlayerMove>().LetGoRight();
            lm.gm.LoadNextLevel();
        }
    }
}
