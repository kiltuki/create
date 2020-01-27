//時間切れになった際の処理を書くクラス

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeUp : MonoBehaviour
{
    // Use this for initialization
    //ゲームオーバー用の文字表示用テキスト
    public Text gameOver;
    //タイムアップ用の文字表示用テキスト
    public Text timeUp;
    //タイムアタックモードでのスコア保存用変数
    public static int clearScore;

    void Start()
    {
        gameOver.GetComponent<Text>().enabled = false;
        timeUp.GetComponent<Text>().enabled = false;
    }

    //ゲームオーバー時に呼び出されるメソッド
    //左クリックにてタイトルに戻る
    public void GameOver()
    {

        gameOver.GetComponent<Text>().enabled = true;

       
        if (Input.GetMouseButtonDown(0))
        {
            Time.timeScale = 0;
            CardManager.gameStatus = 0;
            SceneManager.LoadScene("title");

        }
    }

    //タイムアップ時に呼び出されるメソッド
    public void Timeup()
    {
        timeUp.GetComponent<Text>().enabled = true;
        

        //タイムアップ時の得点格納
        clearScore= Score.gameScore;

        if (Input.GetMouseButtonDown(0))
        {
            Time.timeScale = 0;
            CardManager.gameStatus = 0;
            SceneManager.LoadScene("result");

        }
    }  
}
