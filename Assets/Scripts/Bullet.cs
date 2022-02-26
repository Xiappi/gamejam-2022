using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("3");
        if (collision.CompareTag("Wall"))
        {
            Debug.Log("1");
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Enemy"))
        {
            Debug.Log("2");
            Destroy(gameObject);
        }

        

    }
}
