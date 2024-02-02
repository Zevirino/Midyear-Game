using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
{
    public GameObject fireball;
    public GameObject dissapearingWall;
    public GameObject fire;
    public GameObject player;
    public GameObject camera;
    public GameObject startCamera;
    public GameObject laser;
    public GameObject laserSpawner;
    public GameObject door;

    public GameObject slider;

    public static bool first;

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

    private float health;
    public float laserDamageVar = 5f;
    public float playerDamageVar = 10f;
    public static bool restartFight = false;

    // Start is called before the first frame update
    void Start()
    {
        startCamera.SetActive(true);
        CameraFollow.bossFight = false;
        BossRoomCamera.on = false;
        health = 100f;
        invulnerable = false;
        BossScript.first = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (health <= 0)
        {
            door.SetActive(true);
            dissapearingWall.SetActive(true);
            Destroy(gameObject);
        }
        if (BossScript.first)
        {
            StartCoroutine(poisonAttack());
            BossScript.first = false;
        }
    }

    public IEnumerator attackPattern()
    {
        if (camera.GetComponent<BossRoomCamera>().enabled && !invulnerable)
        {
            int RandomNum = Random.Range(0, 4);
            switch (RandomNum)
            {
                case 0:
                    Debug.Log("fire ball attack");
                    StartCoroutine(fireBallAttack());
                    break;
                case 1:
                    Debug.Log("fire attack");
                    StartCoroutine(fireAttack());
                    break;
                case 2:
                    Debug.Log("poisonAttack");
                    StartCoroutine(poisonAttack());
                    break;
                case 3:
                    Debug.Log("freezePeriod");
                    StartCoroutine(freezePeriod());
                    break;
            }
        }
        else
        {
            yield return new WaitForSeconds(1);
            StartCoroutine(attackPattern());
        }
        yield break;
    }

    public IEnumerator fireBallAttack()
    {
        float corY = -200f;
        while (corY < 200f)
        {
            GameObject go = Instantiate(fireball, new Vector2(transform.position.x + fireballXOffset, transform.position.y + fireballYOffset), Quaternion.identity) as GameObject;
            go.GetComponent<FireBall>().onCreation(corY);
            yield return new WaitForSeconds(fireBallTimeDelay);
            corY += 10f;
        }
        corY = -200f;
        while (corY < 200f)
        {
            GameObject go = Instantiate(fireball, new Vector2(transform.position.x + fireballXOffset, transform.position.y + fireballYOffset), Quaternion.identity) as GameObject;
            go.GetComponent<FireBall>().onCreation(corY);
            yield return new WaitForSeconds(fireBallTimeDelay);
            corY += 10f;
        }
        yield return new WaitForSeconds(5f);
        StartCoroutine(attackPattern());
    }

        public IEnumerator fireAttack()
    {
        GameObject go = Instantiate(fire, ogFirePos, Quaternion.identity);
        go.transform.Rotate(new Vector3(0f,0f,-20f));
        go.transform.localScale = ogFireSize;
        while (go.transform.localScale.x < maxFireSize)
        {
            yield return new WaitForSeconds(fireGrowDelay);
            go.transform.localScale = new Vector3(go.transform.localScale.x + fireGrowSpeed, go.transform.localScale.y + fireGrowSpeed, go.transform.localScale.z + fireGrowSpeed);
            go.transform.position = new Vector2(go.transform.position.x - 1.5f*fireGrowSpeed, go.transform.position.y - fireGrowSpeed);
        }
        yield return new WaitForSeconds(0.1f);
        Destroy(go);
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
            if (player.transform.position.x < playerOgXPos + 2f)
            {
                player.transform.Translate(new Vector3(-0.4f, 0f, 0f));
            }
            else
            {
                player.transform.Translate(new Vector3((playerOgXPos - player.transform.position.x) * playerThrownBackSpeed * Time.deltaTime, 0.01f, 0f));
            }
        }
        GameManager.gameFreeze = false;
        dissapearingWall.SetActive(true);
        StartCoroutine(attackPattern());
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
                slider.GetComponent<Slider>().value -= laserDamageVar;
                StartCoroutine(invinciblePeriod());
                StartCoroutine(laserFall(collision.gameObject));
            }
        }
        if (collision.gameObject.CompareTag("Weapon"))
        {
            health -= playerDamageVar;
            slider.GetComponent<Slider>().value -= playerDamageVar;
            StartCoroutine(invinciblePeriod());
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().takeDamage(10);
        }
    }
}
