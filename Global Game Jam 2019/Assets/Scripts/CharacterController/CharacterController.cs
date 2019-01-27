using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    
    public GameController.InputType inputType;


    public float speed;
    float distToGround;
    Rigidbody playerRigidbody;
    public bool canMove = true;
    float moveMagnitude;
    float strafeAmount;
    Vector3 camForward;
    bool strafing;
    Quaternion mouseRotation;
    Animator anim;
    int forward = 1;

    float moveHorizontal;
    float moveVertical;
    float rotateHorizontal;
    float rotateVertical;
    

    GameController gameController;
    Vector3 playerDirection;
    Vector3 mouseDirection;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        gameController = FindObjectOfType<GameController>();
        inputType = gameController.GetControlls();
    }

    // Update is called once per frame
    void Update()
    {
        strafing = Input.GetButton("Aiming");        

        if (canMove)
            Move();

        SetAnimation();

    }

    private void FixedUpdate()
    {
        
       

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
                case GameController.InputType.controller:
                    HandleControllerRotation();
                    break;
                case GameController.InputType.mouse:
                    HandleMouseRotation();
                    break;
            }
        }
        else
        {
            forward = 1;
            playerDirection = Vector3.right * moveHorizontal + Vector3.forward * moveVertical;
            if (playerDirection.sqrMagnitude > 0.0f)
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(playerDirection, Vector3.up), 0.1f);
        }

        
        Vector3 movement = Vector3.right * moveHorizontal + Vector3.forward * moveVertical;
        Vector3 clampedMovement = Vector3.ClampMagnitude(movement, 1);
        Vector3 moveVelocity = new Vector3(clampedMovement.x * speed, playerRigidbody.velocity.y, clampedMovement.z * speed);

        playerRigidbody.velocity = moveVelocity;
    }


    //Handle Strafe Movements With Controller
    void HandleControllerRotation()
    {
        playerDirection = Vector3.right * rotateHorizontal + Vector3.forward * rotateVertical;
        if (playerDirection.sqrMagnitude > 0.0f)
        {
            transform.Rotate(0, rotateVertical, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(playerDirection, Vector3.up), 0.1f);
        }
    }


    //Handle Strafe Movements With Mouse
    void HandleMouseRotation()
    {
        Vector3 mousePos = new Vector3(0, 0, 0);
      

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
        Vector3 moveDirWorld = new Vector3(moveHorizontal, 0, moveVertical);
        float animV = Vector3.Dot( transform.forward, moveDirWorld);
        float animH = Vector3.Dot( transform.right, moveDirWorld);
        

        anim.SetFloat("Forward", animV);
        anim.SetFloat("Sidewards", animH);
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }
}
