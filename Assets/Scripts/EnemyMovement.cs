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
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        head = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        rb.velocity = new Vector2(moveSpeed * transform.localScale.x, 0);
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
            Destroy(gameObject);

    }

    public void DealDamage(Collision2D other)
    {

        var playerRb = other.gameObject.GetComponent<Rigidbody2D>();
        var playerController = other.gameObject.GetComponent<PlayerController>();

        var direction = Mathf.Abs(rb.worldCenterOfMass.x) - Mathf.Abs(playerRb.worldCenterOfMass.x);
        var vector = new Vector2(XRebound * Mathf.Sign(direction), YRebound);
        playerRb.velocity = vector;
        playerController.TakeDamage();
    }

}
