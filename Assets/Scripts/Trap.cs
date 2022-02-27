using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private Knockback knockback;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        knockback = GetComponent<Knockback>();

    }




    public void DealDamage(Collider2D other)
    {
        var playerRb = other.gameObject.GetComponent<Rigidbody2D>();
        var playerController = other.gameObject.GetComponent<PlayerController>();

        knockback.KnockbackEntity(rb, playerRb);

        playerController.canMove = false;
        playerController.TakeDamage();

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
            DealDamage(other);
    }
}
