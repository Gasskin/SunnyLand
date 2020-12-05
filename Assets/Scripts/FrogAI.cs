using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogAI : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform LeftTrans, RightTrans;
    public float speedx = 5;
    public float speedy = 5;

    float left, right;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        left = LeftTrans.position.x;
        right = RightTrans.position.x;
        Destroy(LeftTrans.gameObject);
        Destroy(RightTrans.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (transform.localScale.x > 0)
        {
            rb.velocity = new Vector2(-speedx, rb.velocity.y);
            if (transform.position.x < left)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        else
        {
            rb.velocity = new Vector2(speedx, rb.velocity.y);
            if (transform.position.x > right)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
}
