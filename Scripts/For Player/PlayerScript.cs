using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public GameObject shadow;

    //Other Variables
    public bool using2d;
    public float minY = -10.0f;
    private bool deathByBranch;
    private bool doorEntry;

    //Other objects
    public GameObject weapon;
    private WeaponScript weaponScript;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        weaponScript = weapon.GetComponent<WeaponScript>();

        using2d = true;
        canJump = true;
        doorEntry = false;
        deathByBranch = false;

        // Set initial position
        transform.position = new Vector3(-3f, 0f, 0f);
    }

    void FixedUpdate()
    {
        if (!GameManager.gameFreeze && !deathByBranch && !doorEntry)
        {
            rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
            if (!using2d)
            {
                rb.velocity = new Vector2(rb.velocity.x, verticalInput * speed);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.gameFreeze && !deathByBranch && !doorEntry)
        {
            //Check for death
            if (transform.position.y < minY)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            //horizontal movement
            horizontalInput = Input.GetAxis("Horizontal");

            //Animations
            if (!canJump)
            {
                anim.Play("Jump");
            }
            else if (horizontalInput != 0 || (verticalInput!=0 && !using2d))
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
                if (!using2d)
                {
                    anim.Play("Idle");
                }
            }

            //Things for 2D
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
                shadow.SetActive(false);
                
            }
            //Movement for 2.5D
            else
            {
                verticalInput = Input.GetAxis("Vertical");
                rb.gravityScale = 0;
                shadow.SetActive(true);
            }
        }
        else if (!deathByBranch && !doorEntry)
        {
            anim.Play("Idle");
        }
    }
    public Text collisionText; // Reference to the Text UI element for collision messages

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && transform.position.y>collision.gameObject.transform.position.y)
        {
            canJump = true;
            rb.gravityScale=0;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Collision handling between the player and an enemy

            Debug.Log("Two characters have collided!");
            // Additional processing can be added here
        }
    }
        
    

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Laser"))
        {
            Debug.Log("hi");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (col.gameObject.CompareTag("Branch"))
        {
            transform.position = new Vector2(col.gameObject.transform.position.x, transform.position.y);
            rb.velocity = new Vector2(0, 0);
            deathByBranch = true;
            StartCoroutine((GetComponent<BranchDeath>()).deathAnimation(col.gameObject.transform.position));
        }
        if (col.gameObject.CompareTag("Door"))
        {
            doorEntry = true;
            StartCoroutine((GetComponent<DoorEntry>()).doorAnimation());
            StartCoroutine((GetComponent<DoorEntry>()).fadeAnimation());
        }
    }

    public float getHorizontalInput()
    {
        return horizontalInput;
    }
}