//カードのクリックを邪魔するEnemyの生成を行うクラス
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    //Enemyを格納するための配列
    public GameObject[] enemies;
    //Enemyが出現するまでの時間を格納するための変数
    public float appearanceTime;
    //Enemyの出現数を保存する変数
    private int enemyNum;
    //生成したEnemyオブジェクトを格納するための変数
    private GameObject enemyObj;
    //前のEnemyが現れてから次のEnemyが現れるまでの時間格納用変数
    private float enemyTime;
    //Enemyを設置するポジションX,Z
    private float enemyPosX;
    private float enemyPosZ;

    // Use this for initialization
    void Start()
    {
        enemyNum = 0;
        enemyTime = 0f;
        if (CardManager.gameMode == 3)
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {

        //カードの最大枚数以上の敵は出現しないようにしきい値を設ける
        //cardListの要素数以上になればenemyNumを0に戻し永続的に敵が出現できるようにする


        if (enemyNum >= CardManager.cardList.Count)
        {
            enemyNum = 0;
        }
        enemyTime += Time.deltaTime;

        //中盤から敵が生成される
        if (Timer.time < 100.0f)
        {
            if (enemyTime > appearanceTime)
            {
                enemyTime = 0f;
                if (CardManager.cardList.Count != 0)
                {
                    AddEnemy();
                }
                else
                {
                    return;
                }
            }
        }

        if (CardManager.gameStatus == 1 || Timer.time < 0.0f)
        {
            Destroy(this);
        }
    }

    //クリックを邪魔するEnemyを生成する。
    //カードと同じ配置で生成されるようにした。
    void AddEnemy()
    {
        if (CardManager.cardList[enemyNum] != null)
        {
            enemyPosX = CardManager.cardList[enemyNum].transform.position.x;
            enemyPosZ = CardManager.cardList[enemyNum].transform.position.z;
        }

        //今後敵の種類が増えればランダムで出現するEnemyを選択できるようにする
        enemyObj = GameObject.Instantiate(enemies[0], transform.position, Quaternion.Euler(0f, 0f, 0f));

        iTween.MoveTo(enemyObj, iTween.Hash("Position", new Vector3(enemyPosX, 1.0f, enemyPosZ), "easyType", iTween.EaseType.easeOutSine));
        enemyNum++;
        enemyTime = 0f;
    }
}
