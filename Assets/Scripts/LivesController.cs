using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LivesController : MonoBehaviour
{
    [SerializeField] public int Lives = 3;
    private Text livesText;

    void Start()
    {
        livesText = GetComponentInChildren<Text>();
    }
    public int UpdateLives(int n)
    {
        Lives += n;
        livesText.text = $"x {Lives}";

        return Lives;
    }
}
