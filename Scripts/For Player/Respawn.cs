using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    private Vector2 respawnPoint;
    private Rigidbody2D rb;
    public float minY = -10.0f;
    // Start is called before the first frame update
    void Start()
    {
        respawnPoint = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < minY)
        {
            transform.position = respawnPoint;
            rb.velocity = Vector2.zero;
        }
    }
}
