using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player  : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransform = null;
    [SerializeField] LayerMask playerMask;
    private int walkSpeed = 4;
    private float jumpForce = 7.0f;
    private float jumpForceMultiplier = 1.0f;
    private bool jumpKeyWasPressed;
    private float horizontalInput;
    private Rigidbody rigidbodyComponent;
    private int superJumpsRemaining = 0;

    // Start is called before the first frame update
    void Start()
    {
      rigidbodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
      if ( Input.GetKeyDown(KeyCode.Space) == true )
      {
        jumpKeyWasPressed = true;
      }
      horizontalInput = Input.GetAxis("Horizontal"); // Look in input manager
    }
    // Fixed Update is called once every physics Update
    // Physics updates at 100 hz (100 times per second)
    private void FixedUpdate()
    {
      rigidbodyComponent.velocity = new Vector3(horizontalInput * walkSpeed, rigidbodyComponent.velocity.y, 0);

      if ( Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0 )
      {
        return;
      }
      if ( jumpKeyWasPressed == true )
      {
        if (superJumpsRemaining > 0){
          jumpForceMultiplier = 1.3f;
          superJumpsRemaining --;
        }
        rigidbodyComponent.AddForce(Vector3.up * jumpForce * jumpForceMultiplier, ForceMode.VelocityChange);
        jumpKeyWasPressed = false;
        jumpForceMultiplier = 1;
      }
    }
    private void OnTriggerEnter(Collider other)
    {
      if (other.gameObject.layer == 9)
      {
        Destroy(other.gameObject);
        superJumpsRemaining ++;
      }
    }

}
