using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionGravitySwitch : MonoBehaviour
{
    public bool using2d;
    private Rigidbody2D rb;
    public float gravityWeight = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        using2d = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            using2d = using2d==true?false:true;
        }
        if (using2d)
        {
            rb.gravityScale = gravityWeight;
        }
        else
        {
            rb.gravityScale = 0;
        }
    }
}
