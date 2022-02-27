using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float XRebound = 10f;
    [SerializeField] float YRebound = 10f;

    Rigidbody2D rb;
    private BoxCollider2D head;
    private Animator anim;

    private bool isDead = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        head = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isDead)
        {
            rb.velocity = new Vector2(moveSpeed * transform.localScale.x, 0);
        } else
        {
            rb.velocity = new Vector2(0,0);
        }
        anim.SetFloat("Speed", rb.velocity.magnitude);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player") return;


        moveSpeed = -moveSpeed;
        transform.localScale = new Vector2(-Mathf.Sign(rb.velocity.x), 1f);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && !head.IsTouchingLayers(LayerMask.GetMask("Player")))
            DealDamage(other);

        if (head.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            other.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up * 10f;
            anim.SetTrigger("Death");
            isDead = true;
            Destroy(gameObject, 2);
        }
            
    }

    public void DealDamage(Collision2D other)
    {
        if (isDead) return;

        anim.SetTrigger("Attack");
        var playerRb = other.gameObject.GetComponent<Rigidbody2D>();
        var playerController = other.gameObject.GetComponent<PlayerController>();

        var direction = Mathf.Abs(rb.worldCenterOfMass.x) - Mathf.Abs(playerRb.worldCenterOfMass.x);
        var vector = new Vector2(XRebound * Mathf.Sign(direction), YRebound);
        playerRb.velocity = vector;
        playerController.TakeDamage();
    }

}
