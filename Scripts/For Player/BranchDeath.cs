using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BranchDeath : MonoBehaviour
{
    public float minPlayerHeight = 0.1f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator deathAnimation(Vector2 branchPosition)
    {
        if (transform.localScale.y >= minPlayerHeight)
        {
            yield return new WaitForSeconds(0.01f);
            transform.Rotate(0f, 0f, -5f);
            transform.localScale += new Vector3(-0.01f, -0.01f, -0.01f);
            StartCoroutine(deathAnimation(branchPosition));
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }   
    }
}
