using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float rayDistance = 5f;
    [SerializeField] private float slopeHitDistance;
    public Rigidbody2D playerRigid;

    [field:SerializeField]
    public Rigidbody2D frontWheelRigid { get; set; }
    [field: SerializeField]
    public Rigidbody2D backWheelRigid { get; set; }

    public float walkForce;
    public float maxWalkForce;
    public float jumpForce;
    public float time;
    public float rotateInterpolationFactor;
    public Vector3 velocity;
    private Vector3 upDirection;


    [SerializeField] public WheelJoint2D backWheel;
    public JointMotor2D backMoter { get; set; }

    float playerAngle;
    private float boostCheck=0;
    
    public bool isMove;
    public bool isJump;
    public bool isStop;
    public bool isBoost;
    public bool isGrounded;

    private RaycastHit2D raycastHit;
    private RaycastHit2D rightSlopeHit;
    private RaycastHit2D leftSlopeHit;

    public Transform childAngle;
    public int anglePos;
    public int airRotateForce;

    private float boostTimer;
    [SerializeField] private float boostTime;
    [SerializeField] private float boostForce;
    private IPlayerState moveState,jumpState, rotateState, stopState, idleState;
    private PlayerStateContext playerStateContext;

    [SerializeField] private ParticleSystem boostParticle;
    [SerializeField] private ParticleSystem flipParticle;

    private GameObject playerOnTerrain;
    public GameObject PlayerOnTerrain => playerOnTerrain;

    private void Start()
    {
        playerRigid = GetComponent<Rigidbody2D>();
        velocity=transform.right;
        upDirection = new Vector3(0, 1, 0);
        backMoter = backWheel.motor;

        playerStateContext = new PlayerStateContext(this);
        moveState = GetComponent<PlayerMoveState>();
        jumpState = GetComponent<PlayerJumpState>();
        rotateState = GetComponent<PlayerRotateState>();
        stopState= GetComponent<PlayerStopState>();
        idleState = GetComponent<PlayerIdleState>();
    }

    public void MovePlayer()
    {
        playerStateContext.Transition(moveState);
        
    }

    public void RotatePlayer()
    {
        playerStateContext.Transition(rotateState);
    }

    public void StopPlayer()
    {
        playerStateContext.Transition(stopState);
    }

    public void IdlePlayer()
    {
        playerStateContext.Transition(idleState);
    }

    public void JumpPlayer()
    {
        playerStateContext.Transition(jumpState);
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
            boostCheck = 0;
            isGrounded = true;
        }

        if (!rightSlopeHit && !leftSlopeHit)
        {
            isGrounded = false;
        }
    }
    

    private void SetJumpFlag()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            isMove = true;
        }

        if (!Input.GetKey(KeyCode.RightArrow))
        {
            isMove = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            isJump = true;
        }
    }

    public void SetBoostCheck(float value)
    {
        boostCheck += value;
        if (boostCheck >= 180)
        {
            flipParticle.Play();
            boostTimer = boostTime;
            isBoost = true;
            boostCheck = 0;
           
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

            Quaternion targetRotation = Quaternion.Euler(0, 0, playerAngle);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime*rotateInterpolationFactor);
            transform.localEulerAngles = new Vector3(0, 0, playerAngle);
        }

        if (!isGrounded)
        {
            transform.localEulerAngles += new Vector3(0, 0, -0.01f * airRotateForce);
            SetBoostCheck(-0.01f * airRotateForce);
            playerRigid.constraints = RigidbodyConstraints2D.None;
        }
        else
        {
            playerRigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

    }

    private void ActivateBoost()
    {
        if (isBoost&&isGrounded)
        {
            if (!boostParticle.isPlaying) {
                boostParticle.Play();
            }

            if (playerRigid.velocity.x >= 0)
            {
                playerRigid.velocity += playerRigid.velocity.normalized * boostForce * Time.deltaTime;
            }
            
            boostTimer -= Time.deltaTime;
            if (boostTimer <= 0)
            {
                boostParticle.Stop();
                isBoost = false;
            }
        }
    }

   
    private void PlayerState()
    {

        if (isGrounded && isJump)
        {
            JumpPlayer();
            return;
        }


        if (isGrounded&&isMove)
        {
            MovePlayer();
            return;
        }


        if (!isGrounded)
        {
            RotatePlayer();
            return;
        }

        if (isStop)
        {
            StopPlayer();
            return;
        }

        IdlePlayer();


    }

    void FixedUpdate()
    {
       
        SetVelocity();
        PlayerState();

    }

    void Update()
    {
        SetRaycastHit();
        SetSlopeHit();
        SetJumpFlag();
        //ChangePlayerAngle();
    }

    private void LateUpdate()
    {
        ActivateBoost();
    }
}
