using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControl : MonoBehaviour {

    public float timeIncrease;



    void Start()
    {
        timeIncrease = 1;

    }

    public void FastForward()

    {

        Time.timeScale += timeIncrease; 
        
    }

    public void Pause()
    {

        Time.timeScale = 0f;
    }

    public void PlayNormal()
    {

        Time.timeScale = 1f;
    }

    
}
