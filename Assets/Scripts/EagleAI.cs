using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleAI : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform topPos, bottomPos;

    float top, bottom;

    bool isUp = true;

    private Collider2D collider;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();

        top = topPos.position.y;
        bottom = bottomPos.position.y;

        Destroy(topPos.gameObject);
        Destroy(bottomPos.gameObject);
    }
 
    // Update is called once per frame
    void Update()
    {
        if (isUp)
        {
            rb.velocity = new Vector2(rb.velocity.x, 2);
            if (transform.position.y > top)
            {
                isUp = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, -2);
            if (transform.position.y < bottom)
            {
                isUp = true;
            }
        }

    }
}
