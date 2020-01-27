//ステージ上にあるカードをすべてめくるためのクラス
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAllOpen : MonoBehaviour {
    private int cardCount;

	// Use this for initialization
	void Start () {
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	public void AllOpen () {
        cardCount = CardManager.cardList.Count;

        for (int i = 0; i<cardCount; i++)
        {
            CardCheck.AllOpen(i);

        }
        gameObject.SetActive(false);
	}
}
