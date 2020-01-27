using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    float countTime = 0;
    public bool startFlg = false;
    public bool goalFlg = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (startFlg == true && goalFlg == false)
        {
            countTime += Time.deltaTime;
            GetComponent<Text>().text = "Time " + countTime.ToString("F2");
            if (goalFlg == true)
            {
                countTime += 0;
            }
        } else if(startFlg == false && goalFlg == false)
        {
            GetComponent<Text>().text = "Time 00:00";
        }
		
	}
}
