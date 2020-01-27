//各モードにおけるリザルト画面の表示内容を管理するクラス

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    //タイムアタックモードのハイスコア保存用変数
    private int highScore;
    //今回のタイム保存用変数
    private int resultScore;

    //通常モードの最速タイムを保存用変数
    private float bestTime;

    //今回のスコア保存用変数
    private float resultTime;


    //ゲームの結果を表示するテキスト
    public Text resultText;

    //通常モード、タイムアタックモードは今までのハイスコア用だがBattleモードではNPCとPlayerの勝敗表示に用いている
    public Text topText;



    // Use this for initialization
    void Start()
    {    //今までの最速タイムよりも早かった場合上書きして保存する
        if (CardManager.gameMode == 1)
        {
            if (PlayerPrefs.HasKey("BestTime"))
            {
                bestTime = PlayerPrefs.GetFloat("BestTime");
            }
            else
            {
                bestTime = 0.00f;
            }
        }
        //今までのハイスコアよりも得点が高い場合上書きして保存する
        else if (CardManager.gameMode == 2)
        {
            if (PlayerPrefs.HasKey("HighScore"))
            {
                highScore = PlayerPrefs.GetInt("HighScore");
            }
            else
            {
                highScore = 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (CardManager.gameMode == 1)
        {
            resultTime = Clear.clearTime;
            resultText.text = "Time : " + resultTime.ToString("F2");

            topText.text = "BestTime : " + bestTime.ToString("F2");
            if (bestTime < resultTime) PlayerPrefs.SetFloat("BestTime", resultTime);

        }

        else if (CardManager.gameMode == 2)
        {
            resultScore = TimeUp.clearScore;
            resultText.text = "Score : " + resultScore.ToString();

            topText.text = "HighScore : " + highScore.ToString();
            if (highScore < resultScore) PlayerPrefs.SetInt("HighScore", resultScore);
        }


        else if (CardManager.gameMode == 3)
        {
            resultText.text = "Time:" + Score.clearTime.ToString("F2") + " " + Score.npcScore + "-" + Score.playerScore;
            if (Score.npcScore > Score.playerScore)
            {
                topText.text = "     NPC  Win!!";
            }
            else if (Score.npcScore < Score.playerScore)
            {
                topText.text = "     Player  Win!!";
            }
            else
            {
                topText.text = "      Draw!!";
            }
        }
    }

}
