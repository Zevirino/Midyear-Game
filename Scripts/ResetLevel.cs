using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


//Currently, this program only resets all boxes and the player
public class ResetLevel : MonoBehaviour
{
    private int levelIndex;
    private Dictionary<GameObject, Vector3> posStor = new Dictionary<GameObject, Vector3>();
    private GameObject[] boxes;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        boxes = GameObject.FindGameObjectsWithTag("Box");
        player = GameObject.FindGameObjectWithTag("Player");

        //Storing the original position of all respawnable objects in a dictionary
        foreach(GameObject box in boxes) 
        {
            posStor.Add(box, new Vector3(box.transform.position.x, box.transform.position.y, box.transform.position.z)); 
        }
        posStor.Add(player, new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            player.GetComponent<PlayerScript>().using2d = true;
            foreach (GameObject respawnable in posStor.Keys) 
            {
                respawnable.transform.position = posStor[respawnable];
                if (!respawnable.CompareTag("Player"))
                {
                    respawnable.GetComponent<DimensionGravitySwitch>().using2d = true;
                }
            }
        }
    }
}
