using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gross : MonoBehaviour
{
    public float health;
    public float invulnerableTime = 2f;
    private bool invulnerable;
    public GameObject invulnerableSquare;

    // Start is called before the first frame update
    void Start()
    {
        health = 10f;
        invulnerable = false;
        invulnerableSquare.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        if (invulnerable)
        {
            invulnerableSquare.SetActive(true);
        }
    }

    public void takeDamage(float dmg)
    {
        if (!invulnerable)
        {
            health -= dmg;
            StartCoroutine(becomeInvulnerable());
        }
    }

    public IEnumerator becomeInvulnerable()
    {
        invulnerable = true;
        yield return new WaitForSeconds(invulnerableTime);
        invulnerable = false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
            takeDamage(5f);
        }
    }
}
