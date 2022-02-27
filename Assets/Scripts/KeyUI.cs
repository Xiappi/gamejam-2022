using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyUI : MonoBehaviour
{
    public Text keyText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int keys = FindObjectOfType<PlayerController>().keyNumber;
        keyText.text = "x " + keys;
    }
}
