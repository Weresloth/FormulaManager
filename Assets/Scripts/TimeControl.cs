using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeControl : MonoBehaviour {

    public float timeIncrease;
    public TMP_Text speedCounter;


    void Start()
    {
        Time.timeScale = 1f;
        timeIncrease = 1;
        speedCounter.text = "1x";
    }

    public void FastForward()

    {

        Time.timeScale += timeIncrease;
        speedCounter.text = Time.timeScale.ToString()+"x";

    }

    public void Pause()
    {

        Time.timeScale = 0f;
        speedCounter.text = "Paused";
    }

    public void PlayNormal()
    {

        Time.timeScale = 1f;
        speedCounter.text = "1x";
    }

    
}
