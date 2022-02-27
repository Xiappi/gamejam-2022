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
    private BoxCollider2D attackHitbox;
    private Knockback knockback;
    private SoundController soundController;
    public bool canMove = true;
    private int jumpTime;
    private const float _hitTimeout = 0.5f;
    private const float _reloadDelay = 3f;
    public int keyNumber = 0;
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        livesController = FindObjectOfType<LivesController>();
        attackHitbox = GetComponentInChildren<BoxCollider2D>();
        knockback = GetComponent<Knockback>();
        soundController = FindObjectOfType<SoundController>();

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
        Attack();

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
        if (!canMove)
            return;

        if (Input.GetButtonDown("Jump"))
        {
            if (jumpTime < 1)
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
            animator.SetBool("Grounded", false);
        }
    }

    private void FallingDetection()
    {
        float airSpeedHorizontal = rb.velocity.y;
        animator.SetFloat("AirSpeedY", airSpeedHorizontal);
    }

    /* 
        Returns true if the player took damage, false otherwise
    */
    public bool TakeDamage()
    {
        // can only take damage when player is in control
        if (!canMove)
            return false;

        soundController.PlayDeathSound();
        animator.SetTrigger("Hurt");
        // player still has lives
        if (livesController.UpdateLives(-1) > 0)
        {
            canMove = false;
            StartCoroutine(knockbackTimer());
            return true;
        }

        // player dies
        LevelFailed();
        return true;
    }

    private void LevelFailed()
    {
        canMove = false;
        animator.SetTrigger("Death");
        StartCoroutine(ReloadScene());
    }

    private IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(_reloadDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator knockbackTimer()
    {
        yield return new WaitForSeconds(_hitTimeout);
        canMove = true;
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("Attack");
            attackHitbox.enabled = true;
            StartCoroutine(attackTimer());
            animator.SetTrigger("Attack1");
            soundController.PlayAttackSound();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Enemy") return;

        // var enemy = other.GetComponent<EnemyMovement>();

        // enemy.TakeDamage();
        // knockback.KnockbackEntity(rb, enemy.GetComponent<Rigidbody2D>());
    }

    IEnumerator attackTimer()
    {
        yield return new WaitForSeconds(0.3f);
        attackHitbox.enabled = false;
    }

}
