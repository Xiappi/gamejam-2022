using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;

    [SerializeField] int health = 2;
    Rigidbody2D rb;
    private BoxCollider2D head;
    private Animator anim;

    private bool isDead = false;
    private Knockback knockback;
    private EdgeCollider2D edgeDetection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        knockback = GetComponent<Knockback>();

        head = GetComponentInChildren<BoxCollider2D>();
        edgeDetection = GetComponentInChildren<EdgeCollider2D>();
    }

    void Update()
    {
        if (!isDead)
        {
            rb.velocity = new Vector2(moveSpeed * transform.localScale.x, 0);
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
        anim.SetFloat("Speed", rb.velocity.magnitude);
    }

    void FixedUpdate()
    {
        if (!edgeDetection.IsTouchingLayers(LayerMask.GetMask("Ground")))
            ChangeDirection();
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player") return;
    }

    private void ChangeDirection()
    {
        transform.localScale = new Vector2(-Mathf.Sign(rb.velocity.x), 1f);
    }


    private void OnCollisionEnter2D(Collision2D other)
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
        FindObjectOfType<SoundController>().PlayWolfSound();
        var playerRb = other.gameObject.GetComponent<Rigidbody2D>();
        var playerController = other.gameObject.GetComponent<PlayerController>();

        if (playerController.TakeDamage())
        {
            head.enabled = false;
            anim.SetTrigger("Attack");
            StartCoroutine(DisableHead());
            knockback.KnockbackEntity(rb, playerRb);
        }

    }

    // attacking and head collision is causing issues, hacky fix
    private IEnumerator DisableHead()
    {
        yield return new WaitForSeconds(0.3f);
        head.enabled = true;
    }

    public void TakeDamage()
    {
        health--;
    }

}
