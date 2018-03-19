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
    public TMP_Text status;
    public TMP_Text tyreCompound;
    public static int playerCount;
    	
	void Start () {


        carsList = GameObject.Find("racetrack").GetComponent<StartTheRace>().Cars;


        for (int i = 0; i < carsList.Length; i++)
        {


            if (driverNumber == 1)
            {
                if (carsList[i].GetComponent<Lapping>().playerCar1 == true)
                {
                    playerCount = playerCount + 1;
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

                    playerCount = playerCount + 1;
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

            /*if (carsList[i].GetComponent<Lapping>().playerCar1 == false && carsList[i].GetComponent<Lapping>().playerCar2 == false)
            {

                displayedCar = carsList[0];        


            }   */        
            
         }



        if (playerCount == 0)
        {

            gameObject.SetActive(false);
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

            if (carsList[i].GetComponent<Lapping>().playerCar2 == true)
                {


                Debug.Log("Car2 start applied");
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



	void Update () {

        tyreCondition.value = CalculateTyreHP();
        tyreCompound.text = displayedCar.GetComponent<Lapping>().tyreCompound;
        status.text = displayedCar.GetComponent<Lapping>().status;
        displayedCar.GetComponent<Lapping>().paceNumber = (int)paceSlider.value;
        displayedCar.GetComponent<Lapping>().revNumber = (int)revSlider.value;


        fuelAmount.text = string.Format("{0:00}" ,displayedCar.GetComponent<Lapping>().fuel);

        if (pitcall.isOn == true)
        {
            displayedCar.GetComponent<Lapping>().pitCalled = true;

        } else if (pitcall.isOn == false)
        {
            displayedCar.GetComponent<Lapping>().pitCalled = false;

        }


    }


    float CalculateTyreHP()
    {

        return displayedCar.GetComponent<Lapping>().tyreHP / displayedCar.GetComponent<Lapping>().tyreHPMax;

    }

    public void CheckFuelInput()
    {

        if (refuelAmount.text == "" || refuelAmount.text == "-" || refuelAmount.text == "0")
        {

            Debug.Log("Here it checks the text...");
            refuelAmount.text = "5";
            
        }



    }
    

}

