using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    public int level;
    private bool avail = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && avail)
        {
            collision.GetComponent<PlayerControl>().guiManager.ShopOn(level);
            avail = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            avail = true;
        }
    }
}
