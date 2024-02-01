using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerHealth : MonoBehaviour
{

    public int duration = 60;
    public int timeRemaining;
    private bool debugging = true;
    public int health = 10;
    private Animator anim;
    private Gross_movement enemy_entity;
    // Start is called before the first frame update
    void Start()
    {
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        print(enemy);
if (enemy!=null){
            enemy_entity = enemy.GetComponent<Gross_movement>(); //change the class name     
            print("ENEMY_ENTITY " + enemy_entity);
        }
        anim = GetComponent<Animator>();
        
    }
    private void handle_health_damage(){
        timeRemaining--;
        print (timeRemaining);
        if(timeRemaining <= 0 && health >= 0){
            timeRemaining = duration;
            health--;
            health = System.Math.Max(health,0);
            if (health <= 0){
                //TODO: include player death game logic
                anim.Play("Die");
                health = -1;
            }
            if (debugging) print("player took damage health now: "+health);
        }
if (health <= 0){
            //TODO: include player death game logic
            anim.Play("Die");
            print("deader than a doornail");
        }

        
    }

    // Update is called once per frame

    void Update(){
        handle_health_damage();
    }
    public void takeDamage(int dmg)
    {
        health -= dmg;
        if(health <= 0){
            Destroy(gameObject);
        }
    }
}
