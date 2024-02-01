using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBottomShaper : MonoBehaviour
{
    public GameObject laser;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float ySize = laser.transform.localScale.y * 3.6f;
        transform.position = new Vector3(laser.transform.position.x, laser.transform.position.y - (ySize/2.0f) + (GetComponent<BoxCollider2D>().size.y/2.0f), laser.transform.position.z);
    }
}
