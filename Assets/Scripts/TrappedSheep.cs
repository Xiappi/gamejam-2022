using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrappedSheep : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public bool canOpen = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && canOpen){
            FindObjectOfType<LivesController>().UpdateLives(1);
            player.GetComponent<PlayerController>().keyNumber -= 1;
            Destroy(gameObject);
        }        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (player.GetComponent<PlayerController>().keyNumber>0){
            canOpen = true;
        }
            
    }
}
