using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    public LayerMask ground;
    public Collider2D collider;
    public Text CherryNum;
    public AudioSource audioJump;
    public AudioSource audioCollect;
    public AudioSource audioHurt;

    public float MaxSpeed = 5.0f;
    public float JumpForce = 10.0f;
    public float HurtForce = 5.0f;

    bool canJump = true;
    int CollectionNums = 0;
    bool isHurt = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isHurt)
        {
            Move();
            Crouch();
            Jump();
        }
        if (Mathf.Abs(rb.velocity.x) < 0.1)
        {
            isHurt = false;
            anim.SetBool("hurt", false);
        }
    }

    void Move()
    {
        //移动
        float horizontal = Input.GetAxis("Horizontal");
        if (horizontal != 0)
        {
            anim.SetBool("running", true);
            rb.velocity = new Vector2(horizontal * MaxSpeed, rb.velocity.y);
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
    }

    void Crouch()
    {
        //蹲伏
        if (Input.GetButtonDown("Crouch"))
        {
            MaxSpeed /= 2.0f;
            anim.SetBool("crouching", true);
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            MaxSpeed *= 2.0f;
            anim.SetBool("crouching", false);
        }
    }

    void Jump()
    {
        if (rb.velocity.y < 0.1 && !collider.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", true);
        }
        //下蹲时不可以跳跃
        if (!anim.GetBool("crouching"))
        {
            //跳跃
            if (Input.GetButtonDown("Jump") && canJump)
            {
                anim.SetBool("jumping", true);
                anim.SetBool("falling", false);
                audioJump.Play();
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
            audioCollect.Play();
            CherryNum.text = CollectionNums.ToString();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (anim.GetBool("falling"))
            {
                FrogAI frog = collision.gameObject.GetComponent<FrogAI>();
                frog.DeathAnim();
            }
            else
            {
                isHurt = true;
                anim.SetBool("hurt", true);
                audioHurt.Play();
                if (transform.position.x < collision.gameObject.transform.position.x)
                {
                    rb.velocity = new Vector2(-HurtForce, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(HurtForce, rb.velocity.y);
                }
            }
        }
    }
}   
