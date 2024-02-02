using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
    public GameObject button;
    public GameObject playText;
    private bool instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (instance)
            {
                if (PlayerPrefs.GetInt("SceneID", 2) > 2)
                {
                    playText.GetComponent<TMPro.TMP_Text>().text = "Continue";
                }
                else
                {
                    playText.GetComponent<TMPro.TMP_Text>().text = "Play";
                }
                instance = false;
            }
        }
    }

    public void Clicked(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }

    public void PlayClicked()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("SceneID", 2));
    }
}
