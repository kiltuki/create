/* タイトル画面からゲームモードを指定してgameSceneへ遷移するためのクラスです。
 * タイトルから遷移しないと各モード特有の機能が発動しないので注意
 */
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{

    public void NormalGameLoad()   //通常モード（一人で遊ぶモード、全部めくったときのタイムが記録になる）めくりきらないとゲームオーバーへ
    {
        //タイマーのカウントを戻す
        Time.timeScale = 1;
        SceneManager.LoadScene("game");
        CardManager.gameMode = 1;

        //初期配置カード枚数設定
        //    CardManager.cardSize = 2;
    }

    public void TimeatackGameLoad()  //タイムアタックモード(できるだけ多くのカードをめくれ)ゼロ枚でもResult行く
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("game");
        CardManager.gameMode = 2;
    }

    public void BattleGameLoad()  //PCとのバトルモード、プレイ時間と何戦何勝くらいは出したい（希望的観測）
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("game");
        CardManager.gameMode = 3;
    }

}
