using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameClear : MonoBehaviour {

    public GameObject gameClearBtn;
    Timer goaltime;

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            Destroy(coll.gameObject);
            gameClearBtn.SetActive(true);
            goaltime.GetComponent<Timer>().goalFlg = true;
           
        }
    }

    public void GameRestart()
    {
        SceneManager.LoadScene("Game");
    }


}
