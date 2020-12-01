using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTrigger : MonoBehaviour
{
    private string Goal2Tag = "Goal2";
    private string Goal1Tag = "Goal1";

    public UIManager _uiManager;

    // Start is called before the first frame update
    void Start()
    {
        float randomY = Random.Range(-7f, 7f);
        transform.position = new Vector3(0, randomY, 0);        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Goal2Tag))
        {
            //Add point to player 1 
            _uiManager.UpdateScore1();
            Debug.Log("Player 1 scored");
            float randomY = Random.Range(-7f, 7f);
            transform.position = new Vector3(0, randomY, 0);
            //return players for the original position
        }

        if (other.CompareTag(Goal1Tag))
        {
            //Add point to player 2
            _uiManager.UpdateScore2();
            Debug.Log("Player 2 scored");
            float randomY = Random.Range(-7f, 7f);
            transform.position = new Vector3(0, randomY, 0);
            //return players for the original position
        }
    }
}
