using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class DrawMouse : MonoBehaviour
{
    public static DrawMouse instance;

    private LineRenderer lineRenderer;
    private Vector3 mousePosStart, mousePosEnd, tempPos;
    

    private const float YObject = 10f;
    private const float YCamera = 50f;
    private const float ZConvert = YCamera - YObject; // Convert Y position from Bird to Main Camera
    private const float MaxVectorLength = 10f;
    private const float minDistance = 0.1f;

    private Vector3 mouseVector;
    public Vector3 MouseVector
    {
        get { return mouseVector; }
        set { mouseVector = value; }
    }


    void Awake()
    {
        instance = this;
    }

    public void Init()
    {
        lineRenderer = GetComponent<LineRenderer>();
        //mouseVector = Vector3.zero;
    }


    void Update()
    {
        if (Bird.instance.Flying)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            mousePosStart = Input.mousePosition;
            mousePosStart.z = ZConvert;
            mousePosStart = Camera.main.ScreenToWorldPoint(mousePosStart);
      
            Bird.instance.Init(); // Reset Bird position
            UIManager.instance.UpdateStatus("Pull the bow");
            lineRenderer.positionCount = 1; // Reset line renderer
            lineRenderer.SetPosition(0, mousePosStart);
        }

        if (Input.GetMouseButton(0))
        {
            mousePosEnd = Input.mousePosition;
            mousePosEnd.z = ZConvert;
            mousePosEnd = Camera.main.ScreenToWorldPoint(mousePosEnd);
            tempPos = mousePosEnd - mousePosStart;
            if (tempPos.sqrMagnitude > minDistance)
            {
                lineRenderer.positionCount = 2;
                lineRenderer.SetPosition(1, mousePosEnd);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            mousePosEnd = Input.mousePosition;
            mousePosEnd.z = ZConvert;
            mousePosEnd = Camera.main.ScreenToWorldPoint(mousePosEnd);
            tempPos = mousePosEnd - mousePosStart;
           
            mouseVector = mousePosEnd - mousePosStart;
            if (mouseVector.magnitude > MaxVectorLength)
            {
                mouseVector = mouseVector.normalized * MaxVectorLength;
            }
            lineRenderer.positionCount = 0; // Clear line renderer
            Bird.instance.StartFly();
        }

    }





}
