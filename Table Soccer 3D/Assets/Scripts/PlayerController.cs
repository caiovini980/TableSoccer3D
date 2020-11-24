using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviourPunCallbacks
{
    private GameObject mousePointA; //the green ball
    private GameObject mousePointB; //the orange ball
    private GameObject arrow;
    private GameObject circle;
    public float maxDistance = 3f;
    public bool isShootable = true;
    [Tooltip("Maximum value of X before player can shoot again"), Range(0, 1)]
    public float maxX;
    [Tooltip("Maximum value of Y before player can shoot again"), Range(0, 1)]
    public float maxY;
    [Tooltip("Minimum value of X before player can shoot again"), Range(-1, 0)]
    public float minX;
    [Tooltip("Minimum value of Y before player can shoot again"), Range(-1, 0)]
    public float minY;
    

    //Calculate distance
    private float currentDistance;
    private float safeSpace;
    private float shootPower;
    private int acceleration = 2;
    private Vector3 shootDirection;
    private Renderer arrowRenderer, circleRenderer;
    private Rigidbody rb;
    private Player _photonPlayer;
    private int _playerId;

    private void Awake()
    {
        mousePointA = GameObject.FindGameObjectWithTag("PointA");
        mousePointB = GameObject.FindGameObjectWithTag("PointB");
        arrow = GameObject.FindGameObjectWithTag("Arrow");
        circle = GameObject.FindGameObjectWithTag("Circle");

        rb = GetComponent<Rigidbody>();
        arrowRenderer = arrow.GetComponent<Renderer>();
        circleRenderer = circle.GetComponent<Renderer>();
    }

    [PunRPC]
    public void Initialize(Player player)
    {
        _photonPlayer = player;
        _playerId = player.ActorNumber;
        GameSetupController.Instance.Players.Add(this);

        if (!photonView.IsMine)
        {
            rb.isKinematic = true;
        }
    }


    private void LateUpdate()
    {
        if (rb.velocity.x > maxX || rb.velocity.x < minX &&
            rb.velocity.y > maxY || rb.velocity.y < minY)
        {
            isShootable = false;
        }
        else
        {
            Vector3 velocity = rb.velocity;
            velocity = new Vector3(0f, 0f, 0f);
            rb.velocity = velocity;

            isShootable = true;
        }
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
            safeSpace = maxDistance;
        }

        ArrowAndCircle();

        //calculate power and direction
        shootPower = Mathf.Abs(safeSpace) * acceleration;

        Vector3 dimensionXY = mousePointA.transform.position - transform.position;
        float difference = dimensionXY.magnitude;

        mousePointB.transform.position = transform.position + ((dimensionXY / difference) * currentDistance * - 1);

        shootDirection = Vector3.Normalize(mousePointA.transform.position - transform.position);
    }

    //Apply the force
    private void OnMouseUp() 
    {
        arrowRenderer.enabled = false;
        circleRenderer.enabled = false;
        isShootable = false;

        Vector3 push = shootDirection * shootPower * -1;
        rb.AddForce(push, ForceMode.Impulse);
    }

    private void ArrowAndCircle()
    {
        arrowRenderer.enabled = true;
        circleRenderer.enabled = true;

        //calculate position of the arrow and the circle
        if (currentDistance <= maxDistance)
        {
            //spawn arrow to show direction of the shot
            arrow.transform.position = new Vector3((2 * transform.position.x) - mousePointA.transform.position.x,
            (2 * transform.position.y) - mousePointA.transform.position.y, 0);
        }
        else
        {
            Vector3 dimensionXY = mousePointA.transform.position - transform.position;
            float difference = dimensionXY.magnitude;

            arrow.transform.position = transform.position + ((dimensionXY / difference) * maxDistance * -1);
        }

        //circle position
        circle.transform.position = transform.position + new Vector3(0, 0, 0.05f);

        //direction = mouse position - object position
        Vector3 direction = mousePointA.transform.position - transform.position;
        float rotation;

        #region Rotation of the arrow
        if (Vector3.Angle(direction, transform.forward) > 90)
        {
            rotation = Vector3.Angle(direction, transform.right);
        }
        else
        {
            rotation = Vector3.Angle(direction, transform.right) * -1;
        }
        arrow.transform.eulerAngles = new Vector3(0, 0, rotation);
        #endregion

        float scaleX = Mathf.Log(1 + safeSpace / 2, 2) * 2.2f;
        float scaleY = Mathf.Log(1 + safeSpace / 2, 2) * 2.2f;

        arrow.transform.localScale = new Vector3(scaleX, scaleY, 0.001f);
        circle.transform.localScale = new Vector3(1 + scaleX, 1 + scaleY, 0.001f);
    }


}
