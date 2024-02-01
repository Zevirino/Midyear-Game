using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public GameObject fireball;
    public GameObject dissapearingWall;
    public GameObject fire;
    public GameObject player;
    public GameObject camera;
    public GameObject laser;
    public GameObject laserSpawner;

    public float freezeDelay = 5.0f;
    public float playerOgXPos;
    public float playerThrownBackTime = 0.01f;
    public float playerThrownBackSpeed = 0.2f;

    public float fireBallTimeDelay = 0.2f;
    public float fireballXOffset;
    public float fireballYOffset;

    public Vector3 ogFireSize;
    public Vector2 ogFirePos;
    public float maxFireSize;
    public float fireGrowSpeed;
    public float fireGrowDelay;

    public Vector2 laserPos;
    public float laserGrowSpeed;
    public float laserGrowDelay;

    public bool invulnerable = true;
    public float invulnerableTime = 2f;
    public GameObject invulnerableBox;

    private float health = 100f;
    public float laserDamageVar = 5f;
    public float playerDamageVar = 10f;

    // Start is called before the first frame update
    void Start()
    {
        playerOgXPos = player.GetComponent<PlayerScript>().transform.position.x;
        invulnerable = false;
        StartCoroutine(attackPattern());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator attackPattern()
    {
        if (camera.GetComponent<BossRoomCamera>().enabled && !invulnerable)
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
            //StartCoroutine(attackPattern());
            StartCoroutine(poisonAttack());
        }
        yield break;
    }

    public IEnumerator fireBallAttack()
    {
        float corY = -20f;
        while (corY < 20f)
        {
            GameObject go = Instantiate(fireball, new Vector2(transform.position.x + fireballXOffset, transform.position.y + fireballYOffset), Quaternion.identity) as GameObject;
            go.GetComponent<FireBall>().onCreation(corY);
            yield return new WaitForSeconds(fireBallTimeDelay);
            corY += 2f;
        }
        corY = -20f;
        while (corY < 20f)
        {
            GameObject go = Instantiate(fireball, new Vector2(transform.position.x + fireballXOffset, transform.position.y + fireballYOffset), Quaternion.identity) as GameObject;
            go.GetComponent<FireBall>().onCreation(corY);
            yield return new WaitForSeconds(fireBallTimeDelay);
            corY += 2f;
        }
        yield return new WaitForSeconds(5f);
        StartCoroutine(attackPattern());
    }

    public IEnumerator fireAttack()
    {
        fire.transform.localScale = ogFireSize;
        fire.transform.position = ogFirePos;
        fire.SetActive(true);
        while (fire.transform.localScale.x < maxFireSize)
        {
            yield return new WaitForSeconds(fireGrowDelay);
            fire.transform.localScale = new Vector3(fire.transform.localScale.x + fireGrowSpeed, fire.transform.localScale.y + fireGrowSpeed, fire.transform.localScale.z + fireGrowSpeed);
            fire.transform.position = new Vector2(fire.transform.position.x - 1.5f*fireGrowSpeed, fire.transform.position.y - fireGrowSpeed);
        }
        yield return new WaitForSeconds(0.1f);
        fire.SetActive(false);
        yield return new WaitForSeconds(5f);
        StartCoroutine(attackPattern());
    }

    public IEnumerator poisonAttack()
    {
        GameObject go = Instantiate(laser, laserPos, Quaternion.identity) as GameObject;
        go.transform.localScale = new Vector3(0,0.5f,1);
        while(go.transform.localScale.x < 7f)
        {
            yield return new WaitForSeconds(laserGrowDelay);
            go.transform.localScale = new Vector3(go.transform.localScale.x + 1, go.transform.localScale.y, go.transform.localScale.z);
            go.transform.position = new Vector2(go.transform.position.x + laserGrowSpeed, go.transform.position.y + laserGrowSpeed/2);
        }
        yield return new WaitForSeconds(10f);
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

    public IEnumerator laserFall(GameObject go)
    {
        yield return new WaitForSeconds(3f);
        StartCoroutine(laserSpawner.GetComponent<SpawnLaser>().spawnLaser(go));
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Laser"))
        {
            if (!invulnerable)
            {
                health -= laserDamageVar;
                StartCoroutine(invinciblePeriod());
                StartCoroutine(laserFall(collision.gameObject));
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
