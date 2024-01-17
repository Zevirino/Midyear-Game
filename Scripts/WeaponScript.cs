using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public GameObject weapon;
    public float speed = -5f;
    private Quaternion startingRotation;
    // Start is called before the first frame update
    void Start()
    {
        startingRotation = weapon.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void attack()
    {
        weapon.transform.rotation = startingRotation;
        weapon.SetActive(true);
        StartCoroutine(attackAnimation());
    }

    private IEnumerator attackAnimation()
    {
        yield return new WaitForSeconds(0.01f);
        transform.Rotate(0f, 0f, speed);
        if (weapon.transform.rotation.eulerAngles==)
        
    }
}
