using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gross_movement : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform target; // 다른 캐릭터의 Transform을 저장할 변수
    public float speed = 2f; // 이동 속도
    public float distance;
    public float range;

public Vector3 initialPosition = new Vector3(100f, 0f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3 (3, 0, 0);
    }


    // Update is called once per frame
    void Update()
    {

            distance = Vector2.Distance(transform.position, target.transform.position);
            Vector2 direction = target.transform.position - transform.position; 
            Debug.Log("Target Position: " + target.position);
        Debug.Log("Current Position: " + transform.position);
            if(Vector2.Distance(transform.position, target.position) <= range){
                //attack
            }
            else{
                transform.position = Vector2.MoveTowards(this.transform.position, target.transform.position, speed*Time.deltaTime);
            }
            
            // float newX = Mathf.MoveTowards(transform.position.x, target.position.x, speed * Time.deltaTime);
            
            // // y좌표에 대해 움직임
            // float newY = Mathf.MoveTowards(transform.position.y, target.position.y, speed * Time.deltaTime);

            // // z좌표에 대해 움직임
            // float newZ = Mathf.MoveTowards(transform.position.z, target.position.z, speed * Time.deltaTime);

            // // 각각의 좌표를 업데이트
            // transform.position = new Vector3(newX, newY, newZ);
    }
}
