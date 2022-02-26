using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5;
    public float JumpSpeed = 5;
    private Rigidbody2D rb;
    private BoxCollider2D myFeet;
    private bool isGround;
    private Image[] hearts;
    private LivesController livesController;

    public bool CanMove = true;
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        myFeet = GetComponent<BoxCollider2D>();
        livesController = FindObjectOfType<LivesController>();

    }
    void Update()
    {
        Jump();
    }
    void FixedUpdate()
    {
        Run();
    }

    void Run()
    {
        if (!CanMove) return;

        float moveDir = Input.GetAxisRaw("Horizontal");
        Vector2 PlayerVel = new Vector2(moveDir * moveSpeed, rb.velocity.y);
        rb.velocity = PlayerVel;
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            CheckGrounded();
            if (isGround)
            {
                Vector2 jumVel = new Vector2(0.0f, JumpSpeed);
                rb.velocity = Vector2.up * jumVel;
            }

        }
    }

    void CheckGrounded()
    {
        isGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }

    public void TakeDamage()
    {
        if (livesController.UpdateLives(-1) <= 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
