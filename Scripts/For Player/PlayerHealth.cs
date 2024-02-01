using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerHealth : MonoBehaviour
{

    public int duration = 120;
    public int timeRemaining;
    private bool debugging = true;
    public int health = 10;
    private int waitTime = 1;
    private Animator anim;
    private Gross_movement enemy_entity;
    private bool emitted_blood = false;
    public GameObject BloodEffect;
    // Start is called before the first frame update
    void Start()
    {
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        if (enemy!=null)enemy_entity = enemy.GetComponent<Gross_movement>(); 
        anim = GetComponent<Animator>();
        
    }
    void Update(){
        takeDamage(0);
    }
    public void takeDamage(int dmg)
    {
        
        if (enemy_entity.is_attacking){
            print("TAKING DAMAGE "+health);
            timeRemaining--;
            if(timeRemaining <= 0 && health >= 0){
                if (timeRemaining%53 == 0)Instantiate(BloodEffect, transform.position, Quaternion.identity);
                timeRemaining = duration;
                health--;
                health = System.Math.Max(health,0);
                if (health <= 0){
                    //TODO: include player death game logic
                    
                    health = -1;
                }
                if (debugging) print("player took damage health now: "+health);
            }
        }    
        if (health <= 0){
            //TODO: include player death game logic
            anim.Play("Die");
            if (!emitted_blood){
                Instantiate(BloodEffect, transform.position, Quaternion.identity);
                emitted_blood = true;
            }
            StartCoroutine(Die());
        } 
    }
    private IEnumerator Die()
    {   
        //gives PlayerScript.cs enough time to animate death
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
}
