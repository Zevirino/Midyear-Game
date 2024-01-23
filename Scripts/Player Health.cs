using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 6;
    public int health;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
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
