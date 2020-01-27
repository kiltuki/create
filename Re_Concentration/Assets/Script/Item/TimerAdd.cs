using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerAdd : MonoBehaviour {
    public GameObject fiveText;
    public Canvas canvas;


    void Start()
    {
        gameObject.SetActive(false);
    }

    //５秒制限時間を加算しボタンを非表示にする
    public void ClickTimeAdd()
    {
        Timer.time += 5.0f;
        gameObject.SetActive(false);
        GameObject addTimeText = GameObject.Instantiate(fiveText);
        addTimeText.transform.SetParent(canvas.transform, false);
        iTween.MoveBy(addTimeText, iTween.Hash("y", 120, "Time", 1.5f, "easyType", iTween.EaseType.easeOutSine));
        Destroy(addTimeText, 1f);
   
   
    }
}
