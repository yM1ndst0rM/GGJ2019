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

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
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

        moveMagnitude = Mathf.Clamp01(new Vector2(moveHorizontal, moveVertical).magnitude);
        Vector3 playerDirection = Vector3.right * moveHorizontal + Vector3.forward * moveVertical;

        if (playerDirection.sqrMagnitude > 0.0f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(playerDirection, Vector3.up), 0.1f);
        }

        float velocityY = playerRigidbody.velocity.y;
        if (velocityY > 0.2f)
            velocityY = 0.2f;

        Vector3 movement = new Vector3(moveHorizontal, velocityY, moveVertical);
        Vector3 clampedMovement = Vector3.ClampMagnitude(movement, 1);
        Vector3 moveVelocity = clampedMovement * speed;

        playerRigidbody.velocity = moveVelocity;
    }
}
