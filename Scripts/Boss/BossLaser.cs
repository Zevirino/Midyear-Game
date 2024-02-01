using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaser : MonoBehaviour
{
    public ParticleSystem poison;
    public Vector2 poisonPos;
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(new Vector3(0f, 0f, 45f));
        StartCoroutine(poisonParticles());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator poisonParticles()
    {
        yield return new WaitForSeconds(1f);
        Instantiate(poison, poisonPos, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
