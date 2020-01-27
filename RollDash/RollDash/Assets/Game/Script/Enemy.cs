using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public GameObject explosion;
    UnityEngine.AI.NavMeshAgent agent;

	// Use this for initialization
	void Start () {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        if (GameObject.Find("Player(Clone)") == true)
        {
            agent.destination = GameObject.Find("Player(Clone)").transform.position;
        }
	}

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            Instantiate(explosion, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            Destroy(coll.gameObject);
            GameObject.Find("Main Camera").GetComponent<GameControl>().gameFlag = false; 
        }
    }
}
