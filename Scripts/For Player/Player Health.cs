using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void takeDamage(int dmg)
    {
        health -= dmg;
        if(health <= 0){
            Destroy(gameObject);
        }
    }
}
