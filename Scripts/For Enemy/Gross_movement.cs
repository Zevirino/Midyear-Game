using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gross_movement : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform target; // 다른 캐릭터의 Transform을 저장할 변수
    public float speed = 1f; // 이동 속도
    public float distance;
    public float range;
    private Animator anim;
    public int duration = 60;
    public int timeRemaining;
    public bool isCountingDown = false;
    public bool facingLeft = true;
    public bool is_attacking = false;

public Vector3 initialPosition = new Vector3(100f, 0f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3 (3, 0, 0);
        anim = GetComponent<Animator>();
    }

    void flipHorizontally(){
                    //flip
        Vector3 flippedScale = transform.localScale;
        flippedScale.x *= -1;
        transform.localScale= flippedScale;
    }

    void handle_face_L_R_logic(){
        if(target.transform.position.x< transform.position.x){
            if (!facingLeft){
                facingLeft=true;
                flipHorizontally();
            }
        }
        else{
            if (facingLeft){
                facingLeft=false;
                flipHorizontally();
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        handle_face_L_R_logic();
        distance = Vector2.Distance(transform.position, target.transform.position);
        Vector2 direction = target.transform.position - transform.position; 

        if(Vector2.Distance(transform.position, target.position) <= range){
            is_attacking = true;
            anim.Play("enemy_attack");
        }
        else{
            is_attacking = false;
            if (target!=null){
                if (PlayerScript.using2d)
                {
                transform.Translate(new Vector3((target.transform.position.x-transform.position.x)*speed * Time.deltaTime, 0f, 0f));
                    transform.position = Vector2.MoveTowards(this.transform.position, target.transform.position, speed * Time.deltaTime);
                }
                else 
                {
                    transform.position = Vector2.MoveTowards(this.transform.position, target.transform.position, speed * Time.deltaTime);
                    
                }
                anim.Play("Gross_walk");
            }
            
        }
            
            // float newX = Mathf.MoveTowards(transform.position.x, target.position.x, speed * Time.deltaTime);
            
            // // y좌표에 대해 움직임
            // float newY = Mathf.MoveTowards(transform.position.y, target.position.y, speed * Time.deltaTime);

            // // z좌표에 대해 움직임
            // float newZ = Mathf.MoveTowards(transform.position.z, target.position.z, speed * Time.deltaTime);

            // // 각각의 좌표를 업데이트
            // transform.position = new Vector3(newX, newY, newZ);
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && is_attacking)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }




}
