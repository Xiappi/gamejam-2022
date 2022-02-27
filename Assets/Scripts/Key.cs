using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{

    public GameObject player;

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        player.GetComponent<PlayerController>().keyNumber += 1;
        Destroy(gameObject);
            
    }
}
