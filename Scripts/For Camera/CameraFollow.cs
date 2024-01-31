using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public bool canFollowYAxis = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.gameFreeze)
        {
<<<<<<< Updated upstream
            transform.Translate(new Vector3((player.transform.position.x - transform.position.x)*Time.deltaTime, 0f,0f));
            //Camera only follows player vertically if they are above a certain point
            if (player.transform.position.y >= player.GetComponent<PlayerScript>().ogPos.y + 1f && player.GetComponent<PlayerScript>().canJump)
            {
                transform.Translate(new Vector3(0f, (player.transform.position.y - transform.position.y) * Time.deltaTime, 0f));
=======
            if (canFollowYAxis)
            {
                transform.Translate(new Vector3((player.transform.position.x - transform.position.x) * Time.deltaTime, (player.transform.position.y - transform.position.y) * Time.deltaTime, 0));
            }
            else
            {
                transform.Translate(new Vector3((player.transform.position.x - transform.position.x) * Time.deltaTime, 0, 0));
>>>>>>> Stashed changes
            }
        }
    }
}
