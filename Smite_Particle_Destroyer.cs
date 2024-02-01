using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smite_Particle_Destroyer : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 1f);
    }
}
