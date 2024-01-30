using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.gameFreeze)
        {
            transform.Translate(new Vector3((player.transform.position.x - transform.position.x)*Time.deltaTime, 0,0));
        }
    }
}
