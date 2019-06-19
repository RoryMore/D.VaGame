using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed;
    public float turnSpeed;

    public float moveSpeedModifier = 0;
    public float turnSpeedModifier = 0;

    float angle;
    Quaternion targetRotation;
    Vector2 horizontalInput;
    Rigidbody rB;
    Animator anim;

    //Dash Variables
    public float maxDashTime = 1.0f;
    public float dashSpeed = 10.0f;
    public float dashStoppingSpeed = 0.1f;

    private float currentDashTime;
    private Vector3 moveDirection;

    public bool dashing = false;

    // Start is called before the first frame update
    void Start()
    {
        rB = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        currentDashTime = maxDashTime;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        GetInput();

        if (Mathf.Abs(horizontalInput.x) < 1 && Mathf.Abs(horizontalInput.y) < 1)
        {
            //anim.SetBool("Walking", false);
            return;
        }
        else
        {
            //anim.SetBool("Walking", true);
        }
        //-----movement
        CalculateDirection();
        Rotate();
        Move();

     
        Dash();
        

        moveSpeedModifier = PlayerModifierManager.Instance.GetMoveSpeed();
        turnSpeedModifier = PlayerModifierManager.Instance.GetTurnSpeed();
        //-----
    }

    void GetInput()
    {
        horizontalInput.x = Input.GetAxisRaw("Horizontal");
        horizontalInput.y = Input.GetAxisRaw("Vertical");
    }

    void Move()
    {
        transform.parent.transform.position += transform.forward * (moveSpeed * moveSpeedModifier) * Time.deltaTime;
    }

    void CalculateDirection()
    {
        angle = Mathf.Atan2(horizontalInput.x, horizontalInput.y);
        angle = Mathf.Rad2Deg * angle;
    }

    void Rotate()
    {
        targetRotation = Quaternion.Euler(0, angle, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, (turnSpeed * turnSpeedModifier));
    }


    void Dash()
    {
        if (PlayerModifierManager.Instance.GetHasDash() == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                currentDashTime = 0.0f;
            }
            if (currentDashTime < maxDashTime)
            {
                moveDirection = transform.forward * dashSpeed;
                currentDashTime += dashStoppingSpeed;
                dashing = true;
            }
            else
            {
                moveDirection = Vector3.zero;
                dashing = false;
            }
            transform.parent.transform.position += moveDirection * Time.deltaTime;
        }
    }

       
}
