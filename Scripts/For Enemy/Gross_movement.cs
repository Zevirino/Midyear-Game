using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gross_movement : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform target; // 다른 캐릭터의 Transform을 저장할 변수
    public float speed = 2f; // 이동 속도
    public float distance;
    public float range;
    private Animator anim;
    public int duration = 60;
    public int timeRemaining;
    public bool isCountingDown = false;
    public bool facingLeft = true;

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

    void handleTransformLogic(){
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
        handleTransformLogic();
            distance = Vector2.Distance(transform.position, target.transform.position);
            Vector2 direction = target.transform.position - transform.position; 

            if(Vector2.Distance(transform.position, target.position) <= range){
                if (!isCountingDown) {
                    isCountingDown = true;
                    timeRemaining = duration;
                    
                }
                _tick();
            }
            else{
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
            
            // float newX = Mathf.MoveTowards(transform.position.x, target.position.x, speed * Time.deltaTime);
            
            // // y좌표에 대해 움직임
            // float newY = Mathf.MoveTowards(transform.position.y, target.position.y, speed * Time.deltaTime);

            // // z좌표에 대해 움직임
            // float newZ = Mathf.MoveTowards(transform.position.z, target.position.z, speed * Time.deltaTime);

            // // 각각의 좌표를 업데이트
            // transform.position = new Vector3(newX, newY, newZ);
    }

    private void _tick() {

        timeRemaining--;
        if(timeRemaining > 0) {
            Invoke ( "_tick", 1f );
        } else {
            isCountingDown = false;
            anim.Play("enemy_attack");
            print("attacks");
        }
    }


}
