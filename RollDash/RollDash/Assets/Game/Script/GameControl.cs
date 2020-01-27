using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameControl : MonoBehaviour {

    public GameObject reStartBtn;
    public GameObject gameStartBtn;
    public GameObject player;
    public bool gameFlag;

    // Use this for initialization
    void Start()
    {
        gameFlag = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (gameFlag == false )
        {
            reStartBtn.SetActive(true);
        }

    }

    public void GameStartButton()
    {
        Instantiate(player);
        gameStartBtn.SetActive(false);
    }

    public void GameRestart()
    {
        SceneManager.LoadScene("Game");
    }
}