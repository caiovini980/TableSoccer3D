using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject mousePointA; //the green ball
    public GameObject mousePointB; //the orange ball
    public GameObject arrow;
    public GameObject circle;

    //Calculate distance
    private float currentDistance;
    private float safeSpace;
    private float shootPower;

    public float maxDistance = 3f;

    private Vector3 shootDirection;

    private void Awake()
    {
        mousePointA = GameObject.FindGameObjectWithTag("PointA");
        mousePointB = GameObject.FindGameObjectWithTag("PointB");
        arrow = GameObject.FindGameObjectWithTag("Arrow");
        circle = GameObject.FindGameObjectWithTag("Circle");
    }

    private void OnMouseDrag()
    {
        currentDistance = Vector3.Distance(mousePointA.transform.position, transform.position);

        if (currentDistance <= maxDistance)
        {
            safeSpace = currentDistance;
        }
        else
        {
            currentDistance = maxDistance;
        }

        doArrowAndCircleStuffy();

        //calculate power and direction
        shootPower = Mathf.Abs(safeSpace) * 13;

        Vector3 dimensionXY = mousePointA.transform.position - transform.position;
        float difference = dimensionXY.magnitude;
        mousePointB.transform.position = transform.position + ((dimensionXY / difference) * currentDistance * - 1);
        mousePointB.transform.position = new Vector3(mousePointB.transform.position.x, mousePointB.transform.position.y, -0.5f);
        shootDirection = Vector3.Normalize(mousePointA.transform.position - transform.position);
    }

    private void OnMouseUp() 
    {
        arrow.GetComponent<Renderer>().enabled = false;
        circle.GetComponent<Renderer>().enabled = false;

        Vector3 push = shootDirection * shootPower * -1;
        GetComponent<Rigidbody>().AddForce(push, ForceMode.Impulse);
    }

    private void doArrowAndCircleStuffy()
    {
        arrow.GetComponent<Renderer>().enabled = true;
        circle.GetComponent<Renderer>().enabled = true;

        //calc position
        if(currentDistance <= maxDistance)
        {
            arrow.transform.position = new Vector3((2 * transform.position.x) - mousePointA.transform.position.x, 
                (2 * transform.position.y) - mousePointA.transform.position.y, -1.5f);
        }
        else
        {
            Vector3 dimensionXY = mousePointA.transform.position - transform.position;
            float difference = dimensionXY.magnitude;
            arrow.transform.position = transform.position + ((dimensionXY / difference) * maxDistance * -1);
            arrow.transform.position = new Vector3(arrow.transform.position.x, arrow.transform.position.y, -1.5f);
        }

        circle.transform.position = transform.position + new Vector3(0, 0, 0.05f);
        Vector3 direction = mousePointA.transform.position - transform.position;
        float rotation;
        if(Vector3.Angle(direction, transform.forward) > 90)
        {
            rotation = Vector3.Angle(direction, transform.right);
        }
        else
        {
            rotation = Vector3.Angle(direction, transform.right) * -1;
        }

        arrow.transform.eulerAngles = new Vector3(0, 0, rotation);

        float scaleX = Mathf.Log(1 + safeSpace / 2, 2) * 2.2f;
        float scaleY = Mathf.Log(1 + safeSpace / 2, 2) * 2.2f;

        arrow.transform.localScale = new Vector3(1 + scaleX, 1 + scaleY, 0.001f);
        circle.transform.localScale = new Vector3(1 + scaleX, 1 + scaleY, 0.001f);
    }

}
