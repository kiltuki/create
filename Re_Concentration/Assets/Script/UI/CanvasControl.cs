//Canvas内から非表示にしたUIの状態を変更させるためのクラス

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasControl : MonoBehaviour {

  　public static Canvas canvas;

	// Use this for initialization
	void Start () {
        canvas = GetComponent<Canvas>();
	}

    //SetActiveでfalseにしたアイテムを再度親のCanvasからたどって表示非表示を操作するためのメソッド
    //第一引数に表示非常時を操作したいものの名前、第二引数に表示か非表示の設定
    public static void SetActive(string name,bool bl)
    {
        foreach (Transform child in canvas.transform)
        {
            if (child.name == name)
            {
                child.gameObject.SetActive(bl);
                return;
            }
        }


    }
}
