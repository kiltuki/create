//クリア時の処理を行うクラス
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Clear : MonoBehaviour
{
    //クリア時の時間を保持する変数
    public static float clearTime;
    //クリア用テキストを保持する変数
    public GameObject clearText;
    //クリア時に表示させるエフェクトを保持する変数
    public GameObject clearEffect;


    // Use this for initialization
    void Start()
    {
        clearText.GetComponent<Text>().enabled = false;
        clearEffect.SetActive(false);
    }

    // Update is called once per frame
    public void GameClear()
    {
        clearEffect.SetActive(true);
        this.gameObject.GetComponent<Text>().enabled = true;
                 
        clearTime = Timer.time;
        if (Input.GetMouseButtonDown(0))
        {
            Time.timeScale = 0;
            CardManager.gameStatus = 0;
            SceneManager.LoadScene("result");

        }
    }
}
