using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TeamSelector : MonoBehaviour {




    public TMP_Dropdown teamSelection;

    public GameObject[] carsList;


	void Start () {		
	}
	

    public void ChangeTeam()
    {


        switch (teamSelection.value)
        {
            case 0:
                for (int i = 0; i < carsList.Length; i++)
                {
                    carsList[i].GetComponent<Lapping>().playerCar1 = false;
                    carsList[i].GetComponent<Lapping>().playerCar2 = false;

                }
                break;
            case 1:
                for (int i = 0; i < carsList.Length; i++)
                {

                    if (carsList[i].GetComponent<Lapping>().carNumber == 1)
                    {
                        carsList[i].GetComponent<Lapping>().playerCar1 = true;

                    }
                    if (carsList[i].GetComponent<Lapping>().carNumber == 2)
                    {
                        carsList[i].GetComponent<Lapping>().playerCar2 = true;

                    }
                }
                break;
            case 2:
                for (int i = 0; i < carsList.Length; i++)
                {

                    if (carsList[i].GetComponent<Lapping>().carNumber == 3)
                    {
                        carsList[i].GetComponent<Lapping>().playerCar1 = true;

                    }
                    if (carsList[i].GetComponent<Lapping>().carNumber == 4)
                    {
                        carsList[i].GetComponent<Lapping>().playerCar2 = true;

                    }
                }
                break;
            case 3:
                for (int i = 0; i < carsList.Length; i++)
                {

                    if (carsList[i].GetComponent<Lapping>().carNumber == 5)
                    {
                        carsList[i].GetComponent<Lapping>().playerCar1 = true;

                    }
                    if (carsList[i].GetComponent<Lapping>().carNumber == 6)
                    {
                        carsList[i].GetComponent<Lapping>().playerCar2 = true;

                    }
                }
                break;
            case 4:
                for (int i = 0; i < carsList.Length; i++)
                {

                    if (carsList[i].GetComponent<Lapping>().carNumber == 7)
                    {
                        carsList[i].GetComponent<Lapping>().playerCar1 = true;

                    }
                    if (carsList[i].GetComponent<Lapping>().carNumber == 8)
                    {
                        carsList[i].GetComponent<Lapping>().playerCar2 = true;

                    }
                }
                break;
            case 5:
                for (int i = 0; i < carsList.Length; i++)
                {

                    if (carsList[i].GetComponent<Lapping>().carNumber == 9)
                    {
                        carsList[i].GetComponent<Lapping>().playerCar1 = true;

                    }
                    if (carsList[i].GetComponent<Lapping>().carNumber == 10)
                    {
                        carsList[i].GetComponent<Lapping>().playerCar2 = true;

                    }
                }
                break;
            case 6:
                for (int i = 0; i < carsList.Length; i++)
                {

                    if (carsList[i].GetComponent<Lapping>().carNumber == 11)
                    {
                        carsList[i].GetComponent<Lapping>().playerCar1 = true;

                    }
                    if (carsList[i].GetComponent<Lapping>().carNumber == 12)
                    {
                        carsList[i].GetComponent<Lapping>().playerCar2 = true;

                    }
                }
                break;
            case 7:
                for (int i = 0; i < carsList.Length; i++)
                {

                    if (carsList[i].GetComponent<Lapping>().carNumber == 13)
                    {
                        carsList[i].GetComponent<Lapping>().playerCar1 = true;

                    }
                    if (carsList[i].GetComponent<Lapping>().carNumber == 14)
                    {
                        carsList[i].GetComponent<Lapping>().playerCar2 = true;

                    }
                }
                break;
            case 8:
                for (int i = 0; i < carsList.Length; i++)
                {

                    if (carsList[i].GetComponent<Lapping>().carNumber == 15)
                    {
                        carsList[i].GetComponent<Lapping>().playerCar1 = true;

                    }
                    if (carsList[i].GetComponent<Lapping>().carNumber == 16)
                    {
                        carsList[i].GetComponent<Lapping>().playerCar2 = true;

                    }
                }
                break;
            case 9:
                for (int i = 0; i < carsList.Length; i++)
                {

                    if (carsList[i].GetComponent<Lapping>().carNumber == 17)
                    {
                        carsList[i].GetComponent<Lapping>().playerCar1 = true;

                    }
                    if (carsList[i].GetComponent<Lapping>().carNumber == 18)
                    {
                        carsList[i].GetComponent<Lapping>().playerCar2 = true;

                    }
                }
                break;
            case 10:
                for (int i = 0; i < carsList.Length; i++)
                {

                    if (carsList[i].GetComponent<Lapping>().carNumber == 19)
                    {
                        carsList[i].GetComponent<Lapping>().playerCar1 = true;

                    }
                    if (carsList[i].GetComponent<Lapping>().carNumber == 20)
                    {
                        carsList[i].GetComponent<Lapping>().playerCar2 = true;

                    }
                }
                break;
            case 11:
                for (int i = 0; i < carsList.Length; i++)
                {

                    if (carsList[i].GetComponent<Lapping>().carNumber == 21)
                    {
                        carsList[i].GetComponent<Lapping>().playerCar1 = true;

                    }
                    if (carsList[i].GetComponent<Lapping>().carNumber == 22)
                    {
                        carsList[i].GetComponent<Lapping>().playerCar2 = true;

                    }
                }
                break;
            case 12:
                for (int i = 0; i < carsList.Length; i++)
                {

                    if (carsList[i].GetComponent<Lapping>().carNumber == 23)
                    {
                        carsList[i].GetComponent<Lapping>().playerCar1 = true;

                    }
                    if (carsList[i].GetComponent<Lapping>().carNumber == 24)
                    {
                        carsList[i].GetComponent<Lapping>().playerCar2 = true;

                    }
                }
                break;


        }       


    }


	
}
