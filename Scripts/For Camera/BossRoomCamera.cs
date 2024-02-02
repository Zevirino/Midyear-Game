using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomCamera : MonoBehaviour
{

    public GameObject camera;
    private Camera bossCamera;
    public Vector3 position;
    public Vector3 scale;
    public float size;
    public static bool on = false;
    // Start is called before the first frame update
    void Start()
    {
       bossCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (on)
        {
            bossCamera.enabled = true;
            camera.SetActive(false);
            transform.position = position;
            transform.localScale = scale;
            gameObject.GetComponent<Camera>().orthographicSize = size;
            CameraFollow.bossFight = true;
        }
        else
        {
            bossCamera.enabled = false;
            camera.SetActive(true);
            CameraFollow.bossFight = false;
        }
    }
}
