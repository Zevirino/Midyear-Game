using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //movement variables
    private float horizontalInput;
    public float speed;
    private float verticalInput;
    public float gravityWeight = 1.0f;
    public bool canJump;
    public float jumpHeight = 10.0f;

    //Components
    private Rigidbody2D rb;
    private Animator anim;

    private bool using2d;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        using2d = true;
        canJump = true;
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
        if (!using2d)
        {
            rb.velocity = new Vector2(rb.velocity.x, verticalInput * speed);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //horizontal movement
        horizontalInput = Input.GetAxis("Horizontal");

        //Animations
        if (!canJump)
        {
            anim.Play("Jump");
        }
        else if (horizontalInput != 0)
        {
            anim.Play("Run");
        }
        else
        {
            anim.Play("Idle");
        }

        //check for switch between 2D and 2.5D
        if (Input.GetKeyDown(KeyCode.Space))
        {
            using2d = using2d==true?false:true;
        }

        //Movement for 2D
        if (using2d)
        {
            rb.gravityScale = gravityWeight;
            //jump
            if (canJump && Input.GetKeyDown(KeyCode.UpArrow))
            {
                rb.gravityScale = 1;
                rb.AddForce(new Vector2(rb.velocity.x, jumpHeight),ForceMode2D.Impulse);
                canJump = false;
            }
        }
        //Movement for 2.5D
        else
        {
            verticalInput = Input.GetAxis("Vertical");
            rb.gravityScale = 0;
        }
    }

    public void onCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
            rb.gravityScale=0;
        }
    }

    public float getHorizontalInput()
    {
        return horizontalInput;
    }
}
