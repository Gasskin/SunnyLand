using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    public LayerMask ground;
    public Collider2D collider;
    public Text CherryNum;

    public float MaxSpeed = 5.0f;
    public float JumpForce = 10.0f;

    bool canJump = true;
    int CollectionNums = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //移动
        float horizontal = Input.GetAxis("Horizontal");
        if ( horizontal!= 0) 
        {
            anim.SetBool("running", true);
            rb.velocity = new Vector2(horizontal*MaxSpeed, rb.velocity.y);
            //面向
            float direction = Input.GetAxisRaw("Horizontal");
            if (direction != 0) 
            {
                transform.localScale = new Vector3(direction, 1.0f, 1.0f);
            }
        }
        else
        {
            anim.SetBool("running", false);
        }


        //蹲伏
        if (Input.GetButtonDown("Crouch"))
        {
            anim.SetBool("crouching", true);
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            anim.SetBool("crouching", false);
        }

        //下蹲时不可以跳跃
        if (!anim.GetBool("crouching"))
        {
            //跳跃
            if (Input.GetButtonDown("Jump") && canJump)
            {
                anim.SetBool("jumping", true);
                rb.velocity = new Vector2(rb.velocity.x, JumpForce);
                canJump = false;
            }
            //跳跃、下落动画切换
            if (anim.GetBool("jumping"))
            {
                if (rb.velocity.y < 0.0f)
                {
                    anim.SetBool("falling", true);
                    anim.SetBool("jumping", false);
                }
            }
            else
            {
                if (collider.IsTouchingLayers(ground))
                {
                    anim.SetBool("falling", false);
                    anim.SetBool("jumping", false);
                    canJump = true;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collection")
        {
            CollectionNums++;
            Destroy(collision.gameObject);
            CherryNum.text = CollectionNums.ToString();
        }
    }
}   
