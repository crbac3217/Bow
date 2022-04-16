using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
    public GameObject particle;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerControl>().GainGold(1);
            var inst = Instantiate(particle, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
