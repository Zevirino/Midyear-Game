using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LaserBlock : MonoBehaviour
{
    public bool isHorizontal = false;
    Vector3 origPos;
    Vector3 origScale;
    public GameObject laserBottom = null;
    private Vector3 origLaserBottomPos;
    private Vector3 nextLaserBottomPos;
    // Start is called before the first frame update
    void Start()
    {
        origScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        if (isHorizontal || !(GetComponent<LaserMove>().isMovable)) 
        {
            origPos = transform.position;
        }
        else
        {
            origLaserBottomPos = laserBottom.transform.position;
            nextLaserBottomPos = laserBottom.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if (!isHorizontal)
        {
            if (nextLaserBottomPos != origLaserBottomPos)
            {
                origPos = new Vector3(transform.position.x, transform.position.y, 0f);
                origLaserBottomPos = nextLaserBottomPos;
            }
            nextLaserBottomPos = laserBottom.transform.position;
        }
        if (BossScript.gameOver)
        {
            Destroy(laserBottom);
            Destroy(gameObject);
        }
    }

    public void OnTriggerStay2D(Collider2D collider) {
        if (collider.gameObject.CompareTag("Box")) {
            /*
            float y = origPos.y;
            float scale = GetComponent<BoxCollider2D>().size.x;
            float topEdge = collider.transform.position.y - collider.GetComponent<BoxCollider2D>().size.y/2 + 0.7f;
            transform.position = new Vector2(transform.position.x, y - (y + scale - topEdge));
            */
            if (!isHorizontal)
            {
                float bottomEdge = transform.position.y - ((laserBottom.GetComponent<BoxCollider2D>().size.y + (transform.localScale.y * GetComponent<BoxCollider2D>().size.y)) / 2.0f);
                float topEdge = collider.transform.position.y - (collider.GetComponent<BoxCollider2D>().size.y * collider.transform.localScale.y) / 2 + 0.7f;
                transform.position = new Vector3(transform.position.x, (bottomEdge + topEdge) / 2.0f, transform.position.z);
                float yScale = Math.Abs(topEdge - bottomEdge) / Math.Abs(GetComponent<BoxCollider2D>().size.y * transform.localScale.y + laserBottom.GetComponent<BoxCollider2D>().size.y);
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * yScale, transform.localScale.z);
            }
            else
            {
                float rightEdge = transform.position.x + (transform.localScale.y * GetComponent<BoxCollider2D>().size.y) / 2.0f;
                float leftEdge = collider.transform.position.x + (collider.GetComponent<BoxCollider2D>().size.x * collider.transform.localScale.x) / 2 - 0.6f;
                transform.position = new Vector3((rightEdge + leftEdge) / 2.0f, transform.position.y, transform.position.z);
                float yScale = Math.Abs(leftEdge - rightEdge) / (GetComponent<BoxCollider2D>().size.y * transform.localScale.y);
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * yScale, transform.localScale.z);
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collider) {
        if (collider.gameObject.CompareTag("Box")) {
            transform.position = new Vector3(origPos.x, origPos.y, origPos.z);
            transform.localScale = origScale;
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("GrossEnemy"))
        {
            collision.gameObject.GetComponent<Gross>().takeDamage(2.5f);
        }
    }
}
