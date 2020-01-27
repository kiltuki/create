//制限時間を管理するクラス

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{   //ゲームオーバーのメソッドを呼び出すためのテキスト
    public Text gameOver;
    //ゲームクリア用テキスト
    public Text gameClear;
    //タイムアップGameObject
    public GameObject timeUp;
    //プレイ中の時間
    public static float time;
    // Use this for initialization
    void Start()
    {
        if (CardManager.gameMode != 3)
        {
            //制限時間を設定する
            time = 80;
        }
        else
        {
            time = 0;
        }

    }

    // Update is called once per frame
    void Update()
    {
        Text timeT = GetComponent<Text>();
        TimeUp timeUp = GetComponent<TimeUp>();

       //Battleモード以外では時間を削っていくように表示させる
        if (CardManager.gameMode != 3)
        {
            if (CardManager.gameStatus == 0)
            {
                time -= 1f * Time.deltaTime;

                timeT.text = "TIME " + time.ToString("F2");

            }
                //通常モードではめくり切ったところでクリアが表示される
            else if (CardManager.gameStatus == 1)
            {
                gameClear.SendMessage("GameClear");

            }
             //時間が0になったとき、通常モードではゲームオーバー、タイムアタックモードではタイムアップと表示させる
            if (time < 0.0f)
            {
                timeT.text = "TIME " + "0.00";
                if (CardManager.gameMode == 1)
                {
                    timeUp.GameOver();
                }
                if (CardManager.gameMode == 2)
                {
                    timeUp.Timeup();
                }

            }
        }

         //Battleモードだけは経過時間がわかるように加算していく
        else
        {
            time += 1f * Time.deltaTime;

            timeT.text = "TIME " + time.ToString("F2");
        }
    }
}
