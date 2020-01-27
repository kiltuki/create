using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public enum Floor
{
    ACC,
    PARALYSIS
}*/


public class Acc : MonoBehaviour {

 //   public Floor floor = Floor.ACC;

    void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(100, 0,0), ForceMode.VelocityChange);
    }
}
