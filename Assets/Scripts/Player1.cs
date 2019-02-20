using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{

    public float speed;

    private Rigidbody rb;

    void OnCollisionEnter(Collision kick)
    {

        if (kick.gameObject.name == "PongBall")
        {
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }
    }
    void OnCollisionExit(Collision kick)
    {
       
        if (kick.gameObject.name == "PongBall")
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {    
        if (Input.GetKey("w") || Input.GetKey("s"))
        {
            float moveVertical = Input.GetAxis("Vertical2");

            Vector3 movement = new Vector3(0.0f, 0.0f, moveVertical);

            rb.AddForce(movement * speed);

        }
        if (Input.GetKey("a") || Input.GetKey("d"))
        {
            float moveHorizontal = Input.GetAxis("Horizontal2");

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);

            rb.AddForce(movement * speed);

        }
       
    } 
  }   


