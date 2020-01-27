/*  CardManager.cs
 *  シャッフル
 *  カードチェック
 *  カードの割り振り
 *  ゲームの根幹部分はここに記述する
 *  NPCの処理
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    //ゲームの状態を管理するフラグ、0ゲーム中、１ゲームクリア
    public static int gameStatus = 0;

    //プレイしているモードを管理するフラグ、１通常モード、２タイムアタックモード、３Battleモード
    public static int gameMode = 0;

    //BattleモードのときのPlayerのターンかどうかを管理するフラグ
    public static bool playerTrun = true;

    //クリックされているカードのゲームオブジェクトを保存する配列
    private GameObject[] reversedCard = { null, null };
    //クリックされているカードの種類を判別するためのIDを保存するための配列
    private int[] reversedID = { 0, 0 };

    //配布するカードの種類を管理する配列　[0]red,[1]blue,[2]green,[3]purple,[4]yellow,[5]heart,[6]joker
    public GameObject[] cardSet;

    GameObject cardObj = null;
    //カードを配るアニメーションで時差をつけるための変数
    private float delay = 0;

    //クリックされたかどうかを保存するフラグ
    public int clicked = 0;

    //カードを配る前に一時的にカードの種類情報を保持するための配列
    int[] cards = new int[24];

    //プレイ中のカードのリスト
    public static List<GameObject> cardList = new List<GameObject>();

    //カードのポジションを一時的に保存しておくためのList
    private List<float> cardPosXList = new List<float>();
    private List<float> cardPosZList = new List<float>();

    System.Random ran = new System.Random();

    //カードの配布等でカウントした数字を一時的に保存するための変数
    private int count = 0;

    //消せたカードの枚数を保存するようの変数
    public static int destroyCount = 0;

    public AudioSource Source;

    //NPCが選んだカード２まいの値を保存するための変数
    public int npcRan1;
    public int npcRan2;


    // Use this for initialization
    void Start()
    {
        GameInit();

        //カードを赤４、青４、緑４、黄４、紫４、ハート２、Joker２枚をcards配列に格納する
        for (int i = 0; i < cards.Length; i++)
        {
            if (i < 4)
            {
                cards[i] = 0;
            }
            else if (i >= 4 && i < 8)
            {
                cards[i] = 1;
            }
            else if (i >= 8 && i < 12)
            {
                cards[i] = 2;
            }
            else if (i >= 12 && i < 16)
            {
                cards[i] = 3;
            }
            else if (i >= 16 && i < 20)
            {
                cards[i] = 4;
            }
            else if (i >= 20 && i < 22)
            {
                cards[i] = 5;
            }
            else
            {
                cards[i] = 6;
            }
        }
        //cards配列の中身をランダムに入れ替える
        for (int i = 0; i < 2000; i++)
        {
            for (int j = 0; j < cards.Length; j++)
            {
                int k = ran.Next(j + 1);
                int tmp = cards[k];
                cards[k] = cards[j];
                cards[j] = tmp;
            }
        }

        //cards配列に格納されている値と対応するカードのオブジェクトを生成する
        while (count < cards.Length)
        {
            cardObj = (GameObject)Instantiate(cardSet[cards[count]]);

            //カードを並べるアニメーション
            var moveHash = new Hashtable();
            cardObj.transform.position = new Vector3(10, 0, 0);
            moveHash.Add("position", new Vector3((float)(count / 6) * 12.3f - 18.0f, 0.5f, (count % 6) * 10.3f + 2.5f));
            moveHash.Add("time", 1.0f + delay);

            iTween.MoveTo(cardObj, moveHash);

            cardObj.GetComponent<CardCheck>().id = cards[count];
            cardList.Add(cardObj);

            count++;
            delay += 0.1f;

        }

    }

    void Update()
    {
        if (Time.timeScale == 0)
        {
            return;
        }
        //デバッグ用(GameClear)
        if (Input.GetKeyDown(KeyCode.C))
        {

            for (int i = 0; i < cardList.Count; i++)
            {
                Destroy(cardList[i]);
            }
            gameStatus = 1;
        }

        //カードをクリックしたらCardReverseクラスのReverseメソッドにアクセス
        if (playerTrun == true)
        {

            ClickAction();


        }

    }
    /// <summary>
    ///  第一引数にカードの種類を判別するための変数id第二引数にカードのゲームオブジェクト本体を渡し、
    ///  二度このメソッドが呼び出されたときカードの種類が一致しているかを判断するCheck()メソッドが呼び出される
    /// </summary>
    /// <param name="id">カードの種類を判別するための変数</param>
    /// <param name="obj">カードのゲームオブジェクト本体</param>

    public void ClickObj(int id, GameObject obj)
    {

        //めくられているカードのid,GameObjectをそれぞれ格納する
        reversedID[clicked] = id;
        reversedCard[clicked] = obj;


        if (clicked == 1)
        {

            Check();
        }
        clicked = (clicked + 1) % 2;

    }

    public void Check()
    {

        //一枚目と二枚目が存在していればcheckを行う
        if (reversedCard[0] != null && reversedCard[1] != null)
        {
            //１枚目と２枚目のidが一致していたらそのidに対応したカードの処理を行う
            if (reversedID[0] == reversedID[1])
            {
                //一致処理中にオブジェクトが消えないようにする
                Destroy(reversedCard[0].GetComponent<RandomDestroy>());
                Destroy(reversedCard[1].GetComponent<RandomDestroy>());

                switch (reversedID[0])
                {

                    case 0://火のエフェクト

                        reversedCard[0].GetComponent<CardCheck>().effect(true);
                        reversedCard[1].GetComponent<CardCheck>().effect(true);

                        cardList.RemoveAt(cardList.IndexOf(reversedCard[0]));
                        cardList.RemoveAt(cardList.IndexOf(reversedCard[1]));

                        break;
                    case 1://氷のエフェクト

                        reversedCard[0].GetComponent<CardCheck>().effect(true);
                        reversedCard[1].GetComponent<CardCheck>().effect(true);

                        cardList.RemoveAt(cardList.IndexOf(reversedCard[0]));
                        cardList.RemoveAt(cardList.IndexOf(reversedCard[1]));

                        break;
                    case 2://自然のエフェクト

                        reversedCard[0].GetComponent<CardCheck>().effect(true);
                        reversedCard[1].GetComponent<CardCheck>().effect(true);

                        cardList.RemoveAt(cardList.IndexOf(reversedCard[0]));
                        cardList.RemoveAt(cardList.IndexOf(reversedCard[1]));

                        break;
                    case 3://闇のエフェクト

                        reversedCard[0].GetComponent<CardCheck>().effect(true);
                        reversedCard[1].GetComponent<CardCheck>().effect(true);

                        cardList.RemoveAt(cardList.IndexOf(reversedCard[0]));
                        cardList.RemoveAt(cardList.IndexOf(reversedCard[1]));

                        break;
                    case 4:// 光のエフェクト

                        reversedCard[0].GetComponent<CardCheck>().effect(true);
                        reversedCard[1].GetComponent<CardCheck>().effect(true);

                        cardList.RemoveAt(cardList.IndexOf(reversedCard[0]));
                        cardList.RemoveAt(cardList.IndexOf(reversedCard[1]));

                        break;
                    case 5://回復のエフェクト

                        reversedCard[0].GetComponent<CardCheck>().effect(true);
                        reversedCard[1].GetComponent<CardCheck>().effect(true);
                        cardList.RemoveAt(cardList.IndexOf(reversedCard[0]));
                        cardList.RemoveAt(cardList.IndexOf(reversedCard[1]));
                        break;
                    case 6://虹カード
                        if (cardList.Count > 4)
                        {
                            cardList.RemoveAt(cardList.IndexOf(reversedCard[0]));
                            cardList.RemoveAt(cardList.IndexOf(reversedCard[1]));
                            Shuffle();
                        }

                        break;
                }
                //カードが消失する場所をそれぞれListに保存する
                CardAdd.emptyPosXList.Add(reversedCard[0].transform.position.x);
                CardAdd.emptyPosXList.Add(reversedCard[1].transform.position.x);
                CardAdd.emptyPosZList.Add(reversedCard[0].transform.position.z);
                CardAdd.emptyPosZList.Add(reversedCard[1].transform.position.z);
                Destroy(reversedCard[0]);
                Destroy(reversedCard[1]);

                destroyCount = destroyCount + 2;

                //Battleモードのときターンを交代する
                if (gameMode == 3)
                {
                    if (playerTrun == true)
                    {
                        Score.playerScore += 2;
                    }
                    else if (playerTrun != true)
                    {
                        Score.npcScore += 2;
                    }
                }

                //通常モードでありカードをすべてめくりきったらGameStatusをゲーム終了に変更する
                if (gameMode == 1 && cardList.Count == 0)
                {
                    gameStatus = 1;
                }

            }
            else
            {
                //処理の途中でカードがどちらか消失したら残っている方を元の状態に戻す
                if (reversedCard[0] != null)
                {
                    reversedCard[0].GetComponent<CardCheck>().ReSet();

                }
                if (reversedCard[1] != null)
                {
                    reversedCard[1].GetComponent<CardCheck>().ReSet();

                }
            }
        }
        //処理の途中でカードがどちらか消失したら残っている方を元の状態に戻す
        if (reversedCard[0] != null)
        {
            reversedCard[0].GetComponent<CardCheck>().ReSet();

        }
        if (reversedCard[1] != null)
        {
            reversedCard[1].GetComponent<CardCheck>().ReSet();

        }

        //Battleモードでそれぞれのターンを終えたら相手にターンを移す
        if (gameMode == 3 && playerTrun == true)
        {
            playerTrun = false;
            Invoke("NPCTurn", 1.0f);

        }
        else
        {
            playerTrun = true;
        }

    }

    //DirectoryからListに変更してジョーカーよりも後ろにあるカードが先に消滅した場合シャッフルがうまく発動しないバクを解消
    //カードのオブジェクトの配置を保ちながら中身をシャッフルする方式に変更
    public void Shuffle()
    {
        count = 0;


        for (int i = 0; i < cardList.Count; i++)
        {
            cardPosXList.Add(cardList[i].transform.position.x);
            cardPosZList.Add(cardList[i].transform.position.z);
        }

        for (int i = 0; i < 10; i++)
        {
            for (int k = 0; k < cardList.Count; k++)
            {

                int r = ran.Next(k + 1);

                GameObject tmp = cardList[r];
                cardList[r] = cardList[k];
                cardList[k] = tmp;


            }
            Debug.Log(cardList.Count);
        }

        while (count < cardList.Count)
        {

            var moveHash = new Hashtable();
            cardList[count].transform.position = new Vector3(10, 0, 0);
            moveHash.Add("position", new Vector3(cardPosXList[count], 0.5f, cardPosZList[count]));

            moveHash.Add("time", 1.0f + delay);

            iTween.MoveTo(cardList[count], moveHash);
            count++;
        }
        cardPosXList.Clear();
        cardPosZList.Clear();

    }

    //パラメーターの初期化を行う
    void GameInit()
    {
        clicked = 0;
        destroyCount = 0;
        Source = GetComponent<AudioSource>();
        cardList.Clear();
        playerTrun = true;

    }

    //BattleモードのNPCのターンの処理を行う
    public void NPCTurn()
    {
        if (playerTrun == false)
        {
            //ランダムにカードをめくる
            RandmonBrain();
        }
        else
        {
            return;
        }

    }

    //ランダムにカードを二枚めくるためのメソッド
    public void RandmonBrain()
    {
        npcRan1 = Random.Range(0, cardList.Count);
        npcRan2 = Random.Range(0, cardList.Count);

        //同じ数字だったら再度乱数で値が変わるまで処理をおこなy
        while (npcRan1 == npcRan2)
        {
            npcRan2 = Random.Range(0, cardList.Count);
        }

        cardList[npcRan1].GetComponent<CardCheck>().Open();
        cardList[npcRan2].GetComponent<CardCheck>().Open();
    }

    //オブジェクトをクリック時のそれぞれの処理を行うメソッド
    public void ClickAction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "card")
                {
                    hit.collider.gameObject.GetComponent<CardCheck>().Open();
                }
                else if (hit.collider.gameObject.tag == "enemy")
                {
                    Destroy(hit.collider.gameObject);
                }

            }
        }
    }



}
