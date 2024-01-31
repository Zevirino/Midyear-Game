using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float rotationVar;
    public float aimPointY;

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
        transform.Translate(new Vector3(-1f, (aimPointY - transform.position.y)*Time.deltaTime, 0f));
    }
}
