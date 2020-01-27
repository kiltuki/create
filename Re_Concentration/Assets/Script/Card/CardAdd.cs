/* TimeAttackモードのみ使用する機能です
 * ゲーム終盤になったところでカードを追加するクラスです*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAdd : MonoBehaviour {

    int count;
    //GameObjectが消滅した場所を保存するためのList
    public static List<float> emptyPosXList = new List<float>();
    public static List<float> emptyPosZList = new List<float>();
    //乱数で生成したカードの属性番号保存用
    public int ranCardNum;
    //インスタンスを生成し終えた追加カードを保存するための変数
    public GameObject ranCardObj;
    //追加生成出来るカードのGameObjectを入れるための配列
    public GameObject[] cardSet;
    //カードを追加する時間を保存する変数
    private float startTime;


	// Use this for initialization
	void Start () {
        emptyPosXList.Clear();
        emptyPosZList.Clear();
        count = 0;
        startTime = 40.0f;
	}
	
	// Update is called once per frame
	void Update () {
        if (CardManager.gameMode == 2)
        {
            //startTimeを残り時間が下回ったらカードを追加する
            if (Timer.time < startTime)
            {
                while (count < emptyPosXList.Count)
                {

                    ranCardNum = Random.Range(0, cardSet.Length);
                    
                    ranCardObj = GameObject.Instantiate(cardSet[ranCardNum], new Vector3(emptyPosXList[count], 0.5f, emptyPosZList[count]), Quaternion.Euler(0f, 0f, 0f));

                    ranCardObj.GetComponent<CardCheck>().id = ranCardNum;
                    CardManager.cardList.Add(ranCardObj);

                    count++;
                }
                emptyPosXList.Clear();
                emptyPosZList.Clear();
            }

        }
	}


}
