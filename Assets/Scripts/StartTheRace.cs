using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class StartTheRace : MonoBehaviour {


	public GameObject[] Cars; 
	public GameObject StartPosition;
	public GameObject[] Position;
	public Text[] DriverNames;
	public Text[] Status;
	public Text[] Laps;
	private Text DriverName;
	public GameObject[] PitBoxes;
	private int raceFinished = 0;


    private float timeMinutesSelf;
    private float timeSecondsSelf;
    private float timeFractionsSelf;
    private float timeRawSelf;

    private float timeRaw;
    private float timeGap;

    private float timeGapMinutes;
    private float timeGapSeconds;
    private float timeGapFractions;
    


	public void RaceStart(){			 

		Debug.Log ("Start Button is pushed!");
		StartCoroutine (StartWait());

	}

	
	IEnumerator StartWait(){
	
		for (int i = 0; i < Cars.Length; i++) {

			Cars[i] = Instantiate (Cars[i], Position[0].transform.position, Quaternion.identity);
			yield return new WaitForSeconds(0.3f);


		}
	
		StartCoroutine (RaceStandings ());
        

    }

	IEnumerator RaceStandings(){
	
		while (raceFinished == 0) {
			RaceOrder ();
			yield return new WaitForSeconds (1f);
		}
			
			}
	

	public void QualifyingOrder(){	
	
		Cars = Cars.OrderBy(x=>x.GetComponent<Lapping>().qualifyingNumber * Random.Range(0.5f, 2f)).ToArray();


		for (int i = 0; i < Cars.Length; i++) {
		
			//DriverName.text = (Cars [i].GetComponent<Lapping> ().drivername).ToString();
			DriverNames[i].text = (Cars [i].GetComponent<Lapping>().drivername).ToString();
		
		}



		}


	public void RaceOrder(){
	
		Debug.Log ("RaceOrder is called!");
		if (raceFinished == 0) {
		
			Cars = Cars.OrderByDescending (x => x.GetComponent<Lapping>().distanceTraveled).ToArray ();


			for (int i = 0; i < Cars.Length; i++) {

				//Debug.Log ("Inside the for loop now...");
				DriverNames[i].text = (Cars [i].GetComponent<Lapping> ().drivername).ToString();

                if (i != 0)
                {
                    Status[i].text = (Cars[0].GetComponent<Lapping>().distanceTraveled - Cars[i].GetComponent<Lapping>().distanceTraveled).ToString();


                } else if (i == 0){

                    Status[i].text = "leader";
                }

                             
                
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
                
                Laps[i].text =  (Cars [i].GetComponent<Lapping> ().lap).ToString();
		
			}
		

			
		}

	}


}











	