using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript : MonoBehaviour
{
    private bool playerOnTop = false;
    private BoxCollider2D boxCollider;
    private bool using2D = true;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            using2D = using2D==true?false:true;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("hello");
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.transform.position.y < transform.position.y && using2D)
            {
                //Makes the ground isTrigger so player can go through
                boxCollider.isTrigger = true;
            }
        }
    }

    public void onTriggerExit2D(Collider2D col)
    {
        Debug.Log("THIs is mesage");
        if (col.gameObject.CompareTag("Player"))
        {
            boxCollider.isTrigger = false;
        }
    }
}
