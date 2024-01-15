using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject menuButton;
    public GameObject resumeButton;
    public GameObject grayOut;
    public GameObject player;
    public static bool gameFreeze;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            createMenu();
        }
    }

    public void createMenu()
    {
        menuButton.SetActive(true);
        resumeButton.SetActive(true);
        grayOut.SetActive(true);
        GameManager.gameFreeze = true;
    }

    public void resumeGame()
    {
        menuButton.SetActive(false);
        resumeButton.SetActive(false);
        grayOut.SetActive(false);
        GameManager.gameFreeze = false;
    }
}
