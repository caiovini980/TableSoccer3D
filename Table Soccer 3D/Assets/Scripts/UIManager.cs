using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text score1, score2;
    private int points1, points2;

    // Start is called before the first frame update
    void Start()
    {
        points1 = 0;
        points2 = 0;

        score1.text = "" + 0;
        score2.text = "" + 0;
    }

    public void UpdateScore1()
    {
        points1++;
        score1.text = "" + points1;
    }

    public void UpdateScore2()
    {
        points2 ++;
        score2.text = "" + points2;
    }
}
