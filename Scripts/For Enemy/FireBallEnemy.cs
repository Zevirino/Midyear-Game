using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallEnemy : MonoBehaviour
{
    public float fireBallCooldown;
    public int direction;
    public GameObject fireBall;

    public float minY = -10;
    private Vector2 ogPos;

    private float rotation;
    // Start is called before the first frame update
    void Start()
    {
        ogPos = transform.position;
        if (direction == -1)
        {
            rotation = -90f;
        }
        else if (direction == 1)
        {
            rotation = 90f;
        }
        StartCoroutine(spawnFireball());
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < minY)
        {
            transform.position = ogPos;
        }
    }

    public IEnumerator spawnFireball()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireBallCooldown);
            GameObject go = Instantiate(fireBall, new Vector2(transform.position.x + direction,transform.position.y+0.8f), Quaternion.Euler(0f,0f,rotation));
            if (direction == -1)
            {
                go.GetComponent<FireBallScript>().speed *= -1;
            }
        }
    }
}