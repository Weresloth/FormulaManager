using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectKiller : MonoBehaviour {
	
	void Start () {

        StartCoroutine(killObject());

        }

    IEnumerator killObject()
    {

        if (gameObject.tag == "Smoke")
        {
            yield return new WaitForSeconds(5f);
            DestroyObject(gameObject);

        } else
        {
            yield return new WaitForSeconds(2f);
            DestroyObject(gameObject);

        }
        

    }

    /*gridSize = GameObject.Find ("racetrack").GetComponent<StartTheRace> ().Cars.Length;


		for (int i = 0; i < gridSize; i++) {

			if (finishingCar.GetComponent<Lapping> ().lap < GameObject.Find ("racetrack").GetComponent<StartTheRace> ().Cars [i].GetComponent<Lapping> ().lap && GameObject.Find ("racetrack").GetComponent<StartTheRace> ().Cars [i].GetComponent<Lapping> ().raceFinished == 0) {

				lapDownPosition = lapDownPosition + 1;
				//LapDownFix = LapDownFix + 1;
				itWasALappedCar = itWasALappedCar + 1;
				if (itWasALappedCar > 1) {
					itWasALappedCar = 1;
				}
			} 
		}

		if (itWasALappedCar == 0) {
			LapDownFix = 0;
			}

		lapDownPosition = LapDownFix + lapDownPosition;

		while (EndDriverNames [racePosition + lapDownPosition].text != "") {
			racePosition = racePosition + 1;
		
		}
        */
    /*
raceResults[racePosition+lapDownPosition] = finishingCar;
EndDriverNames [racePosition+lapDownPosition].text = finishingCar.GetComponent<Lapping> ().drivername;
EndTeamNames [racePosition+lapDownPosition].text = finishingCar.GetComponent<Lapping> ().teamname;
EndPits [racePosition+lapDownPosition].text = (finishingCar.GetComponent<Lapping> ().pitCounter).ToString();
EndPoints [racePosition+lapDownPosition].text = (PointAllocation [racePosition+lapDownPosition+1]).ToString ();
*/
    /*
    if (racePosition == 0) {

        numberOfLaps = (finishingCar.GetComponent<Lapping> ().raceDistance).ToString();
        EndGaps [racePosition].text = numberOfLaps + " laps";
        checkeredFlag = 1;
        resulted += 1;
        raceLeaderLap = finishingCar.GetComponent<Lapping> ().raceDistance;
        leaderFinished = 1;

    } else {

        seconds = finishTime % 60;
        fraction = (finishTime * 100) % 100;

        if (finishingCar.GetComponent<Lapping> ().lap == raceLeaderLap) {
            EndGaps [racePosition+lapDownPosition].text = "+" + (Mathf.Round (seconds)).ToString () + "." + (Mathf.Round (fraction)).ToString () + "s";
        } else {
            EndGaps [racePosition+lapDownPosition].text = "+" + (finishingCar.GetComponent<Lapping> ().raceDistance - finishingCar.GetComponent<Lapping> ().lap).ToString() + " lap";			
        }


        resulted += 1;


    }

    if (itWasALappedCar == 0) {
        racePosition = racePosition + 1;
        LapDownFix = 0;
    } else if (itWasALappedCar == 1){
        LapDownFix = LapDownFix + 1;

    }

    itWasALappedCar = 0;
    lapDownPosition = 0;
    */
    /*if (i != 0)
                {
                    timeRaw = Cars[0].GetComponent<Lapping>().personalTimer;
                    timeRawSelf = Cars[i].GetComponent<Lapping>().personalTimer;
                    timeGap = timeRaw - timeRawSelf;

                    if (timeGap < 60)
                    {
                        timeGapSeconds = Mathf.Floor(timeGap);
                        timeGapFractions = Mathf.Floor((timeGap * 1000) % 1000);
                        Status[i].text = string.Format("+{0:00}.{1:000}", timeGapSeconds, timeGapFractions);
                    }
                    else
                    {
                        timeGap = timeGap - 60;
                        timeGapSeconds = Mathf.Floor(timeGap);
                        timeGapFractions = Mathf.Floor((timeGap * 1000) % 1000);
                        Status[i].text = "+1:"+timeGapSeconds.ToString() + "." + timeGapFractions.ToString();
                    }


                } else if (i == 0)
                {

                    timeRawSelf = Cars[i].GetComponent<Lapping>().personalTimer;
                    if (timeRawSelf < 60)
                    {
                        timeSecondsSelf = Mathf.Floor(timeRawSelf);
                        timeFractionsSelf = Mathf.Floor((timeRawSelf * 1000) % 1000);
                        Status[i].text = timeSecondsSelf.ToString() + "." + timeFractionsSelf.ToString();
                    }
                    else
                    {
                        timeRawSelf = timeRawSelf - 60;
                        timeSecondsSelf = Mathf.Floor(timeRawSelf);
                        timeFractionsSelf = Mathf.Floor((timeRawSelf * 1000) % 1000);
                        Status[i].text = "1:" + timeSecondsSelf.ToString() + "." + timeFractionsSelf.ToString();
                    }


                }*/
    //timeGap = Cars[0].GetComponent<Lapping>().distanceGap - Cars[i].GetComponent<Lapping>().distanceGap;
    //timeGapSeconds = Mathf.Floor(timeGap);
    //timeGapFractions = Mathf.Floor((timeGap * 1000f) % 1000f);                    

    //Gaps[i].text = (Cars[0].GetComponent<Lapping>().distanceTraveled - Cars[i].GetComponent<Lapping>().distanceTraveled).ToString();
    /*gridSize = GameObject.Find ("racetrack").GetComponent<StartTheRace> ().Cars.Length;


		pseudoRetire = retirePosition;

		for (int i = 0; i < gridSize; i++) {

			if (retiringCar.GetComponent<Lapping> ().lap > GameObject.Find ("racetrack").GetComponent<StartTheRace> ().Cars [i].GetComponent<Lapping> ().lap && checkeredFlag == 1) {


				retirePosition = retirePosition - 1;
				EndOfRaceRetirement = 1;
				if (EndOfRaceRetirement > 1) {
					EndOfRaceRetirement = 1;
				}
				}
			} 

		while (EndDriverNames [retirePosition].text != "") {
			retirePosition = retirePosition - 1;
		}*/
    /*raceResults [retirePosition] = retiringCar;
		EndDriverNames [retirePosition].text = retiringCar.GetComponent<Lapping> ().drivername;
		EndTeamNames [retirePosition].text = retiringCar.GetComponent<Lapping> ().teamname;
		EndGaps [retirePosition].text = retiringCar.GetComponent<Lapping>().status +" lap "+((retiringCar.GetComponent<Lapping>().lap)-1).ToString(); 
		EndPits [retirePosition].text = (retiringCar.GetComponent<Lapping> ().pitCounter).ToString();
		EndPoints [retirePosition].text = (PointAllocation [retirePosition+1]).ToString ();*/
    /*if (EndOfRaceRetirement != 0) {
        retirePosition = pseudoRetire;
        pseudoRetire = 0;
        EndOfRaceRetirement = 0;
    } else if (EndOfRaceRetirement == 0) {
        retirePosition = retirePosition + 1;		
    }*/
    /*void onCollisionExit2D (Collision2D Collision) {
		
		Debug.Log (drivername + " No more collision");
		carCollider = car.GetComponent<CircleCollider2D> ();
		carCollider.enabled = true;

	}*/
    /*if (x >= (points.Length)) {
		

			if (lap == raceDistance || GameObject.Find("racetrack").GetComponent<EndRace>().leaderFinished == 1) {
			
				Debug.Log (drivername + " finished the race!");
				raceFinished = 1;
				distanceTraveled += 5000f;
				x = 0;
				gameObject.transform.position = pitBox [pitBoxNumber].transform.position;
				isPitting = 1;
				GameObject.Find ("racetrack").GetComponent<EndRace> ().RaceFinish (gameObject);

			
			}




			if (fuel < 15 && raceFinished == 0) {
				Debug.Log (drivername + " is entering to pits");
				isPitting = 1;
				wearChecker = 1;
				carspeed = 0;
				gameObject.transform.position = pitBox [pitBoxNumber].transform.position;
				//carImage = car.GetComponent<SpriteRenderer>();
				//carImage.enabled = false;
				StartCoroutine (PitStop ());

				x = 0;
				LapTimes.Add (lap, personalTimer);
				personalTimer = 0;
				lap = lap + 1;
				distanceTraveled += 5000f;
				driverForm = Random.Range (0.1f, 0.6f);
				CalculateSpeed ();

			
			} else if (raceFinished == 0) {
				x = 0;
				LapTimes.Add (lap, personalTimer);
				personalTimer = 0;
				lap = lap + 1;
				distanceTraveled += 5000f;
				driverForm = Random.Range (0.1f, 0.6f);
				CalculateSpeed ();
			}
		}*/
    /*public void ShowResults(){
	

		if (resultPanel.activeSelf == false) {

			resultPanel.SetActive (true);
	
		} else {
		
			resultPanel.SetActive (false);
		
		}


	}*/

}




