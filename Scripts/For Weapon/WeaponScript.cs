using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    private SpriteRenderer sr;
    private PolygonCollider2D polyCol;

    public static float speed = 5f;
    public float range = 180f;
    private Quaternion startingRotation;
    public float attackSpeed = 0.01f;

    public static bool isFlipped;
    public static bool attackBool = false;
    private bool using2D;
    // Start is called before the first frame update
    void Start()
    {
        startingRotation = transform.rotation;
        using2D = true;
        isFlipped = false;

        sr = GetComponent<SpriteRenderer>();
        polyCol = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            using2D = using2D==true?false:true;
        }
        if (Input.GetKeyDown(KeyCode.Z) && using2D && !attackBool)
        {
            transform.rotation = startingRotation;
            //disable collider and sprite renderer
            sr.enabled = true;
            polyCol.enabled = true;

            attackBool = true;
            StartCoroutine(attackAnimation());
        }
    }

    public IEnumerator attackAnimation()
    {
        if (!isFlipped)
        {
            if (transform.eulerAngles.z < startingRotation.eulerAngles.z + range && transform.eulerAngles.z > startingRotation.eulerAngles.z + range - 360f)
            {
                yield return new WaitForSeconds(attackSpeed);
                transform.Rotate(0f, 0f, speed);
                StartCoroutine(attackAnimation());
                yield break;
            }
        }
        else
        {
            if (transform.eulerAngles.z > startingRotation.eulerAngles.z - range && transform.eulerAngles.z < startingRotation.eulerAngles.z - range + 360f)
            {
                yield return new WaitForSeconds(attackSpeed);
                transform.Rotate(0f, 0f, -1*speed);
                StartCoroutine(attackAnimation());
                yield break;
            }
        }
        sr.enabled = false;
        polyCol.enabled = false;
        attackBool = false;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (attackBool && collision.gameObject.CompareTag("Branch"))
        {
            BreakBranch.isBreaking=true;
        }
        
    }
}
