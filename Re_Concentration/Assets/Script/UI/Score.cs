//通常モードでは残り枚数、タイムアタックモードでは現在スコア、BattleモードではPlayerとNPCのそれぞれの現在の獲得枚数を管理するクラス
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    //BattleモードのPlayerとNPCのスコアとその表示用テキスト
    public Text playerScoreText;
    public Text npcScoreText;
    public static int playerScore;
    public static int npcScore;

    //クリア時間を保持するための変数
    public static float clearTime;

    //Battleモードですべてのカードがめくり終えたときに表示するテキスト
    public Text finish;

    //タイムアタックモードでの獲得スコア
    public static int gameScore;

    //通常モードでの残り枚数
    private int left;

    //獲得スコア、残り枚数表示用テキスト
    public Text scoreText;

    // Use this for initialization
    void Start()
    {
        finish.GetComponent<Text>().enabled = false;

        if (CardManager.gameMode == 1)
        {
            scoreText.GetComponent<Text>().enabled = true;
            scoreText.text = "LEFT : " + left.ToString();


        }
        else if (CardManager.gameMode == 2)
        {

            scoreText.GetComponent<Text>().enabled = true;
            gameScore = 0;
            scoreText.text = "SCORE :" + gameScore.ToString();
        }
        else if (CardManager.gameMode == 3)
        {
            playerScoreText.GetComponent<Text>().enabled = true;
            npcScoreText.GetComponent<Text>().enabled = true;
            playerScore = 0;
            npcScore = 0;
            playerScoreText.text = playerScore.ToString();
            npcScoreText.text = npcScore.ToString();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (CardManager.gameMode == 1)
        {
            left = CardManager.cardList.Count;
            scoreText.text = "LEFT : " + left.ToString();

        }
        else if (CardManager.gameMode == 2)
        {
            gameScore = CardManager.destroyCount * 100;
            scoreText.text = "SCORE :" + gameScore.ToString();
        }
        else if (CardManager.gameMode == 3)
        {
            playerScoreText.text = playerScore.ToString();
            npcScoreText.text = npcScore.ToString();

            //PlayerとNPCが獲得したカード枚数が初期枚数になったら
            if (playerScore + npcScore == 24)
            {

                FinishAction();

            }
        }

    }

    //BattleモードでFinish時に呼び出されるメソッド
    void FinishAction()
    {

        finish.GetComponent<Text>().enabled = true;
        Time.timeScale = 0;
        clearTime = Timer.time;
        if (Input.GetMouseButtonDown(0))
        {
            CardManager.gameStatus = 0;
            SceneManager.LoadScene("result");

        }
    }
}
