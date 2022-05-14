using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    public GameObject coin;
    public GameObject[] chests = new GameObject[] { };
    public void DropGold(int gold, Vector2 objPos)
    {
        for (int i = 0; i < gold; i++)
        {
            var goldcoin = Instantiate(coin, objPos + Vector2.up, Quaternion.identity);
            goldcoin.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1);
        }
    }
    public void DropChest(int Tier, Vector2 objPos)
    {
        var chest = Instantiate(chests[Tier - 1], objPos + Vector2.up, Quaternion.identity);
        chest.GetComponent<Rigidbody2D>().velocity = Vector2.up * 2;
        chest.GetComponent<Chest>().tier = Tier;
    }
}
