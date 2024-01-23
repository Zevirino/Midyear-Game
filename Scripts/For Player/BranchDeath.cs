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
        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(deathAnimation());
    }

    public IEnumerator deathAnimation()
    {
        yield return new WaitForSeconds(0.1f);
        transform.Rotate(0f, 0f, -5f);
        transform.localScale += new Vector3(-0.01f, -0.01f, -0.01f);
        if (transform.localScale.y < minPlayerHeight)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
