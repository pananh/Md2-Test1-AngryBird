using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public static Bird instance;
    [SerializeField] LineRenderer lineRenderer;

    private Vector3 direction;
    public Vector3 Direction
    {
        get { return direction; }
        set { direction = value; }
    }
    private float speed;
   
    private bool startFlying, isFlying, fliedDestination;
    public bool StartFlying
    {
        get { return startFlying; }
        set { startFlying = value; }
    }

    private Vector3 startPos, endPos;
    private float startTime, totalTime;
    private float minDistance;

    void Awake()
    {
        instance = this;
        startPos = new Vector3(-30, 10, -15);  // Luc xuat hien dau tien
        minDistance = 0.1f;
        lineRenderer.positionCount = 0;
    }

    public void Init()
    {
        fliedDestination = false;
        isFlying = false;
        startFlying = false;
        transform.position = startPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (startFlying)
        {
            startTime = Time.time;
            speed = direction.magnitude;
            endPos = startPos + direction * speed;
            totalTime = speed/5;

            startFlying = false;
            isFlying = true;
            lineRenderer.positionCount = 1;
            lineRenderer.SetPosition(0, startPos);

        }
        if (isFlying && !fliedDestination)
        {
            BirdFlying();
        }

    }


    public void BirdFlying()
    {
        Vector3 currentPos = Vector3.Slerp(startPos, endPos, Mathf.Min((Time.time - startTime) / totalTime,1));
        if (Vector3.Distance(currentPos, endPos) < minDistance)
        {
            isFlying = false;
            fliedDestination = true;
            currentPos = endPos;
            
            GM.instance.NeedMouseVector = true; // Cho phep ve lai
            DrawMouse.instance.HavingMouseVector = false; // Reset mouse vector
      
        }
        transform.position = currentPos;
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, currentPos);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Bird collided with: " + other.name);

        if (other.CompareTag("Pig"))
        {
           
            isFlying = false;
            fliedDestination = true;

            GM.instance.Score += 1; // Tang diem khi va cham voi Pig
            GM.instance.NeedMouseVector = true; // Cho phep ve lai
            DrawMouse.instance.HavingMouseVector = false; // Reset mouse vector

            Debug.Log("Bird hit an obstacle! " + GM.instance.Score);
        }
    }


}
