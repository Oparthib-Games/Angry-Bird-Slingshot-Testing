using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    public LineRenderer[] lineRenderers;
    public Transform[] stripPosition;
    public Transform center;
    public Transform idlePosition;

    bool isMouseDown;

    public Vector3 currPos;
    public float maxLength;

    public GameObject bird;
    Rigidbody2D birdRB;
    Collider2D birdCollider;
    public float birdPosOffset;
    public float force;

    void Start()
    {
        lineRenderers[0].positionCount = 2;
        lineRenderers[1].positionCount = 2;
        lineRenderers[0].SetPosition(0, stripPosition[0].position);
        lineRenderers[1].SetPosition(0, stripPosition[1].position);

        CreateBird();
    }

    void Update()
    {
        if (!isMouseDown || birdRB == null)
        {
            ResetStrips();
            return;
        }

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10;

        currPos = Camera.main.ScreenToWorldPoint(mousePos);
        currPos = center.position + Vector3.ClampMagnitude(currPos - center.position, maxLength);

        SetStrips(currPos);
    }

    void CreateBird()
    {
        if(bird != null)
        {
            birdRB = Instantiate(bird).GetComponent<Rigidbody2D>();
            birdRB.isKinematic = true;
            birdCollider = birdRB.GetComponent<Collider2D>();
            birdCollider.enabled = false;
        }
    }

    private void OnMouseDown()
    {
        isMouseDown = true;
    }
    private void OnMouseUp()
    {
        isMouseDown = false;
        Shoot();
    }

    void Shoot()
    {
        if(birdRB != null)
        {
            birdRB.isKinematic = false;
            birdCollider.enabled = true;

            Vector3 birdForce = (currPos - center.position) * force * -1;
            birdRB.velocity = birdForce;

            birdRB = null;
            Invoke("CreateBird", 2);
        }
    }

    void ResetStrips()
    {
        currPos = idlePosition.position;
        SetStrips(idlePosition.position);
    }
    void SetStrips(Vector3 pos)
    {
        lineRenderers[0].SetPosition(1, pos);
        lineRenderers[1].SetPosition(1, pos);

        if(birdRB != null)
        {
            Vector3 dir = pos - center.position;
            birdRB.transform.position = pos + dir.normalized * birdPosOffset;
        }
    }
}
