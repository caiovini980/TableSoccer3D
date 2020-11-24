using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTrigger : MonoBehaviour
{
    private string Goal2Tag = "Goal2";
    private string Goal1Tag = "Goal1";

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 7, 0);        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Goal2Tag))
        {
            //Add point to player 1 
            Debug.Log("Player 1 scored");
            transform.position = new Vector3(0, 7, 0);
            //return players for the original position
        }

        if (other.CompareTag(Goal1Tag))
        {
            //Add point to player 2
            Debug.Log("Player 2 scored");
            transform.position = new Vector3(0, 7, 0);
            //return players for the original position
        }
    }

}
