using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour {

    float differenceX;
    private Vector3 difference;
    private GameObject target;



    void Start () {
        difference = transform.localPosition;
        differenceX = difference.x;
	}

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Player(Clone)") == true)
        {
            /* target = GameObject.Find("Player(Clone)");
             offset = transform.position - target.gameObject.transform.position;
             //プレイヤーの座標に差分を加えたもの（ずっとついてくるタイプ）
             transform.position = GameObject.Find("Player(Clone)").transform.position + offset;*/

            Vector3 startVec = GameObject.Find("Player(Clone)").transform.localPosition;
            transform.localPosition = new Vector3(startVec.x + differenceX, startVec.y + difference.y, startVec.z+ difference.z+5);
        }
    }
			
}
