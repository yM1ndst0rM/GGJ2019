using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float speed;
    float distToGround;
    Rigidbody playerRigidbody;
    public bool canMove = true;
    float moveMagnitude;
    Transform camera;
    Vector3 camForward;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        if (Camera.main != null)
        {
            camera = Camera.main.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (canMove)
            Move();
    }

    void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        camForward = Vector3.Scale(camera.forward, new Vector3(1, 0, 1)).normalized;


        moveMagnitude = Mathf.Clamp01(new Vector2(moveHorizontal, moveVertical).magnitude);
        
        //Player Rotation related to Camera Rotation
        Vector3 playerDirection = moveVertical * camForward + moveHorizontal * camera.right;
        if (playerDirection.sqrMagnitude > 0.0f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(playerDirection, Vector3.up), 0.1f);
        } 

        float velocityY = playerRigidbody.velocity.y;
        if (velocityY > 0.2f)
            velocityY = 0.2f;
                         
        //Apply Movement related to Camera Rotation
        Vector3 movement = moveVertical * camForward + moveHorizontal * camera.right;
        Vector3 clampedMovement = Vector3.ClampMagnitude(movement, 1);
        Vector3 moveVelocity = clampedMovement * speed;

        playerRigidbody.velocity = moveVelocity;
    }
}
