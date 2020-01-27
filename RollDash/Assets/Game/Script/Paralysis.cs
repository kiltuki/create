using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralysis : MonoBehaviour {

   public GameObject target;
 
    void OnTriggerEnter(Collider other)
    {
        this.gameObject.SetActive(false);

        // プレーヤーの動きを止める。
        target = GameObject.Find("Player(Clone)");
        GameObject obj = other.gameObject;
        
        other.GetComponent<Rigidbody>().velocity = new Vector3(0, 0,0);
        target.GetComponent<Player>().enabled = false;
       
        // 2秒後にReleaseメソッドを呼び出す。
        Invoke("Release", 2.0f);
    }

    void Release()
    {
        if (GameObject.Find("Player(Clone)") == true)
        {
            target.GetComponent<Player>().enabled = true;
        }
    }
}
