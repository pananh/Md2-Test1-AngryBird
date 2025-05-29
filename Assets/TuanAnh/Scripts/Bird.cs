using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public static Bird instance;
    [SerializeField] LineRenderer lineRenderer;

    private Vector3 direction;
   
    private float speed;
   
    private bool  isFlying;
    public bool Flying
    {
        get { return isFlying; }
        set { isFlying = value; }
    }

    private Vector3 startPos, endPos;
    private float startTime, totalTime;
    private float minDistance;

    void Awake()
    {
        instance = this;
        startPos = new Vector3(-30, 10, -15);  // Luc xuat hien dau tien
        minDistance = 0.1f;
    }
    public void Init()
    {
        isFlying = false;
        lineRenderer.positionCount = 0;
        transform.position = startPos;
    }

    public void StartFly()
    {
        isFlying = true;
        direction = DrawMouse.instance.MouseVector;

        Debug.Log("Direction: " + direction);

        startTime = Time.time;
        speed = direction.magnitude;
        endPos = startPos + direction * speed;
        totalTime = speed / 5;
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, startPos);

    }

    void Update()
    {

        if (isFlying)
        {
            BirdFlying();
        }

    }


    public void BirdFlying()
    {
        Vector3 currentPos = Vector3.Slerp(startPos, endPos, Mathf.Min((Time.time - startTime) / totalTime, 1));
        if (Vector3.Distance(currentPos, endPos) < minDistance)
        {
            isFlying = false;
            currentPos = endPos;
        }

        transform.position = currentPos;

        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, currentPos);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pig"))
        {
            isFlying = false;
            UIManager.instance.UpdateStatus("Yeah hit PIG");
            GM.instance.AddScore(1); // Tang diem khi va cham voi Pig
            Destroy(other.gameObject);
        }

        else if (other.CompareTag("Wall"))
        {
            isFlying = false;
            UIManager.instance.UpdateStatus("Hit wall");
        }
        else if (other.CompareTag("Ground"))
        {
            isFlying = false;
            UIManager.instance.UpdateStatus("Hit ground");
        }

    }


}
