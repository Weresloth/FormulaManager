using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class Lapping : MonoBehaviour {


	public GameObject[] points;
	public GameObject car;
	public float carspeed;
	private int x = 0;
	public int lap = 1;
    private int checkpoints = 0;
	private float tyreHP;
	public float fuel;
	private float carHP;
    public float distanceTraveled;
	private Vector2 LastPosition;	
	private GameObject[] pitBox;
	private int isPitting = 0;
	private float tyreHPMax = 100f;
	private float carHPMax = 100f;	
	private float driverForm;
    private float teamSpeed;
    public float driverspeed;
    private float mistakeChance;
    private float crashChance;
    public int crashEnabled;
    

	[Range(1,5)]
	public int paceNumber = 1;

	[Range(1,3)]
	public int revNumber = 1;

	public string status;
	private int wearChecker = 0;
	private int OnTrack = 0;
	private float tyreWear;
	private float carWear;
	private float engineWear;
	private float fuelBurn;
	private int isRetired = 0;
	private int isCrashed = 0;
	CircleCollider2D carCollider;
	SpriteRenderer carImage;
	public GameObject Initials;
	private float TheOtherCarFuel;
	Dictionary<string, float> Teams = new Dictionary<string, float> ();
	Dictionary<int, float> LapTimes = new Dictionary<int, float> ();
	public float personalTimer;
	public float personalTimerTotal;
	public int raceDistance;
	public int raceFinished = 0;
	public int pitCounter = 0;
	public float debugTestSpeed;
    private bool raceStartChecker;
    private float accelSpeed;
    //Rigidbody2D carRB;

    [Header("Stats")]
    public float driverskill;
    [Tooltip("1 to 5")]
    public float qualifyingNumber;
    [Tooltip("95-98")]
    public float mistakeSkill;
    [Tooltip("Less the better(1-5)")]
    public float crashRate;

    [Header("Driver Details")]
    public string drivername;
    public string driverShort;
    public string teamname;
    public int carNumber;
    public int pitBoxNumber;
    public Color teamColor;
    public Color teamColor2;


    //public float overtakingskill;
    //private float overtakinghandicap;
    


    void Start () {





		//carRB = gameObject.GetComponent<Rigidbody2D>();


		personalTimerTotal = 0;
		personalTimer = 0;
        accelSpeed = 0;
        crashEnabled = 0;

		//List of Team Performances
		Teams.Add ("Ferrari", 10);
		Teams.Add ("BMW", 10);
		Teams.Add ("Ravel", 9);
		Teams.Add ("Audi", 9);
		Teams.Add ("Etihad", 6);
		Teams.Add ("Stacey", 5);
		Teams.Add ("Williams", 3);
		Teams.Add ("Schumacher", 2);
		Teams.Add ("Kitano", 5);
		Teams.Add ("Conrad", 6);

		raceDistance = 12;

		teamSpeed = Teams [teamname];

		OnTrack = 1;

		//These for adding initials to the sprite
		Initials.GetComponent<TextMeshPro>().text = driverShort;
		Initials.GetComponent<TextMeshPro>().color = teamColor2;
		Instantiate (Initials, gameObject.transform.position, Quaternion.identity, gameObject.transform);

		//Starting strategy of the driver
		carHP = carHPMax;
		tyreHP = tyreHPMax;
		for (int i = 0; i < GameObject.Find("racetrack").GetComponent<StartTheRace>().Cars.Length; i++){

			if (GameObject.Find ("racetrack").GetComponent<StartTheRace> ().Cars [i].GetComponent<Lapping> ().OnTrack == 1) {
				if (GameObject.Find ("racetrack").GetComponent<StartTheRace> ().Cars [i].GetComponent<Lapping> ().drivername != drivername && GameObject.Find ("racetrack").GetComponent<StartTheRace> ().Cars [i].GetComponent<Lapping> ().teamname == teamname) {

					TheOtherCarFuel = GameObject.Find ("racetrack").GetComponent<StartTheRace> ().Cars [i].GetComponent<Lapping> ().fuel;
					fuel = TheOtherCarFuel + 10f;

				}

			}
			}





		driverForm = Random.Range (0.1f, 0.6f);
		carImage = car.GetComponent<SpriteRenderer>();
		carImage.color = teamColor;
		CalculateSpeed ();
		LastPosition = transform.position;


		pitBox = GameObject.Find ("racetrack").GetComponent<StartTheRace>().PitBoxes;
        points = GameObject.Find("racetrack").GetComponent<StartTheRace>().Position;
        StartCoroutine(RaceStartChecker());

	}


    IEnumerator RaceStartChecker()
    {

        yield return new WaitForSeconds(3f);
        raceStartChecker = true;
        yield return new WaitForSeconds(3f);
        crashEnabled = 1;
    }


    void CalculateSpeed(){

		Random.InitState (System.DateTime.Now.Millisecond);
		//Random.seed = System.DateTime.Now.Millisecond;
		mistakeChance = Random.Range (0f, 50f);


		if (Random.Range (0f, 1000f) <= crashRate && crashEnabled == 1 && isPitting == 0 && fuel > 0 && carHP > 0) {
            Debug.Log(drivername + " has crashed! Oops!");
            isCrashed = 1;
           	carHP = 0;
            carspeed = 0;
            accelSpeed = 0;
            
					
		}

		driverspeed = (driverskill * 0.1f) * driverForm;
		if (fuel > 0 && carHP > 0) {

			if ((mistakeSkill + mistakeChance) < 100){
                                
                Debug.Log (drivername + " made a mistake!");
				carHP = carHP - 5;
				driverForm = 0.1f;

			} 


			carspeed = ((tyreHP * 0.01f) - (fuel * 0.005f) + (carHP * 0.001f) + (driverspeed)) + 0.3f + (teamSpeed * 0.01f) + debugTestSpeed;
			

		} else if (isRetired == 0) {
			carspeed = 0;
			carCollider = car.GetComponent<CircleCollider2D> ();
			carCollider.enabled = false;

			if (isCrashed == 1) {

				Debug.Log (drivername + "CRASHED!");
				status = "crashed";

				} else if (fuel <= 0) {
					Debug.Log (drivername + " is out of fuel!"); 
					status = "fuel";
				} else if (carHP <= 0) { 
					Debug.Log (drivername + " has chassis problem!");
					status = "chassis";
				}


			GameObject.Find ("racetrack").GetComponent<EndRace> ().RetireFromRace (gameObject);
			isRetired = 1;
		}
	}




	IEnumerator PitStop(){
	
		tyreHP = tyreHPMax;
		carHP = carHP + 30;
		carHP = (carHP > carHPMax) ? carHPMax : carHP;
		status = "pitstop";


		switch (Random.Range(1, 4)){

		case 1: 
			fuel = fuel + 25;
			Debug.Log (drivername +" Pitting");
			yield return new WaitForSeconds(18f);
			break;
		case 2:
			fuel = fuel + 35;
			Debug.Log (drivername + " Pitting");
			yield return new WaitForSeconds(18.5f);
			break;
		case 3:
			fuel = fuel + 45;
			Debug.Log (drivername + " Pitting");
			yield return new WaitForSeconds(19f);
			break;
		}


		Debug.Log (drivername + " is exiting pits!");

        for (int i = 0; i < points.Length; i++)
        {
            checkpoints = checkpoints + 1;
            if (points[i].GetComponent<Checkpoints>().pitExit == true)
            {
                x = i + 1;
                gameObject.transform.position = points[i].transform.position;
                accelSpeed = 0;
                break;
            }
           


        }
        


        wearChecker = 0;
		CalculateSpeed ();
		carImage = car.GetComponent<SpriteRenderer>();
		carImage.enabled = true;
		pitCounter += 1;
		isPitting = 0;
		status = "";

		
			

	
	}


	IEnumerator CalculateWears(){

		wearChecker = 1;
		tyreWear = 0f;
		fuelBurn = 0f;
		carWear = 0f;

		switch (paceNumber) {

		case 1:
			tyreWear = tyreWear + 0.1f;
			fuelBurn = fuelBurn + 0.5f;
			carWear = carWear + 0.1f;
			break;
		case 2:
			tyreWear = tyreWear + 0.5f;
			fuelBurn = fuelBurn + 0.8f;
			carWear = carWear + 0.3f;
			break;
		case 3:
			tyreWear = tyreWear + 1f;
			fuelBurn = fuelBurn + 1.1f;
			carWear = carWear + 0.5f;
			break;
		case 4:
			tyreWear = tyreWear + 1.5f;
			fuelBurn = fuelBurn + 1.4f;
			carWear = carWear + 0.7f;
			break;
		case 5:
			tyreWear = tyreWear + 2f;
			fuelBurn = fuelBurn + 1.7f;
			carWear = carWear + 0.9f;
			break;
		}
	
		switch (revNumber) {

		case 1:
			tyreWear = tyreWear + 0.5f;
			fuelBurn = fuelBurn + 0.5f;
			carWear = carWear + 0.1f;
			break;
		case 2:
			tyreWear = tyreWear + 1f;
			fuelBurn = fuelBurn + 2f;
			carWear = carWear + 0.5f;
			break;
		case 3:
			tyreWear = tyreWear + 1.5f;
			fuelBurn = fuelBurn + 5f;
			carWear = carWear + 1f;
			break;


		}


		tyreHP = tyreHP - tyreWear;
		carHP = carHP - carWear;
		fuel = fuel - fuelBurn;

		fuel = (fuel < 0) ? 0 : fuel; 
		carHP = (carHP < 0) ? 0 : carHP;
		tyreHP = (tyreHP < 0) ? 0 : tyreHP;
		yield return new WaitForSeconds(5f);

		if (isPitting == 0) wearChecker = 0;

	}
	void OnCollisionEnter2D (Collision2D collision){

		if (collision.gameObject.GetComponent<Lapping>().lap > lap) {

			Debug.Log (drivername+" must yield!!!!");
			carCollider = car.GetComponent<CircleCollider2D> ();
			carCollider.enabled = false;
			StartCoroutine (BlueFlag ());
		}

	}




	IEnumerator BlueFlag(){
	
		yield return new WaitForSeconds(1f);
		carCollider.enabled = true;
	
	}

	/*void onCollisionExit2D (Collision2D Collision) {
		
		Debug.Log (drivername + " No more collision");
		carCollider = car.GetComponent<CircleCollider2D> ();
		carCollider.enabled = true;

	}*/


	void FixedUpdate () {

		personalTimer = personalTimer + Time.deltaTime;

        if (fuel > 0 && carHP > 0)
        {
            accelSpeed = accelSpeed + 0.0001f;
            carspeed = carspeed + accelSpeed;
            
        }


        if (raceFinished == 0) {
			personalTimerTotal = personalTimerTotal + Time.deltaTime;
		}

		distanceTraveled = (lap * 10000) + (checkpoints * 100) - (Vector2.Distance(car.transform.position, points[x].transform.position));
		LastPosition = transform.position;


		if (Vector2.Distance(car.transform.position, points[x].transform.position) < 0.1f) {

            if (points[x].GetComponent<Checkpoints>().start == true && raceStartChecker == true) {

                if (lap == raceDistance || GameObject.Find("racetrack").GetComponent<EndRace>().leaderFinished == 1)
                {

                    Debug.Log(drivername + " finished the race!");
                    raceFinished = 1;
                    //distanceTraveled += 5000f;
                    x = 0;
                    gameObject.transform.position = pitBox[pitBoxNumber].transform.position;
                    isPitting = 1;
                    GameObject.Find("racetrack").GetComponent<EndRace>().RaceFinish(gameObject);
                    accelSpeed = 0;
                }
                else if (raceFinished == 0)
                {
                    x = 0;
                    LapTimes.Add(lap, personalTimer);
                    personalTimer = 0;
                    lap = lap + 1;
                    //distanceTraveled += 5000f;
                    driverForm = Random.Range(0.1f, 0.6f);                
                }
                
            }


            if (GameObject.Find("racetrack").GetComponent<EndRace>().leaderFinished == 0 && lap < raceDistance && points[x].GetComponent<Checkpoints>().pitEnter == true)
            {
                if (fuel < 15) {
                    Debug.Log(drivername + " entering pits!");
                    checkpoints = checkpoints + ((points.Length - x) - 1);
                    isPitting = 1;
                    wearChecker = 1;
                    carspeed = 0;
                    gameObject.transform.position = pitBox[pitBoxNumber].transform.position;
                    StartCoroutine(PitStop());
                    LapTimes.Add(lap, (personalTimer+10));
                    personalTimer = 0;
                    lap = lap + 1;
                    //distanceTraveled += 5000f;
                    driverForm = Random.Range(0.1f, 0.6f);
                    accelSpeed = 0;
                    CalculateSpeed();

                }

            }


            if (points[x].GetComponent<Checkpoints>().turn == true)
            {
                accelSpeed = 0;
                CalculateSpeed();
            }
                
            x = x + 1;
            checkpoints = checkpoints + 1;
            if (x >= points.Length) x = 0;
			//distanceTraveled += 750f;
			

            





            
            
            
            		
		}

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


        //If not in the pits, go towards the next checkpoint
        if (isPitting == 0) car.transform.position = Vector2.MoveTowards (car.transform.position, points[x].transform.position, carspeed * Time.deltaTime);


        //If wear is being applied, calculate it
		if (wearChecker == 0) {
			StartCoroutine (CalculateWears());		
		}

		}

}