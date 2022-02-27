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
    private LivesController livesController;

    private bool canMove = true;
    private bool canJump = true;

    private bool onGround;

    private const float _hitTimeout = 0.5f;
    private const float _reloadDelay = 3f;
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
        if (!canMove) return;

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
            if (canJump && onGround && canMove)
            {
                canJump = false;
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
            onGround = true;
            canJump = true;
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
        // player still has lives
        if (livesController.UpdateLives(-1) > 0)
        {
            canMove = false;
            animator.SetTrigger("Hurt");
            StartCoroutine(knockbackTimer());
            return;
        }

        // player dies
        LevelFailed();

    }

    private void LevelFailed()
    {
        canMove = false;
        animator.SetTrigger("Death");
        StartCoroutine(ReloadScene());
    }


    private IEnumerator knockbackTimer()
    {
        yield return new WaitForSeconds(_hitTimeout);
        canMove = true;
    }

    private IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(_reloadDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
