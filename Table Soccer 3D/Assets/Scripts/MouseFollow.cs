using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    private float offset = 0f;
    private Vector3 tempPosition;
    public Camera cam;
    public PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, offset);
    }

    // Update is called once per frame
    void Update()
    {
        tempPosition = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));


        //Move the object to the mouse position
        if(player.isShootable)
        {
            transform.position = new Vector3(tempPosition.x, tempPosition.y, offset);
        }
    }
}
