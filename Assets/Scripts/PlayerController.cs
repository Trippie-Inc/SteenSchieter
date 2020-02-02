﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float wasdSpeed;
    public float mouseSpeed;
    public float tilt;
    public Boundary boundary;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireDelta = 0.5F;

    private float nextFire = 0.5F;
    private float myTime = 0.0F;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        myTime = myTime + Time.deltaTime;

        if ((Input.GetButton("Fire1") && myTime > nextFire) || (Input.GetKey(KeyCode.Space) && myTime > nextFire))
        {
            nextFire = myTime + fireDelta;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            nextFire = nextFire - myTime;
            myTime = 0.0F;
            GetComponent<AudioSource>().Play();
        }
    }

    void FixedUpdate() {
        float moveHorizontal = Input.GetAxis("Mouse X");
        float moveHorizontal2 = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Mouse Y");
        float moveVertical2 = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        Vector3 movement2 = new Vector3(moveHorizontal2, 0.0f, moveVertical2);
        rb.velocity = (movement + (movement2 * wasdSpeed)) * mouseSpeed;

        rb.position = new Vector3
        (
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax), 
            0.0f,
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }
}
