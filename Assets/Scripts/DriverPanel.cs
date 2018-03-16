using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DriverPanel : MonoBehaviour {


    public int driverNumber;
    public GameObject displayedCar;
    public TMP_Text driverName;
    public Slider tyreCondition;
    public Toggle pitcall;
    public Slider paceSlider;
    public Slider revSlider;
    public TMP_Text fuelAmount;
    private GameObject[] carsList;
    public Image driverPanel;
    public InputField refuelAmount;
    public Dropdown tyreChoice;
	
	void Start () {


        carsList = GameObject.Find("racetrack").GetComponent<StartTheRace>().Cars;


        for (int i = 0; i < carsList.Length; i++)
        {


            if (driverNumber == 1)
            {
                if (carsList[i].GetComponent<Lapping>().playerCar1 == true)
                {

                    displayedCar = carsList[i];
                    driverName.text = carsList[i].GetComponent<Lapping>().drivername;
                    fuelAmount.text = carsList[i].GetComponent<Lapping>().fuel.ToString();
                    tyreCondition.value = CalculateTyreHP();
                    

                    if (carsList[i].GetComponent<Lapping>().teamColor == Color.white)
                    {
                        driverPanel.color = carsList[i].GetComponent<Lapping>().teamColor2;

                    } else
                    {
                        driverPanel.color = carsList[i].GetComponent<Lapping>().teamColor;

                    }
                    


                }


            } else
            {
                if (carsList[i].GetComponent<Lapping>().playerCar2 == true)
                {

                    displayedCar = carsList[i];
                    driverName.text = carsList[i].GetComponent<Lapping>().drivername;
                    fuelAmount.text = carsList[i].GetComponent<Lapping>().fuel.ToString();
                    tyreCondition.value = CalculateTyreHP();

                    if (carsList[i].GetComponent<Lapping>().teamColor == Color.white)
                    {
                        driverPanel.color = carsList[i].GetComponent<Lapping>().teamColor2;

                    }
                    else
                    {
                        driverPanel.color = carsList[i].GetComponent<Lapping>().teamColor;

                    }

                }
            }
         }




    }
	
	

    public void ApplyTheStart()
    {

        carsList = GameObject.Find("racetrack").GetComponent<StartTheRace>().Cars;


        for (int i = 0; i < carsList.Length; i++)
        {


            if (driverNumber == 1)
            {
                if (carsList[i].GetComponent<Lapping>().playerCar1 == true)
                {

                    displayedCar = carsList[i];
                    driverName.text = carsList[i].GetComponent<Lapping>().drivername;
                    fuelAmount.text = carsList[i].GetComponent<Lapping>().fuel.ToString();
                    tyreCondition.value = CalculateTyreHP();

                    if (carsList[i].GetComponent<Lapping>().teamColor == Color.white)
                    {
                        driverPanel.color = carsList[i].GetComponent<Lapping>().teamColor2;

                    }
                    else
                    {
                        driverPanel.color = carsList[i].GetComponent<Lapping>().teamColor;

                    }



                }


            }
            else
            {
                if (carsList[i].GetComponent<Lapping>().playerCar2 == true)
                {

                    displayedCar = carsList[i];
                    driverName.text = carsList[i].GetComponent<Lapping>().drivername;
                    fuelAmount.text = carsList[i].GetComponent<Lapping>().fuel.ToString();
                    tyreCondition.value = CalculateTyreHP();

                    if (carsList[i].GetComponent<Lapping>().teamColor == Color.white)
                    {
                        driverPanel.color = carsList[i].GetComponent<Lapping>().teamColor2;

                    }
                    else
                    {
                        driverPanel.color = carsList[i].GetComponent<Lapping>().teamColor;

                    }

                }
            }
        }


    }



	void Update () {





        tyreCondition.value = CalculateTyreHP();

        displayedCar.GetComponent<Lapping>().paceNumber = (int)paceSlider.value;
        displayedCar.GetComponent<Lapping>().revNumber = (int)revSlider.value;
        fuelAmount.text = displayedCar.GetComponent<Lapping>().fuel.ToString();




    }


    float CalculateTyreHP()
    {

        return displayedCar.GetComponent<Lapping>().tyreHP / displayedCar.GetComponent<Lapping>().tyreHPMax;

    }


}
