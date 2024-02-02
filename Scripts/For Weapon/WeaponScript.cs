using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    private SpriteRenderer sr;
    private PolygonCollider2D polyCol;

    public static float speed = 2f;
    public float range = 180f;
    private Quaternion startingRotation;
    public float attackSpeed = 0.01f;

    public float weaponCooldown = 1f;
    private bool curCooldown = false;

    public static bool isFlipped;
    public static bool attackBool = false;
    private static bool z_depressed = false;
    private bool using2D;


    public GameObject smite_particles;
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
        
        if (Input.GetKeyUp(KeyCode.Z) && using2D && !attackBool && !curCooldown)
        {
            z_depressed = false;
            transform.rotation = startingRotation;
            //disable collider and sprite rendererz
            sr.enabled = true;
            polyCol.enabled = true;

            attackBool = true;
            StartCoroutine(attackAnimation());
        }
    }

    public IEnumerator attackAnimation()
    {
        float totalRotation = 0f;
        float rotationDirection = isFlipped ? -1 : 1; 
        print("isFlipped: " + isFlipped);
        while (totalRotation < range)
        {
            float rotationStep = speed * (Time.deltaTime / attackSpeed) * rotationDirection;
            transform.Rotate(0f, 0f, rotationStep); 
            totalRotation += Mathf.Abs(rotationStep);

            yield return null; 
        }
        Instantiate(smite_particles, transform.position, Quaternion.identity);
        
        sr.enabled = false;
        polyCol.enabled = false;
        attackBool = false;
        StartCoroutine(resetCooldown());
    }

    public IEnumerator resetCooldown()
    {
        curCooldown = true;
        yield return new WaitForSeconds(weaponCooldown);
        
        curCooldown = false;
    }
}

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class WeaponScript : MonoBehaviour
// {
//     private SpriteRenderer sr;
//     private PolygonCollider2D polyCol;

//     public static float speed = 0.5f;
//     public float range = 180f;
//     private Quaternion startingRotation;
//     public float attackSpeed = 0.01f;

//     public float weaponCooldown = 1f;
//     private bool curCooldown = false;

//     public static bool isFlipped;
//     public static bool attackBool = false;
//     private bool using2D;


//     public GameObject smite_particles;
//     // Start is called before the first frame update
//     void Start()
//     {
//         startingRotation = transform.rotation;
//         using2D = true;
//         isFlipped = false;

//         sr = GetComponent<SpriteRenderer>();
//         polyCol = GetComponent<PolygonCollider2D>();
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.Space))
//         {
//             using2D = using2D==true?false:true;
//         }
//         if (Input.GetKeyDown(KeyCode.Z) && using2D && !attackBool && !curCooldown)
//         {
//             transform.rotation = startingRotation;
//             //disable collider and sprite renderer
//             sr.enabled = true;
//             polyCol.enabled = true;

//             attackBool = true;
//             StartCoroutine(attackAnimation());
//         }
//     }

//     public IEnumerator attackAnimation()
//     {
//         if (!isFlipped)
//         {
//             if (transform.eulerAngles.z < startingRotation.eulerAngles.z + range && transform.eulerAngles.z > startingRotation.eulerAngles.z + range - 360f)
//             {
//                 yield return new WaitForSeconds(attackSpeed);
//                 transform.Rotate(0f, 0f, speed);
//                 StartCoroutine(attackAnimation());
//                 yield break;
//             }
//         }
//         else
//         {
//             if (transform.eulerAngles.z > startingRotation.eulerAngles.z - range && transform.eulerAngles.z < startingRotation.eulerAngles.z - range + 360f)
//             {
//                 yield return new WaitForSeconds(attackSpeed);
//                 transform.Rotate(0f, 0f, -1*speed);
//                 StartCoroutine(attackAnimation());
//                 yield break;
//             }
//         }
//         sr.enabled = false;
//         polyCol.enabled = false;
//         attackBool = false;
//         StartCoroutine(resetCooldown());
//     }

//     public IEnumerator resetCooldown()
//     {
//         curCooldown = true;
//         // Instantiate(smite_particles, transform.position, Quaternion.identity);
//         yield return new WaitForSeconds(weaponCooldown);
//         curCooldown = false;
//     }
// }
