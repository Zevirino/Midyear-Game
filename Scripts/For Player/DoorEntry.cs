using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorEntry : MonoBehaviour
{
    public float doorTimeDelay = 0.05f;
    public float frameDistance = 0.025f;

    public GameObject fadeToBlack;
    public float fadeTimeDelay = 0.1f;
    public float transparency;

    // Start is called before the first frame update
    void Start()
    {
        transparency = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator doorAnimation()
    {
        //Player walks through the door
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(doorTimeDelay);
            transform.Translate(new Vector3(frameDistance, 0, 0));
        }
    }

    public IEnumerator fadeAnimation()
    {
        //Screen fades to black
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(fadeTimeDelay);
            fadeToBlack.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, transparency);
            transparency += 0.05f;
        }
        //Loads the next level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
