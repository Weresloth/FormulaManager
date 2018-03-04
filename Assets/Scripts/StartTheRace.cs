using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class StartTheRace : MonoBehaviour {


	public GameObject[] Cars; 
	public GameObject StartPosition;
	public GameObject[] Position;
	public TMP_Text[] DriverNames;
	public TMP_Text[] Status;
	public TMP_Text[] Laps;
    public TMP_Text[] TeamNames;
    public TMP_Text[] Gaps;
    private TMP_Text DriverName;
    private TMP_Text teamColor;
	public GameObject[] PitBoxes;
	private int raceFinished = 0;
    public float pitLaneGapDistance;


    private float timeMinutesSelf;
    private float timeSecondsSelf;
    private float timeFractionsSelf;
    private float timeRawSelf;

    private float timeRaw;
    private float timeGap;

    private float timeGapMinutes;
    private float timeGapSeconds;
    private float timeGapFractions;

    public static GameObject smoke;
    public static GameObject mistake;
    public GameObject smokeSprite;
    public GameObject mistakeSprite;

    public static float raceTimer;
    private int raceStarted = 0;


	public void RaceStart(){

        raceStarted = 1;
        smoke = smokeSprite;
        mistake = mistakeSprite;

		Debug.Log ("Start Button is pushed!");
		StartCoroutine (StartWait());

	}



    public void FixedUpdate()
    {

        if (raceStarted == 1) {

            raceTimer = raceTimer + Time.deltaTime;
        } 

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
            TeamNames[i].text = (Cars[i].GetComponent<Lapping>().teamname).ToString();
            TeamNames[i].color = Cars[i].GetComponent<Lapping>().teamColor;
        }



		}


	public void RaceOrder(){
	
		Debug.Log ("RaceOrder is called!");
		if (raceFinished == 0) {
		
			Cars = Cars.OrderByDescending (x => x.GetComponent<Lapping>().distanceTraveled).ToArray ();


			for (int i = 0; i < Cars.Length; i++) {

				//Debug.Log ("Inside the for loop now...");
				DriverNames[i].text = (Cars [i].GetComponent<Lapping> ().drivername).ToString();
                TeamNames[i].text = (Cars[i].GetComponent<Lapping>().teamname).ToString();
                TeamNames[i].color = Cars[i].GetComponent<Lapping>().teamColor;
                Laps[i].text = (Cars[i].GetComponent<Lapping>().pitCounter).ToString();
                Status[i].text = Cars[i].GetComponent<Lapping>().status;

                if (i != 0)
                {

                    if (Cars[i].GetComponent<Lapping>().isCrashed == 1 || Cars[i].GetComponent<Lapping>().isRetired == 1)
                    {
                        Gaps[i].text = "lap " +Cars[i].GetComponent<Lapping>().lap.ToString();


                    } else
                    {





                        timeGap = Cars[0].GetComponent<Lapping>().distanceTraveled - Cars[i].GetComponent<Lapping>().distanceTraveled;
                        timeGap = timeGap * 2;
                        timeGapSeconds = Mathf.Floor(timeGap / 100);
                        timeGapFractions = Mathf.Floor((timeGap * 1000f) % 1000f);

                        Gaps[i].text = string.Format("+{0:00}.{1:000}", timeGapSeconds, timeGapFractions);
                    }

                    //timeGap = Cars[0].GetComponent<Lapping>().distanceGap - Cars[i].GetComponent<Lapping>().distanceGap;
                    //timeGapSeconds = Mathf.Floor(timeGap);
                    //timeGapFractions = Mathf.Floor((timeGap * 1000f) % 1000f);                    
                     
                    //Gaps[i].text = (Cars[0].GetComponent<Lapping>().distanceTraveled - Cars[i].GetComponent<Lapping>().distanceTraveled).ToString();


                } else if (i == 0){

                    Gaps[i].text = "lap " + Cars[i].GetComponent<Lapping>().lap.ToString();
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
                
                
		
			}
		

			
		}

	}


}











	