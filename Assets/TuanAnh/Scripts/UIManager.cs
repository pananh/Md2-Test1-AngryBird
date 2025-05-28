using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI statusText;


    void Awake()
    {
        instance = this;
    }

    public void Init()
    {
        UpdateScore(0);
        statusText.text = "Pull the bow";
    }
    void Update()
    {


    }

    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void UpdateStatus(string status)
    {
        statusText.text = status;
    }
}
