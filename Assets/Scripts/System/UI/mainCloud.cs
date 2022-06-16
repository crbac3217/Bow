using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainCloud : MonoBehaviour
{
    public float startpos, endpos, rate;
    void Start()
    {
        transform.position = new Vector2(startpos, transform.position.y) ;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x + rate, transform.position.y);
        if (transform.position.x <= endpos)
        {
            transform.position = new Vector2(startpos, transform.position.y);
        }
    }
}
