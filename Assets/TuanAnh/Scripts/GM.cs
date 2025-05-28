using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class GM : MonoBehaviour
{
    public static GM instance;
    private bool needMouseVector;
    public bool NeedMouseVector
    {
        get { return needMouseVector; }
        set { needMouseVector = value; }
    }
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
        needMouseVector = true;
        DrawMouse.instance.Init();
        //Bird.instance.Init();
        UIManager.instance.Init();

    }

    // Update is called once per frame
    void Update()
    {
        MoveBird();

    }

    
    private void MoveBird()
    {
        if ( (DrawMouse.instance.HavingMouseVector) && needMouseVector )
        {
            Bird.instance.StartFlying = true;
            Bird.instance.Direction = DrawMouse.instance.MouseVector;
            needMouseVector = false;
        }
    }



}
