using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gross_movement : MonoBehaviour
{
    // Start is called before the first frame update

public Transform target; // 다른 캐릭터의 Transform을 저장할 변수
    public float speed = 5f; // 이동 속도

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
float newX = Mathf.MoveTowards(transform.position.x, target.position.x, speed * Time.deltaTime);
            
            // y좌표에 대해 움직임
            float newY = Mathf.MoveTowards(transform.position.y, target.position.y, speed * Time.deltaTime);

            // z좌표에 대해 움직임
            float newZ = Mathf.MoveTowards(transform.position.z, target.position.z, speed * Time.deltaTime);

            // 각각의 좌표를 업데이트
            transform.position = new Vector3(newX, newY, newZ);
    }
}
