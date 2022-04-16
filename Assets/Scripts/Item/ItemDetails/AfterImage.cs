using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImage : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public float destroyTime;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if(Time.time >= destroyTime)
        {
            Destroy(this.gameObject);
        }
    }
}
