using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogAI : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform LeftTrans, RightTrans;
    public float speedx = 5;
    public float speedy = 8;
    public LayerMask ground;

    float left, right;

    private Animator anim;
    private Collider2D collider;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();

        left = LeftTrans.position.x;
        right = RightTrans.position.x;
        Destroy(LeftTrans.gameObject);
        Destroy(RightTrans.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        SwitchAnim();
    }

    void Move()
    {
        if (transform.localScale.x > 0)
        {
            if (collider.IsTouchingLayers(ground))
            {
                rb.velocity = new Vector2(-speedx, speedy);
                anim.SetBool("jumping", true);
            }
        }
        else
        {
            if (collider.IsTouchingLayers(ground))
            {
                rb.velocity = new Vector2(speedx, speedy);
                anim.SetBool("jumping", true);
            }
        }
    }

    void ChangeFaceTo()
    {
        if (transform.position.x < left)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if(transform.position.x > right)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void SwitchAnim()
    {
        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0.1)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }
        if (collider.IsTouchingLayers(ground) && anim.GetBool("falling")) 
        {
            anim.SetBool("falling", false);
        }
    }

    public void DeathAnim()
    {
        anim.SetTrigger("death");
        collider.enabled = false;
        Destroy(rb);
    }
    public void Death()
    {
        Destroy(gameObject);
    }
}
