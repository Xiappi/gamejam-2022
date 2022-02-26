using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour
{
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MouseFacing();
    }

    void MouseFacing()
    {
        var dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        if(dir.x>0){
            transform.position = new Vector3(player.position.x+0.5f, player.position.y, player.position.z);
        }
        if(dir.x<0){
            transform.position = new Vector3(player.position.x-0.5f, player.position.y, player.position.z);
        }
    }
}
