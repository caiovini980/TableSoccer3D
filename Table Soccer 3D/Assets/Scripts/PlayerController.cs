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
    private float shotPower;

    public float maxDistance = 2f;

    private Vector3 shootDirection;

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

        //calculate power and direction
        shotPower = Mathf.Abs(safeSpace) * 10;

        Vector3 dimensionXY = mousePointA.transform.position - transform.position;
        float difference = dimensionXY.magnitude;
        mousePointB.transform.position = transform.position + ((dimensionXY / difference) * currentDistance * - 1);
        mousePointB.transform.position = new Vector3(
            mousePointB.transform.position.x, 
            mousePointB.transform.position.y, 
            -0.8f
            );
    }

}
