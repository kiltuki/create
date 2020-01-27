/* ItemControl.cs
 *  アイテムの出現時間等を管理するクラス
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;                                                                                       

public class ItemControl : MonoBehaviour {

    //アイテム格納用配列
    public GameObject[] items;

    //出現時間
    public float appearanceTime;

    //出現しているアイテム数
    public static int itemNum;

    //アイテムのインスタンスを格納する
    private GameObject itemObj;

    //アイテムが出るまでの時間
    private float itemTime;

    //Imageを表示させるCanvas格納用
    public Canvas canvas;

    //アイテムの種類を選ぶための変数
    private int appiaranceItemNum;

    //生成したアイテムが消滅するまでの時間を格納する変数
    private float destroyTime;

	// Use this for initialization
	void Start () {
        itemNum = 0;
        itemTime = 0f;
        appiaranceItemNum = 0;
        destroyTime = 1f;
        if (CardManager.gameMode == 3)
        {
            Destroy(this);
        }
	}
	
	// Update is called once per frame
	void Update () {
        //ゲームモードごとにアイテムの出現量を変える
        if (CardManager.gameMode == 1)
        {
            if (itemNum >= 3)
            {
                return;
            }
        }
        else if (CardManager.gameMode == 2)
        {
            if (itemNum >= 8)
            {
                return;
            }
        }

 
        itemTime += Time.deltaTime;
    
        //アイテム生成開始まで制限をつける
        if (Timer.time < 100.0f)
        {
            //ItemTimeがappearanceTimeを上回ったらAddIten()メソッドを呼び出す
            if (itemTime > appearanceTime)
            {
                itemTime = 0f;
                AddItem();
            }

        }

        if (CardManager.gameStatus == 1 || Timer.time < 0.0f)
        {
            Destroy(this);
        }

	}

    //アイテムのインスタンスを生成するメソッド
    void AddItem()
    {
       itemObj =  GameObject.Instantiate(items[appiaranceItemNum]);

        //ImageはUIだから通常のCanvasの外では座標上に生成はされるが非表示状態になっているそのため、Canvasと親子関係をつけることにより表示できる
       itemObj.transform.SetParent(canvas.transform, false); 

       iTween.MoveTo(itemObj, iTween.Hash("Position", new Vector3(transform.position.x, -175, 0),"Time",6.0f, "easyType", iTween.EaseType.easeOutSine));

        //yが-178で消えるぽい、１６５が出現ポイントとしては最適
        itemNum++;
        itemObj.transform.parent = transform;
        Destroy(itemObj, destroyTime);

        if (appiaranceItemNum == 1)
        {
            appiaranceItemNum = 0;
        }
        else
        {
            appiaranceItemNum = 1;
        }
        itemTime = 0f;
    }

}

