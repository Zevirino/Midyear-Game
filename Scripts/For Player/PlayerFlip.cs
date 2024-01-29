using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlip : MonoBehaviour
{
    public GameObject player;
    public GameObject weapon;
    private SpriteRenderer sr;

    public bool isWeapon;
    public bool isEye;
    public bool weaponController;

    private float previousInput = 0f;
    public float eyeOffset;
    private float horizontalInput;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = player.GetComponent<PlayerScript>().getHorizontalInput();
        //Flips the player depending on the direction they are moving
        if (!isWeapon)
        {
            sr.flipX = horizontalInput>0?true:horizontalInput<0?false:sr.flipX;
        }
        WeaponScript.isFlipped = horizontalInput > 0 ? true : horizontalInput < 0 ? false : WeaponScript.isFlipped;

        if (!(previousInput<0 && horizontalInput<0 || previousInput>0 && horizontalInput>0 || horizontalInput == 0 || previousInput==0 && horizontalInput<0))
        {
            if (isEye)
            {
                if (sr.flipX)
                {
                    transform.position = new Vector2(transform.position.x + eyeOffset, transform.position.y);
                }
                else
                {
                    transform.position = new Vector2(transform.position.x - eyeOffset, transform.position.y);
                }
            }
            if (isWeapon)
            {
                if (WeaponScript.isFlipped)
                {
                    if (WeaponScript.attackBool)
                    {
                        weapon.transform.Rotate(0f, 0f, -180f);
                    }
                    weapon.transform.position = new Vector2(weapon.transform.position.x + eyeOffset, weapon.transform.position.y);
                }
                else
                {
                    if (WeaponScript.attackBool)
                    {
                        weapon.transform.Rotate(0f, 0f, 180f);
                    }
                    weapon.transform.position = new Vector2(weapon.transform.position.x - eyeOffset, weapon.transform.position.y);
                }
            }
        }
        if (horizontalInput != 0)
        {
            previousInput = horizontalInput;
        }
    }
}
