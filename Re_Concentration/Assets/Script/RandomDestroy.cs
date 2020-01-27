//カードがランダムな時間で消滅するためのクラス
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDestroy : MonoBehaviour
{
    //オブジェクトを消すまでの時間を格納する変数
    private float destroyTime;
    //オブジェクトが消え始める時間を格納する変数
    private float destroyStartTime;
    //このスクリプトを破棄させるかどうかを決める乱数保存用変数
    private int ranNum;


    void Start()
    {
        //乱数を３で割ったあまりがゼロではないときこのスクリプトを削除する
        ranNum = Random.Range(1, 10);
        if (!(ranNum % 3 == 0))
        {
            Destroy(this);
        }
        
        destroyStartTime = Timer.time / 2;
        destroyTime = Random.Range(10.0f, destroyStartTime);

    }

    void Update()
    {
        if (CardManager.gameMode == 2)
        {
            if (Timer.time < destroyTime)
            {
                CardAdd.emptyPosXList.Add(this.gameObject.transform.position.x);
                CardAdd.emptyPosZList.Add(this.gameObject.transform.position.z);
                CardManager.cardList.RemoveAt(CardManager.cardList.IndexOf(this.gameObject));
                Destroy(this.gameObject);
            }
        }

        if (CardManager.gameStatus == 1 || Timer.time < 0.0f)
        {
            Destroy(this);
        }
    }

}
