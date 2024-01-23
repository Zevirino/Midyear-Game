using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public GameObject weapon;
    public static float speed = -5f;
    public static bool isFlipped = false;
    public float range = 180f;
    private Quaternion startingRotation;
    private bool attackBool = false;
    // Start is called before the first frame update
    void Start()
    {
        startingRotation = weapon.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        while (attackBool)
        {
            StartCoroutine(attackAnimation());
        }
    }

    public void attack()
    {
        weapon.transform.rotation = startingRotation;
        weapon.SetActive(true);
        attackBool = true;
    }

    private IEnumerator attackAnimation()
    {
        if (!isFlipped)
        {
            if (weapon.transform.eulerAngles.z < startingRotation.eulerAngles.z + range)
            {
                yield return new WaitForSeconds(1);
                transform.Rotate(0f, 0f, speed);
                Debug.Log(weapon.transform.eulerAngles.z);
                Debug.Log(startingRotation.eulerAngles.z);
            }
            else
            {
                weapon.SetActive(false);
                attackBool = false;
            }
        }
        else
        {
            if (weapon.transform.eulerAngles.z > startingRotation.eulerAngles.z - range)
            {
                yield return new WaitForSeconds(1);
                transform.Rotate(0f, 0f, speed);
                Debug.Log(weapon.transform.eulerAngles.z);
                Debug.Log(startingRotation.eulerAngles.z);
            }
            else
            {
                weapon.SetActive(false);
                attackBool = false;
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (attackBool && collision.gameObject.CompareTag("Branch"))
        {
            BreakBranch.isBreaking=true;
        }
        
    }
}
