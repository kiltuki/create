//ポーズ画面の表示非表示を管理するクラス

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {

    //ポーズ画面格納用変数
    public GameObject pouseUI;

	//ポーズ画面が出現しているときはtimeScaleを０にしゲームを中断する
	public void OnClickPouse () {
        pouseUI.SetActive(!pouseUI.activeSelf);
        if (pouseUI.activeSelf)
        {
            Time.timeScale = 0;
        }else{
            Time.timeScale = 1;
        }
		
	}
}
