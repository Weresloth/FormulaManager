using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeatherControl : MonoBehaviour {



    public float trackWetness;
    public string weatherCondition;
    public float tempature;
    public GameObject rainFilter;    
    public int[] forecastList;
    private int weatherChange;
    public int currentWeatherStage;

    [Header("Rain Effect Colors")]
    public Color dry;
    public Color lightRain;
    public Color heavyRain;

    [Header("UI Elements")]
    public TMP_Text tempatureText;
    public TMP_Text wetnessText;
    public TMP_Text forecast1;
    public TMP_Text forecast2;
    public TMP_Text forecast3;
    public TMP_Text forecast4;
    public GameObject weatherIcon;

    [Header("Weather Icons")]
    public Sprite sunnyIcon;
    public Sprite cloudyIcon;
    public Sprite lightrainIcon;
    public Sprite heavyrainIcon;


    void Start () {

        currentWeatherStage = 0; 
        forecastList = new int[50];
        forecastList[0] = (int)Random.Range(1, 5);

        switch (forecastList[0])
        {

            case 1:
                weatherCondition = "Sunny";
                tempature = Mathf.Floor(Random.Range(22, 34));
                trackWetness = Mathf.Floor(Random.Range(0, 5));
                weatherIcon.GetComponent<Image>().sprite = sunnyIcon;
                wetnessText.text = "%" + ((int)trackWetness).ToString();
                tempatureText.text = (int)tempature + " ºC";
                break;
            case 2:
                weatherCondition = "Cloudy";
                tempature = Mathf.Floor(Random.Range(12, 24));
                trackWetness = Mathf.Floor(Random.Range(0, 5));
                weatherIcon.GetComponent<Image>().sprite = cloudyIcon;
                wetnessText.text = "%" + ((int)trackWetness).ToString();
                tempatureText.text = (int)tempature + " ºC";
                break;
            case 3:
                weatherCondition = "Light Rain";
                tempature = Mathf.Floor(Random.Range(8, 18));
                trackWetness = Mathf.Floor(Random.Range(5, 40));
                weatherIcon.GetComponent<Image>().sprite = lightrainIcon;
                wetnessText.text = "%" + ((int)trackWetness).ToString();
                tempatureText.text = (int)tempature + " ºC";
                break;
            case 4:
                weatherCondition = "Heavy Rain";
                tempature = Mathf.Floor(Random.Range(6, 15));
                trackWetness = Mathf.Floor(Random.Range(10, 75));
                weatherIcon.GetComponent<Image>().sprite = heavyrainIcon;
                wetnessText.text = "%" + ((int)trackWetness).ToString();
                tempatureText.text = (int)tempature + " ºC";
                break;

        }


        for (int x=1; x < forecastList.Length; x++)
        {

            weatherChange = (int)Random.Range(-1, 2);
            forecastList[x] = forecastList[x - 1] + weatherChange;    
            if (forecastList[x] < 1) forecastList[x] = 1;
            if (forecastList[x] > 4) forecastList[x] = 4;  


        }

        forecastDisplayer();

    }
	
    
    public IEnumerator ChangeWetness()
    {
        
        while (GameObject.Find("racetrack").GetComponent<EndRace>().checkeredFlag == 0)
        {
            if (weatherCondition == "Sunny")
            {
                tempature = tempature + 0.2f;
                trackWetness = trackWetness - 0.6f;
                if (tempature > 36) tempature = 36;
                if (trackWetness < 0) trackWetness = 0;
            } else if (weatherCondition == "Cloudy")
            {
                trackWetness = trackWetness - 0.3f;
                if (trackWetness < 0) trackWetness = 0;
            }
            else if (weatherCondition == "Light Rain")
            {
                tempature = tempature - 0.1f;
                trackWetness = trackWetness + 0.7f;
                if (tempature < 5) tempature = 5;
                if (trackWetness > 100) trackWetness = 100;

            }
            else if (weatherCondition == "Heavy Rain")
            {
                tempature = tempature - 0.2f;
                trackWetness = trackWetness + 1.1f;
                if (tempature < 5) tempature = 5;
                if (trackWetness > 100) trackWetness = 100;

            }
            wetnessText.text = "%" + ((int)trackWetness).ToString();
            tempatureText.text = (int)tempature + " ºC";
            yield return new WaitForSeconds(2f);
        }


        

    }


    public IEnumerator ChangeWeather()
    {

        for (int x = 0; x < forecastList.Length && GameObject.Find("racetrack").GetComponent<EndRace>().checkeredFlag == 0; x++)
        {

            if (forecastList[x] == 1)
            {
                weatherCondition = "Sunny";                
                rainFilter.GetComponent<Image>().color = dry;
                weatherIcon.GetComponent<Image>().sprite = sunnyIcon;


            } else if (forecastList[x] == 2)
            {
                weatherCondition = "Cloudy";                
                rainFilter.GetComponent<Image>().color = dry;
                weatherIcon.GetComponent<Image>().sprite = cloudyIcon;

            }
            else if (forecastList[x] == 3)
            {
                weatherCondition = "Light Rain";                
                rainFilter.GetComponent<Image>().color = lightRain;
                weatherIcon.GetComponent<Image>().sprite = lightrainIcon;

            }
            else if (forecastList[x] == 4)
            {
                weatherCondition = "Heavy Rain";                
                rainFilter.GetComponent<Image>().color = heavyRain;
                weatherIcon.GetComponent<Image>().sprite = heavyrainIcon;
            }

            currentWeatherStage = x;
            forecastDisplayer();
            

            yield return new WaitForSeconds(Random.Range(20, 30));

        }

    }

          
        public void forecastDisplayer()
    {



        if ((currentWeatherStage+2) < forecastList.Length)
        {
            switch (forecastList[currentWeatherStage + 1])
            {
                case 1:
                    forecast1.text = "Sunny";
                    break;
                case 2:
                    forecast1.text = "Cloudy";
                    break;
                case 3:
                    forecast1.text = "Light Rain";
                    break;
                case 4:
                    forecast1.text = "Heavy Rain";
                    break;

            }


        } else
        {
            forecast1.text = "...";
        }
        
        if ((currentWeatherStage+3) < forecastList.Length)
        {
            switch (forecastList[currentWeatherStage + 2])
            {
                case 1:
                    forecast2.text = "Sunny";
                    break;
                case 2:
                    forecast2.text = "Cloudy";
                    break;
                case 3:
                    forecast2.text = "Light Rain";
                    break;
                case 4:
                    forecast2.text = "Heavy Rain";
                    break;

            }


        } else
        {
            forecast2.text = "...";

        }

        if ((currentWeatherStage+4) < forecastList.Length)
        {
            switch (forecastList[currentWeatherStage + 3])
            {
                case 1:
                    forecast3.text = "Sunny";
                    break;
                case 2:
                    forecast3.text = "Cloudy";
                    break;
                case 3:
                    forecast3.text = "Light Rain";
                    break;
                case 4:
                    forecast3.text = "Heavy Rain";
                    break;

            }


        } else
        {
            forecast3.text = "...";

        }
        
        if ((currentWeatherStage+5) < forecastList.Length)
        {

            switch (forecastList[currentWeatherStage + 4])
            {
                case 1:
                    forecast4.text = "Sunny";
                    break;
                case 2:
                    forecast4.text = "Cloudy";
                    break;
                case 3:
                    forecast4.text = "Light Rain";
                    break;
                case 4:
                    forecast4.text = "Heavy Rain";
                    break;

            }

        } else
        {
            forecast4.text = "...";

        }

        

    }
    
    
    
      	
	
}
