//Battleモードでのみ使用するNPCとPlayerのターン表示をするUIの管理を行うクラス
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnUIControl : MonoBehaviour {

    //PlayerSideのUIを格納する変数
    public GameObject playerSide;
    //NPCSideのUIを格納する変数
    public GameObject npcSide;

    //PlayerとNPCのターンが来たとき用のUI保存用変数
    public GameObject playerTurnUI;
    
    public GameObject npcTurnUI;


	void Start () {
        //Battleモード以外ではオブジェクトごとDestroyする
        if (CardManager.gameMode != 3)
        {
            Destroy(playerSide.gameObject);
            Destroy(npcSide.gameObject);
            Destroy(playerTurnUI);
            Destroy(npcTurnUI);
        }      
		
	}
	
	// Update is called once per frame
	void Update () {
            //それぞれターンが来たら、前がターンだった方のターン用UIを非表示にし、現在のターンのほうのUIを表示にする
        if (CardManager.gameMode == 3)
        {
            if (CardManager.playerTrun == true)
            {
                CanvasControl.SetActive(playerTurnUI.name, true);
                CanvasControl.SetActive(npcTurnUI.name, false);
            }
            else
            {
                CanvasControl.SetActive(playerTurnUI.name, false);
                CanvasControl.SetActive(npcTurnUI.name, true);
            }
        }
		
	}
}
