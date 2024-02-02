using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLaser : MonoBehaviour
{
    public GameObject laser;
    // Start is called before the first frame update
    void Start()
    {
        GameObject go = Instantiate(laser, transform.position, Quaternion.identity) as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator spawnLaser(GameObject prevLaser)
    {
        Destroy(prevLaser);
        yield return new WaitForSeconds(0.5f);
        GameObject go = Instantiate(laser, transform.position, Quaternion.identity) as GameObject;
    }
}
