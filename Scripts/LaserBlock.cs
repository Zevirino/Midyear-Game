using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LaserBlock : MonoBehaviour
{
    Vector2 origPos; 
    // Start is called before the first frame update
    void Start()
    {
        origPos = new Vector2(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerStay2D(Collider2D collider) {
        if (collider.gameObject.CompareTag("Box")) {
            float y = origPos.y;
            float scale = GetComponent<BoxCollider2D>().size.x;
            float topEdge = collider.transform.position.y - collider.GetComponent<BoxCollider2D>().size.y;
            transform.position = new Vector2(transform.position.x, y - (y + scale - topEdge));
        }
    }

    public void OnTriggerExit2D(Collider2D collider) {
        if (collider.gameObject.CompareTag("Box")) {
            transform.position = origPos;
        }
    }
}
