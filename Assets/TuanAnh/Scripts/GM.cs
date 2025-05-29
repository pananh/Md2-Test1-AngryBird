using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class GM : MonoBehaviour
{
    public static GM instance;

   
    private int score;
    public void AddScore(int value)
    {
        score += value;
        UIManager.instance.UpdateScore(score);
    }


    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        score = 0;
      
        DrawMouse.instance.Init();
        UIManager.instance.Init();

    }


}
