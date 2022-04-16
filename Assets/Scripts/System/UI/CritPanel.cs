using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritPanel : MonoBehaviour
{
    public bool isFront;
    public SpriteRenderer spren;
    void Start()
    {
        spren = this.GetComponent<SpriteRenderer>();
        if (transform.position.z > 0)
        {
            isFront = false;
        }
        else
        {
            isFront = true;
        }
    }
    void Update()
    {
        if (transform.position.z > 0 && isFront)
        {
            isFront = false;
            spren.sortingOrder = -10;
        }else if(transform.position.z < 0 && !isFront)
        {
            isFront = true;
            spren.sortingOrder = 10;
        }
    }
}
