using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public enum InputType { mouse, controller };
    public InputType inputType;


    public float speed;
    float distToGround;
    Rigidbody playerRigidbody;
    public bool canMove = true;
    float moveMagnitude;
    Vector3 camForward;
    bool strafing;
    Quaternion mouseRotation;
    Animator anim;

    float moveHorizontal;
    float moveVertical;
    float rotateHorizontal;
    float rotateVertical;
    

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        strafing = Input.GetButton("Aiming");

    

    }

    private void FixedUpdate()
    {
        if (canMove)
            Move();

        SetAnimation();
    }



    void Move()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
        rotateHorizontal = Input.GetAxis("HorizontalRight");
        rotateVertical = Input.GetAxis("VerticalRight");

        //camForward = Vector3.Scale(camera.forward, new Vector3(1, 0, 1)).normalized;

        moveMagnitude = Mathf.Clamp01(new Vector2(moveHorizontal, moveVertical).magnitude);       

        if (strafing)
        {
            switch (inputType)
            {
                case InputType.controller:
                    HandleControllerRotation();
                    break;
                case InputType.mouse:
                    HandleMouseRotation();
                    break;
            }
        }
        else
        {
            Vector3 playerDirection = Vector3.right * moveHorizontal + Vector3.forward * moveVertical;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(playerDirection, Vector3.up), 0.1f);
        }

        Vector3 movement = Vector3.right * moveHorizontal + Vector3.forward * moveVertical;
        Vector3 clampedMovement = Vector3.ClampMagnitude(movement, 1);
        Vector3 moveVelocity = clampedMovement * speed;

        playerRigidbody.velocity = moveVelocity;
    }

    void HandleControllerRotation()
    {
        Vector3 playerDirection = Vector3.right * rotateHorizontal + Vector3.forward * rotateVertical;
        if (playerDirection.sqrMagnitude > 0.0f)
        {
            transform.Rotate(0, rotateVertical, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(playerDirection, Vector3.up), 0.1f);
        }
    }

    void HandleMouseRotation()
    {
        Vector3 mousePos = new Vector3(0, 0, 0);
        Vector3 mouseDirection;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            mousePos = hit.point;
        }

        mouseDirection = mousePos - transform.position;

        // transform.rotation = Quaternion.Euler(mouseDirection);
        if (mouseDirection.sqrMagnitude > 0.0f)
        {
            transform.LookAt((new Vector3(mouseDirection.x, transform.position.y, mouseDirection.z)) + new Vector3(transform.position.x, 0, transform.position.z));
        }

    }

    void SetAnimation()
    {
        anim.SetFloat("Speed", moveMagnitude);
    }
}
