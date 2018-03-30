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

	public int checkeredFlag = 0;

	private int resulted = 0;

	public GameObject resultPanel;

	private int raceLeaderLap;

	public int leaderFinished;	

	private int gridSize;	

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

		
	}

	public void Update(){

		if (resulted >= GameObject.Find ("racetrack").GetComponent<StartTheRace>().Cars.Length)
        {
            checkeredFlag = 1;
            StopCoroutine(GameObject.Find("WeatherControl").GetComponent<WeatherControl>().ChangeWetness());
            StopCoroutine(GameObject.Find("WeatherControl").GetComponent<WeatherControl>().ChangeWeather());

        }
			

    }

	public void RaceFinish(GameObject finishingCar){
	    


		Debug.Log("We reached to the endrace method, yaay!");
        finishingCar.GetComponent<Lapping>().finalDistance = finishingCar.GetComponent<Lapping>().finalDistance + (500 - positionSorter);
        finishingCar.GetComponent<Lapping>().status = "finished";
        positionSorter = positionSorter + 20;
        resulted += 1;
        leaderFinished = 1;

        

    }

	public void RetireFromRace(GameObject retiringCar){

		Debug.Log (retiringCar.GetComponent<Lapping> ().drivername + " has retired!");
        resulted += 1;
        StartCoroutine(retireWait(retiringCar));
      	
        }
        

}
