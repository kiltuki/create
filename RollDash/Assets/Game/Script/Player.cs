using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    Rigidbody rb;
    public int moveSpeed = 2;
    private Vector3 position;
    private Vector3 screenToWorldPointPosition;

    void Start()
    {
        //GetComponentの処理をキャッシュしておく
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        // float moveVertical = Input.GetAxis("Vertical");

        // Vector3 movement = new Vector3(0.0f, 0.0f, 0.3f*moveHorizontal*(-1));//出力用
        Vector3 movement = new Vector3(0.0f, 0.0f, 1.5f * moveHorizontal * (-1));//内部用
        rb.AddForce(movement * moveSpeed);
        rb.velocity = new Vector3(moveSpeed, rb.velocity.y, rb.velocity.z);
    }


}
