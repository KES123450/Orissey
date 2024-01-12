using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float rayDistance = 5f;
    public float slopeHitDistance;
    public Rigidbody2D rigid;
    public float jumpForce;
    public float doubleJumpForce;
    public float walkForce;
    public float maxWalkForce;
    public float gravityForce = 5f;
    public float time;
    public float rotateInterpolationFactor;
    public Vector3 velocity;
    public Vector3 gravity;
    private Vector3 upDirection;

    float playerAngle;
    public float boostCheck=0;
    
    public AnimationCurve jumpAnimation;
    public float GoalTime;
    public bool isJump; // 점프중인지 체크
    public bool isDoubleJump;
    public bool isMove;
    public bool isStop;
    public bool isBoost;
    public bool isGrounded;
    private bool isSlope;
    private RaycastHit2D raycastHit;
    private RaycastHit2D rightSlopeHit;
    private RaycastHit2D leftSlopeHit;

    public Transform childAngle;
    public int anglePos;
    public int airRotateForce;


    private IPlayerState moveState, boostState,jumpState, doubleJumpState, rotateState, stopState;
    private PlayerStateContext playerStateContext;

    void Start()
    {
        gravity = Vector3.down * gravityForce;
        rigid = GetComponent<Rigidbody2D>();
        childAngle = transform.GetChild(0);
        velocity=transform.right;
        upDirection = new Vector3(0, 1, 0);

        playerStateContext = new PlayerStateContext(this);
        moveState = gameObject.AddComponent<PlayerMoveState>();
        boostState= gameObject.AddComponent<PlayerBoostState>();
        jumpState = gameObject.AddComponent<PlayerJumpState>();
        doubleJumpState = gameObject.AddComponent<PlayerDoubleJumpState>();
        rotateState = gameObject.AddComponent<PlayerRotateState>();
        stopState= gameObject.AddComponent<PlayerStopState>();
    }

    public void MovePlayer()
    {
        playerStateContext.Transition(moveState);
    }
    public void BoostPlayer()
    {
        playerStateContext.Transition(boostState);
    }

    public void JumpPlayer()
    {
        playerStateContext.Transition(jumpState);
    }

    public void DoubleJumpPlayer()
    {
        playerStateContext.Transition(doubleJumpState);
    }

    public void RotatePlayer()
    {
        playerStateContext.Transition(rotateState);
    }

    public void StopPlayer()
    {
        playerStateContext.Transition(stopState);
    }

    public Vector3 AdjustDirectionToSlope(Vector3 direction)
    {
        Vector3 adjustVelocityDirection = Vector3.ProjectOnPlane(direction, raycastHit.normal).normalized;
        return adjustVelocityDirection;
    }

    public void SetStop()
    {
        isStop = true;
    }
    private void SetVelocity()
    {
        if (raycastHit)
        {
            velocity = AdjustDirectionToSlope(velocity).normalized;
        }
        
    }

    private void SetRaycastHit()
    {
        raycastHit = Physics2D.Raycast(transform.position, Vector3.down, rayDistance, 1 << 6);
        Debug.DrawRay(transform.position, Vector3.down * rayDistance, Color.red);
    }

    private void SetSlopeHit()
    {
        rightSlopeHit = Physics2D.Raycast(transform.position+new Vector3(0.61f, -0.5f, 0), -transform.up, slopeHitDistance, 1 << 6);
        leftSlopeHit = Physics2D.Raycast(transform.position + new Vector3(-0.6f, -0.5f, 0), -transform.up, slopeHitDistance, 1 << 6);
        
        Debug.DrawRay(transform.position + new Vector3(0.62f, -0.5f, 0), -transform.up * slopeHitDistance, Color.cyan);
        Debug.DrawRay(transform.position + new Vector3(-0.6f, -0.5f, 0), -transform.up * slopeHitDistance, Color.cyan);

        if (rightSlopeHit || leftSlopeHit)
        {
            isGrounded = true;
        }

        if (!rightSlopeHit && !leftSlopeHit)
        {
            isGrounded = false;
        }
    }
    

    private void SetJumpFlag()
    {
        /*if (Input.GetKeyDown(KeyCode.Space) && raycastHit && isDoubleJump == false)
        {
            isJump = true;
            isBoost = false;
            boostCheck = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isJump == true && isDoubleJump == false)
        {
            isDoubleJump = true;
            isJump = false;
            time = 0;
        }*/

        if (Input.GetKey(KeyCode.RightArrow))
        {
            isMove = true;
        }

        if (!Input.GetKey(KeyCode.RightArrow))
        {
            isMove = false;
        }
    }

    private void ChangePlayerAngle()
    {
        if (isGrounded)
        {
            if (raycastHit.normal.x >= 0)
            {
                playerAngle = -Vector3.Angle(upDirection, raycastHit.normal);
            }
            else
            {
                playerAngle = Vector3.Angle(upDirection, raycastHit.normal);
            }
            /*Quaternion targetRotation = Quaternion.Euler(0, 0, playerAngle);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime*rotateInterpolationFactor);*/
            transform.localEulerAngles = new Vector3(0, 0, playerAngle);
        }

        if (!isGrounded)
        {
            transform.localEulerAngles += new Vector3(0, 0, -0.01f * airRotateForce);
            rigid.constraints = RigidbodyConstraints2D.None;
        }
        else
        {
            rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

    }

    private void ElevatePlayer()
    {
        if (rightSlopeHit)
        {
            Vector3 playerPos = transform.position;
            playerPos.y += rightSlopeHit.distance;
            transform.localPosition = playerPos;
            return;
        }
        if (leftSlopeHit)
        {
            Vector3 playerPos = transform.position;
            playerPos.y += leftSlopeHit.distance;
            transform.localPosition = playerPos;
            return;
        }
    }
    private void PlayerState()
    {
        if (isGrounded&&isMove)
        {
            MovePlayer();
        }

        if (!isJump && !isDoubleJump && isBoost)
        {
            BoostPlayer();
        }

        if (!isGrounded)
        {
            RotatePlayer();
        }
/*
        if (isJump)
        {
            JumpPlayer();
        }

        if (isDoubleJump)
        {
            DoubleJumpPlayer();
        }
*/
        if (isStop)
        {
            StopPlayer();
        }
    }

    void FixedUpdate()
    {
       
        SetVelocity();
        PlayerState();

    }

    void Update()
    {
        ElevatePlayer();
        SetRaycastHit();
        SetSlopeHit();
        SetJumpFlag();
        ChangePlayerAngle();
    }
}
