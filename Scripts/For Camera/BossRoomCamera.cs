using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomCamera : MonoBehaviour
{
    public Vector2 position;
    public Vector3 scale;
    public float size;
    public static bool on = false;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (on)
        {
            Debug.Log("why hello there");
            transform.position = position;
            transform.localScale = scale;
            gameObject.GetComponent<Camera>().orthographicSize = size;
            CameraFollow.bossFight = true;
        }
    }
}
