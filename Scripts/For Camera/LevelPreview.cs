using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPreview : MonoBehaviour
{
    public float distance;
    public float distanceIncrement;
    public float panTime = 0.01f;
    public float panBackTime = 0.01f;
    private Vector2 ogPos;
    // Start is called before the first frame update
    void Start()
    {
        ogPos = transform.position;
        StartCoroutine(panAnimation());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator panAnimation()
    {
        GameManager.gameFreeze = true;
        while (transform.position.x < ogPos.x + distance)
        {
            yield return new WaitForSeconds(panTime);
            transform.position = new Vector2(transform.position.x + distanceIncrement, transform.position.y);
        }
        while (transform.position.x > ogPos.x)
        {
            yield return new WaitForSeconds(panBackTime);
            transform.position = new Vector2(transform.position.x - distanceIncrement, transform.position.y);
        }
        yield return new WaitForSeconds(0.5f);
        GameManager.gameFreeze = false;
    }
}
