using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlip : MonoBehaviour
{
    public GameObject player;
    private float horizontalInput;
    private SpriteRenderer sr;
    public bool isWeapon;
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
        sr.flipX = horizontalInput>0?true:horizontalInput<0?false:sr.flipX;
        if (isWeapon)
        {
            WeaponScript.speed *= -1;
            WeaponScript.isFlipped = WeaponScript.isFlipped==true?false:true;
        }
    }
}
