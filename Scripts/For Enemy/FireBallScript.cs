using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallScript : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(destroyAfterTime());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x + speed, transform.position.y);
    }

    public IEnumerator destroyAfterTime()
    {
        yield return new WaitForSeconds(6.0f);
        Destroy(gameObject);
    }
}