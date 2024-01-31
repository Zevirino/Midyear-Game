using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gross_dmg : MonoBehaviour
{
    public int dmg;
    public PlayerHealth playerHealth;

    private void OnCollisionEnter2D(Collision2D coll){
        if(coll.gameObject.tag == "Player"){
            playerHealth.takeDamage(dmg);
        }
    }
}
