using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class DrawMouse : MonoBehaviour
{
    public static DrawMouse instance;

    private LineRenderer lineRenderer;
    private Vector3 mousePosStart, mousePosEnd, tempPos;
    private bool isDrawing;
    private float minDistance;

    private const float YObject = 10f;
    private const float YCamera = 50f;
    private const float ZConvert = YCamera - YObject; // Convert Y position from Bird to Main Camera
    private const float MaxVectorLength = 8.5f;

    private Vector3 mouseVector;
    public Vector3 MouseVector
    {
        get { return mouseVector; }
        set { mouseVector = value; }
    }
    private bool havingMouseVector;
    public bool HavingMouseVector
    {
        get { return havingMouseVector; }
        set { havingMouseVector = value; }
    }


    void Awake()
    {
        instance = this;
    }

    public void Init()
    {
        minDistance = 0.1f; 
        lineRenderer = GetComponent<LineRenderer>();
        isDrawing = false;
        havingMouseVector = false;
        mouseVector = Vector3.zero;
    }

    void Update()
    {
        if (GM.instance.NeedMouseVector && !isDrawing && !havingMouseVector && (Input.GetMouseButtonDown(0)))
        {
            mousePosStart = Input.mousePosition;
            mousePosStart.z = ZConvert;
            mousePosStart = Camera.main.ScreenToWorldPoint(mousePosStart);
            lineRenderer.positionCount = 1;
            lineRenderer.SetPosition(0, mousePosStart);
            isDrawing = true;

            Bird.instance.Init(); // Reset Bird position

        }
        if (GM.instance.NeedMouseVector && isDrawing && !havingMouseVector && (Input.GetMouseButton(0)))
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

        if (GM.instance.NeedMouseVector && isDrawing && !havingMouseVector && (Input.GetMouseButtonUp(0)))
        {
            mousePosEnd = Input.mousePosition;
            mousePosEnd.z = ZConvert;
            mousePosEnd = Camera.main.ScreenToWorldPoint(mousePosEnd);
            tempPos = mousePosEnd - mousePosStart;
            if (tempPos.sqrMagnitude > minDistance)
            {
                lineRenderer.SetPosition(1, mousePosEnd);
            }
            mouseVector = mousePosEnd - mousePosStart;
            if (mouseVector.magnitude > MaxVectorLength)
            {
                mouseVector = mouseVector.normalized * MaxVectorLength;
            }

            isDrawing = false;
            
            havingMouseVector = true;
        }

    }





}
