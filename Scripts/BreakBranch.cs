using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBranch : MonoBehaviour
{
    public static bool isBreaking = false;
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        bc.isTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isBreaking)
        {
            breakEffect();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bc.isTrigger = bc.isTrigger==true?false:true;
        }

    }

    private void breakEffect()
    {
        isBreaking = false;
        rb.gravityScale = 1;
        rb.constraints = RigidbodyConstraints2D.None;
        StartCoroutine(dissapear());
    }

    public IEnumerator dissapear()
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }

}
