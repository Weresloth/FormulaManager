using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class StartTheRace : MonoBehaviour {


    public int raceDuration;

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


    public GameObject pitStopPanel1;
    public GameObject pitStopPanel2;


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
			yield return new WaitForSeconds(0.1f);


		}
	
		StartCoroutine (RaceStandings ());

        if (DriverPanel.playerCount == 2)
        {
            GameObject.Find("DriverPanel1").GetComponent<DriverPanel>().ApplyTheStart();
            GameObject.Find("DriverPanel2").GetComponent<DriverPanel>().ApplyTheStart();
        }

        

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

				DriverNames[i].text = (Cars [i].GetComponent<Lapping> ().drivername).ToString();
                TeamNames[i].text = (Cars[i].GetComponent<Lapping>().teamname).ToString();
                TeamNames[i].color = Cars[i].GetComponent<Lapping>().teamColor;
                Laps[i].text = (Cars[i].GetComponent<Lapping>().pitCounter).ToString();
                Status[i].text = Cars[i].GetComponent<Lapping>().status;

                if (i != 0)
                {

                    if (Cars[i].GetComponent<Lapping>().isCrashed == 1 || Cars[i].GetComponent<Lapping>().isRetired == 1)
                    {
                        Gaps[i].text = "lap " +(Cars[i].GetComponent<Lapping>().lap-1).ToString();


                    } else
                    {



                        if (Cars[i].GetComponent<Lapping>().lap < Cars[0].GetComponent<Lapping>().lap)
                        {


                            if (Cars[i].GetComponent<Lapping>().raceFinished == 0)
                            {


                                Debug.Log("We are in the -200 part!");
                                timeGap = Cars[0].GetComponent<Lapping>().distanceTraveled - Cars[i].GetComponent<Lapping>().distanceTraveled;
                                timeGap = timeGap * 2;                                
                                timeGapSeconds = Mathf.Floor(timeGap / 100);
                                timeGapFractions = Mathf.Floor((timeGap * 1000f) % 1000f);

                                timeGapSeconds = timeGapSeconds - 200f;

                                Gaps[i].text = string.Format("+{0:00}.{1:000}", timeGapSeconds, timeGapFractions);

                            }
                            else
                            {

                                Gaps[i].text = "-" + (Cars[0].GetComponent<Lapping>().lap - Cars[i].GetComponent<Lapping>().lap).ToString() + " lap";

                            }

                            


                        } else
                        {

                            timeGap = Cars[0].GetComponent<Lapping>().distanceTraveled - Cars[i].GetComponent<Lapping>().distanceTraveled;
                            timeGap = timeGap * 2;
                            timeGapSeconds = Mathf.Floor(timeGap / 100);
                            timeGapFractions = Mathf.Floor((timeGap * 1000f) % 1000f);

                            Gaps[i].text = string.Format("+{0:00}.{1:000}", timeGapSeconds, timeGapFractions);



                        }


                        
                    }

                } else if (i == 0){

                    Gaps[i].text = "lap " + Cars[i].GetComponent<Lapping>().lap.ToString();
                }

                             
                
                
                
                
		
			}
		

			
		}

	}


    public void PitDropdown1()
    {


        if (pitStopPanel1.activeSelf == false)
        {

            pitStopPanel1.SetActive(true);

        }
        else
        {

            pitStopPanel1.SetActive(false);

        }


    }

    public void PitDropdown2()
    {


        if (pitStopPanel2.activeSelf == false)
        {

            pitStopPanel2.SetActive(true);

        }
        else
        {

            pitStopPanel2.SetActive(false);

        }


    }




}











	