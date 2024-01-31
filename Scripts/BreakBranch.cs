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
        rb.gravityScale = 1;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.AddForce(new Vector3(1f, 0f, 0f));
        StartCoroutine(dissapear());
    }

    public IEnumerator dissapear()
    {
        yield return new WaitForSeconds(2.0f);
        isBreaking = false;
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
            BreakBranch.isBreaking = true;
        }
    }

}
