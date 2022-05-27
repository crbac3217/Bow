using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveTowards : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 p1, p2;
    public float speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (p1 != null && p2 != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, p2, speed*Time.deltaTime);
        }
    }
}
