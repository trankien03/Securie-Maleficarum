using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    private Animator anim;
    [Header("CharacterStats")]
    [SerializeField] private int speedValue = 5;
    [SerializeField] private int jumpValue = 7;
    [SerializeField] private float xInput;

    [Header("Dash info")]
    [SerializeField] private float dashSpeed = 25;
    [SerializeField] private float dashDuration = 0.25f;
    [SerializeField] private float dashTime = 0;
    [SerializeField] private float dashCD = 0.4f;
    [SerializeField] private float dashCDtimer = 0;

    [Header("Attack info")]
    [SerializeField] private float comboTime = 0.7f;
    private float comboTimeCounter = 0;
    [SerializeField] private bool isAttacking;
    [SerializeField] private int comboCounter = 1;


    public float dashDir { get; private set; }

    [Header("Collision Info")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    private bool isGrounded;



    private float facingDir = 1;
    private bool isFacingRight = true;
    /////////////////////
    private bool isMoving = false;




    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ColisionCheck();
        //flip controlling
        FlipController();
        checkInput();
        ///Run
        Move();
        AnimatorController();


        dashTime -= Time.deltaTime;
        dashCDtimer -= Time.deltaTime;
        comboTimeCounter -= Time.deltaTime;
        if (comboTimeCounter < 0)
        {
            comboCounter = 1;
        }


    }

    ////////////////////////////////
    ///ControllerZone///////////////
    ////////////////////////////////

    //Animator controll
    private void AnimatorController()
    {
        isMoving = xInput != 0;
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isDashing", dashTime > 0);
        anim.SetBool("isAttacking", isAttacking);
        anim.SetInteger("comboCounter", comboCounter);
    }
    ////////////////////////////////
    private void checkInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.Mouse0)){
            isAttacking = true;
            comboTimeCounter = comboTime;

        }

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.Space)) && isGrounded)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Dash();
        }
    }
    public void attackOver()
    {
        isAttacking = false;
        Debug.Log("Attack Over");
        comboCounter ++;
        if (comboCounter > 3 )
        {
            comboCounter = 1;
        }
    }
    //Dash code
    private void Dash()
    {
        if  (dashCDtimer < 0)
        {
            dashCDtimer = dashCD;
            dashTime = dashDuration;
        }
        
    }

    //Move Code
    private void Move()
    {
        if (dashTime > 0)
        {
            rb.velocity = new Vector2(xInput * dashSpeed, 0);
        }
        else
        {
            rb.velocity = new Vector2(xInput * speedValue, rb.velocity.y);
        }
        
    }
    ////////////////////////////////
    //Jump code
    private void Jump()
    {
        rb.velocity = new Vector2(xInput * speedValue, jumpValue);
    }
    private void ColisionCheck()
    {
        
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y-groundCheckDistance));
    }
    ////////////////////////////////
    //Flip code
    private void Flip()
    {
        facingDir = facingDir * -1;
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180, 0);
    }

    private void FlipController()
    {
        if (xInput > 0 && !isFacingRight)
        {
            Flip();
        }
        if (xInput < 0 && isFacingRight)
        {
            Flip();
        }
    }
    ////////////////////////////////

}


