using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public int minY = 0;
    public static bool bossFight = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.gameFreeze && !bossFight)
        {
            transform.Translate(new Vector3((player.transform.position.x - transform.position.x)*Time.deltaTime, 0f,0f));
            //Camera only follows player vertically if they are above a certain point
            if (player.transform.position.y > minY && player.GetComponent<PlayerScript>().canJump)
            {
                transform.Translate(new Vector3(0f, (player.transform.position.y - transform.position.y) * Time.deltaTime, 0f));
            }
            else if (player.GetComponent<PlayerScript>().canJump)
            {
                transform.Translate(new Vector3(0f, -1 * transform.position.y * Time.deltaTime, 0f));
            }
        }
    }
}
