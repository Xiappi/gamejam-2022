using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] float XRebound = 10f;
    [SerializeField] float YRebound = 10f;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DealDamage(Collider2D other)
    {
        var playerRb = other.gameObject.GetComponent<Rigidbody2D>();
        var playerController = other.gameObject.GetComponent<PlayerController>();

        var direction = Mathf.Abs(rb.worldCenterOfMass.x) - Mathf.Abs(playerRb.worldCenterOfMass.x);
        var vector = new Vector2(XRebound * Mathf.Sign(direction), YRebound);
        playerRb.velocity = vector;

        playerController.canMove = false;
        playerController.TakeDamage();

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" )
            DealDamage(other); 
    }
}
