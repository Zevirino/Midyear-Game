using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeOnAttack : MonoBehaviour
{
    private int direction = 1;
    public float shakeSpeed = 0.01f;
    public float shakeLength = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Weapon"))
        {
            StartCoroutine(shakeAnimation(col.gameObject.transform.position.x-transform.position.x));
        }
    }

    public IEnumerator shakeAnimation(float posDifference)
    {
        if (posDifference < 0)
        {
            direction = -1;
        }
        for (int i=0; i<5; i++)
        {
            yield return new WaitForSeconds(shakeSpeed);
            transform.Translate(new Vector3(direction * shakeLength, 0f, 0f));
        }
        for (int i=0; i < 10; i++)
        {
            yield return new WaitForSeconds(shakeSpeed);
            transform.Translate(new Vector3(-1 * direction * shakeLength, 0f, 0f));
        }
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(shakeSpeed);
            transform.Translate(new Vector3(direction * shakeLength, 0f, 0f));
        }

    }
}
