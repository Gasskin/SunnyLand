using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    public float MaxSpeed = 5.0f;
    public float JumpForce = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        if ( horizontal!= 0) 
        {
            rb.velocity = new Vector2(horizontal*MaxSpeed, rb.velocity.y);
            anim.SetBool("running", true);
            if (Input.GetAxisRaw("Horizontal") != 0) 
            {
                transform.localScale = new Vector3(Input.GetAxisRaw("Horizontal"), 1.0f, 1.0f);
            }
        }
        else
        {
            anim.SetBool("running", false);
        }

        if (Input.GetAxisRaw("Vertical") > 0.0f) 
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
            anim.SetBool("jumping", true);
            anim.SetBool("falling", false);
        }
        if (rb.velocity.y < 0.0f)
        {
            anim.SetBool("falling", true);
            anim.SetBool("jumping", false);
        }
        if (rb.velocity.y == 0.0f)
        {
            anim.SetBool("falling", false);
            anim.SetBool("jumping", false);
        }
    }
}
