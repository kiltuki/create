using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remove : MonoBehaviour {

    public GameObject explosion;

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
           
            Destroy(coll.gameObject);
            GameObject.Find("Main Camera").GetComponent<GameControl>().gameFlag = false;
        }
    }
}
