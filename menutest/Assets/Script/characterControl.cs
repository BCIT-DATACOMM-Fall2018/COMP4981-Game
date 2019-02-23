using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterControl : MonoBehaviour
{
    public Animator animateObject;
    public float inputH;
    public float inputV;
    public Rigidbody rbody;

    // Start is called before the first frame update
    void Start()
    {
        animateObject = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("w"))
        {
            print("pressing 1");
        }

        inputH = Input.GetAxis("Horizontal");
        inputV = Input.GetAxis("Vertical");

        animateObject.SetFloat("inputH", inputH);
        animateObject.SetFloat("inputV", inputV);

        float moveX = inputH * 600f * Time.deltaTime;
        float moveZ = inputV * 600f * Time.deltaTime;

        rbody.velocity = new Vector3(moveX, 0f, moveZ);

    }
}
