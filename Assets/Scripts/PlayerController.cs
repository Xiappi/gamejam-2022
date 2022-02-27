using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5;
    public float JumpSpeed = 5;
    public Transform rootL;
    public Transform rootR;

    private Animator animator;
    private Rigidbody2D rb;
    private bool onGround;
    private LivesController livesController;

    public bool CanMove = true;
    private bool canJump = true;
    private int jumpTime;
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        livesController = FindObjectOfType<LivesController>();

    }
    void Update()
    {
        Jump();
        GroundDetection();
        FallingDetection();
    }
    void FixedUpdate()
    {
        Run();

        float playerSpeed = Mathf.Abs(rb.velocity.x);

        if (playerSpeed >= 0.3f)
        {
            animator.SetInteger("AnimState", 1);
        }
        else
        {
            animator.SetInteger("AnimState", 0);
        }

    }

    void Run()
    {
        if (!CanMove) return;

        float moveDir = Input.GetAxisRaw("Horizontal");
        Vector2 PlayerVel = new Vector2(moveDir * moveSpeed, rb.velocity.y);
        rb.velocity = PlayerVel;

        if (moveDir > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        if (moveDir < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (jumpTime<1)
            {
                jumpTime += 1;
                Vector2 jumVel = new Vector2(0.0f, JumpSpeed);
                rb.velocity = Vector2.up * jumVel;

                animator.SetTrigger("Jump");
            }

        }
    }

    private void GroundDetection()
    {
        RaycastHit2D hit = Physics2D.Linecast(rootL.position, rootR.position);
        Debug.DrawLine(rootL.position, rootR.position, Color.red);

        if (hit.collider != null)
        {
            jumpTime = 0;
            animator.SetBool("Grounded", true);
        }
        else
        {
            onGround = false;
            animator.SetBool("Grounded", false);
        }
    }

    private void FallingDetection()
    {
        float airSpeedHorizontal = rb.velocity.y;
        animator.SetFloat("AirSpeedY", airSpeedHorizontal);
    }

    public void TakeDamage()
    {
        if (livesController.UpdateLives(-1) <= 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
