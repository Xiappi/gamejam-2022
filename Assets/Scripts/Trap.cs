using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{

    [SerializeField] float moveSpeed = 1f;
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

        playerController.CanMove = false;
        playerController.TakeDamage();
        StartCoroutine(knockbackTimer(0.5f, playerController));

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" )
            DealDamage(other); 
    }
    

    IEnumerator knockbackTimer(float x, PlayerController pc)
    {
        yield return new WaitForSeconds(x);
        pc.CanMove = true;
    }
}
