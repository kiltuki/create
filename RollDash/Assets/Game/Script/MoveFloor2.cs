using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFloor2 : MonoBehaviour {

    private Vector3 initialPosition;

    // Use this for initialization
    void Start()
    {
        initialPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(initialPosition.x, initialPosition.y, Mathf.Sin(Time.time) * -3.0f + initialPosition.z);

    }
}
