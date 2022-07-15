using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoring : MonoBehaviour
{

    static int playerScore = 0; 

    public GUISkin mySkin;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void addPoints(int pointValue)
    {
        playerScore += pointValue;
    }

    public static int returnPoints()
    {
        return playerScore;
    }
    
}
