using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //movement variables
    private float horizontalInput;
    public float speed;
    public bool canJump;
    public float jumpHeight = 10.0f;

    private Rigidbody2D rb;
    private bool using2d;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        using2d = true;

        canJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (using2d)
        {
            //horizontal movement
            horizontalInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
            //jump
            if (canJump && Input.GetKeyDown(KeyCode.UpArrow))
            {
                rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
                canJump = false;
            }
        }
    }

    public void onCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("2dGround"))
        {
            canJump = true;
        }
    }
}
