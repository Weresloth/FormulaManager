using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class EndRace : MonoBehaviour {


	public int racePosition = 0;
	public int retirePosition = 19;
	public GameObject[] raceResults;
	public TMP_Text[] EndDriverNames;
	public TMP_Text[] EndTeamNames;
	public TMP_Text[] EndGaps;
	public TMP_Text[] EndPits;
	public TMP_Text[] EndPoints;

	private float finishTime;
	private float seconds;
	private float fraction;

	private string numberOfLaps;

	Dictionary<int, int> PointAllocation = new Dictionary<int, int>();

	private int checkeredFlag = 0;
	private int resulted = 0;

	public GameObject resultPanel;

	private int raceLeaderLap;

	public int leaderFinished;

	private int lapDownPosition = 0;

	private int gridSize;

	private int itWasALappedCar = 0;

	private int LapDownFix = 0;

	private int pseudoRetire = 0;

	private int EndOfRaceRetirement = 0;

    private float positionSorter;

	void Start() {


        positionSorter = 0;

		leaderFinished = 0;
		resultPanel.SetActive(false);
		PointAllocation.Add (1, 20);
		PointAllocation.Add (2, 14);
		PointAllocation.Add (3, 10);
		PointAllocation.Add (4, 8);
		PointAllocation.Add (5, 6);
		PointAllocation.Add (6, 4);
		PointAllocation.Add (7, 2);
		PointAllocation.Add (8, 1);
		PointAllocation.Add (9, 0);
		PointAllocation.Add (10, 0);
		PointAllocation.Add (11, 0);
		PointAllocation.Add (12, 0);
		PointAllocation.Add (13, 0);
		PointAllocation.Add (14, 0);
		PointAllocation.Add (15, 0);
		PointAllocation.Add (16, 0);
		PointAllocation.Add (17, 0);
		PointAllocation.Add (18, 0);
		PointAllocation.Add (19, 0);
		PointAllocation.Add (20, 0);
		PointAllocation.Add (21, 0);


		retirePosition = (GameObject.Find ("racetrack").GetComponent<StartTheRace> ().Cars.Length) - 1;
	
	}

	IEnumerator retireWait(GameObject retiredCar){

		yield return new WaitForSeconds (5);
		GameObject[] retireSpots;
		retireSpots = GameObject.FindGameObjectsWithTag ("RunOff");
		GameObject closest = null;
		float distance = Mathf.Infinity;

		foreach (GameObject go in retireSpots) {
		
			Vector2 diff = go.transform.position - retiredCar.transform.position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance) {

				closest = go;
				distance = curDistance;
			
			}
		}
		yield return closest;

		retiredCar.transform.position = closest.transform.position;

		/*float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject go in gos)
		{
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance)
			{
				closest = go;
				distance = curDistance;
			}
		}
		return closest;
		gameObject.transform.position = */

	}

	public void Update(){

		if (checkeredFlag == 1) {

			Debug.Log ("calculating TIME!(Killing CPU)");
			finishTime += Time.deltaTime;
					
		}

		if (resulted >= GameObject.Find ("racetrack").GetComponent<StartTheRace> ().Cars.Length)
			checkeredFlag = 0;

	}

	public void RaceFinish(GameObject finishingCar){
	    


		Debug.Log("We reached to the endrace method, yaay!");
        finishTime = finishingCar.GetComponent<Lapping>().personalTimerTotal;
        finishingCar.GetComponent<Lapping>().finalDistance = finishingCar.GetComponent<Lapping>().finalDistance + (500 - positionSorter);
        finishingCar.GetComponent<Lapping>().status = "finished";
        positionSorter = positionSorter + 20;
        checkeredFlag = 1;
        resulted += 1;
        leaderFinished = 1;

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

    }

	public void RetireFromRace(GameObject retiringCar){

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



		Debug.Log (retiringCar.GetComponent<Lapping> ().drivername + " has retired!");
        resulted += 1;
        StartCoroutine(retireWait(retiringCar));

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
        //retirePosition = retirePosition - 1;


		

	}


	public void ShowResults(){
	

		if (resultPanel.activeSelf == false) {

			resultPanel.SetActive (true);
	
		} else {
		
			resultPanel.SetActive (false);
		
		}


	}





}
