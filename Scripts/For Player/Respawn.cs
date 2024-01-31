using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    private Vector2 respawnPoint;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    public float minY = -10.0f;
    // Start is called before the first frame update
    void Start()
    {
        respawnPoint = transform.position;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < minY)
        {
            StartCoroutine(die(0f));
        }
    }

    public IEnumerator die(float timeDelay)
    {
        if (timeDelay != 0f)
        {
            sr.enabled = false;
        }
        yield return new WaitForSeconds(timeDelay);
        transform.position = respawnPoint;
        rb.velocity = Vector2.zero;
        sr.enabled = true;
    }
}
