using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public GameObject fireball;
    public GameObject dissapearingWall;
    public GameObject fire;
    public GameObject player;
    public BossRoomCamera cameraScript;

    public float freezeDelay = 5.0f;
    public float playerOgXPos;
    public float playerThrownBackTime = 0.01f;
    public float playerThrownBackSpeed = 0.2f;

    public float fireBallTimeDelay = 0.2f;

    public bool invulnerable;
    public float invulnerableTime = 2f;
    public GameObject invulnerableBox;

    private float health = 100f;
    public float laserDamageVar = 5f;
    public float playerDamageVar = 10f;

    // Start is called before the first frame update
    void Start()
    {
        playerOgXPos = player.GetComponent<PlayerScript>().transform.position.x;
        StartCoroutine(attackPattern());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator attackPattern()
    {
        if (cameraScript.enabled && !invulnerable)
        {
            int RandomNum = Random.Range(0, 4);
            switch (RandomNum)
            {
                case 0:
                    StartCoroutine(fireBallAttack());
                    break;
                case 1:
                    StartCoroutine(fireAttack());
                    break;
                case 2:
                    StartCoroutine(poisonAttack());
                    break;
                case 3:
                    StartCoroutine(freezePeriod());
                    break;

            }
        }
        else
        {
            StartCoroutine(attackPattern());
        }
        yield break;
    }

    public IEnumerator fireBallAttack()
    {
        float corY = -10f;
        while (corY < 10f)
        {
            GameObject go = Instantiate(fireball, new Vector2(transform.position.x + 2, transform.position.y), Quaternion.identity) as GameObject;
            go.GetComponent<FireBall>().onCreation(corY);
            yield return new WaitForSeconds(fireBallTimeDelay);
            corY += 0.5f;
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(attackPattern());
    }

    public IEnumerator fireAttack()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(attackPattern());
    }

    public IEnumerator poisonAttack()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(attackPattern());
    }

    public IEnumerator freezePeriod()
    {
        dissapearingWall.SetActive(false);
        yield return new WaitForSeconds(freezeDelay);
        GameManager.gameFreeze = true;
        while(player.transform.position.x > playerOgXPos)
        {
            yield return new WaitForSeconds(playerThrownBackTime);
            player.transform.Translate(new Vector3((playerOgXPos - player.transform.position.x) * playerThrownBackSpeed * Time.deltaTime, 0f, 0f));
        }
        GameManager.gameFreeze = false;
        dissapearingWall.SetActive(true);
    }

    public IEnumerator invinciblePeriod()
    {
        invulnerableBox.SetActive(true);
        invulnerable = true;
        yield return new WaitForSeconds(invulnerableTime);
        invulnerableBox.SetActive(false);
        invulnerable = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Laser"))
        {
            if (!invulnerable)
            {
                health -= laserDamageVar;
                StartCoroutine(invinciblePeriod());
            }
        }
        if (collision.gameObject.CompareTag("Weapon"))
        {
            health -= playerDamageVar;
            StartCoroutine(invinciblePeriod());
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().takeDamage(10);
        }
    }
}
