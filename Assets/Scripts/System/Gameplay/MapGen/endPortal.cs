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
#if UNITY_ANDROID
            collision.GetComponent<PlayerShoot>().fixedJoystick.CancelShooting();
#elif UNITY_STANDALONE_WIN
            collision.GetComponent<PlayerShoot>().dynamicJoystick.CancelShooting();
#endif
            collision.GetComponent<PlayerMove>().LetGoLeft();
            collision.GetComponent<PlayerMove>().LetGoRight();
            lm.gm.LoadNextLevel();
        }
    }
}
