using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    private float offset = 0f;
    private Vector3 tempPosition;
    public Camera cam;
    public PlayerController playerController;

    // Start is called before the first frame update
    private void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, offset);
    }

    // Update is called once per frame
    private void Update()
    {
        //get current mouse position
        tempPosition = cam.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)
            );
        Debug.Log(tempPosition);

        if (playerController.isShootable)
        {
            //move the object to the mouse position
            transform.position = new Vector3(tempPosition.x, tempPosition.y, offset);
        }
    }
}
