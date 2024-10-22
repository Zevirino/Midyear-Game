using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float rotationVar;
    private float aimPointY;
    public float minY;
    public float minX;

    private void Start()
    {
        
    }
    public void onCreation(float aimPointY)
    {
        this.aimPointY = aimPointY;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f, 0f, rotationVar));
        transform.Translate(new Vector3(-0.03f, (aimPointY - transform.position.y) * Time.deltaTime / 10, 0f), Space.World);
        if (transform.position.y < minY || transform.position.x < minX)
        {
            Destroy(gameObject);
        }
    }
}
