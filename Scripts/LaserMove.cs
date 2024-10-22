using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMove : MonoBehaviour
{
    public bool isMovable;
    public GameObject laserBottom = null;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isMovable) {
            float ySize = laserBottom.GetComponent<BoxCollider2D>().size.y;
            transform.position = new Vector3(laserBottom.transform.position.x, (laserBottom.transform.position.y - (ySize / 2.0f)) + (((GetComponent<BoxCollider2D>().size.y * transform.localScale.y) + ySize) / 2.0f), laserBottom.transform.position.z);
            laserBottom.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
    }
}
