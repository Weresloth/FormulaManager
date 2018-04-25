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
	public float tyreHP;
	public float fuel;
	private float carHP;
    public float distanceTraveled;
	private Vector2 LastPosition;	
	private GameObject[] pitBox;
	private int isPitting = 0;	
	private float carHPMax = 100f;
    public float tyreHPMax;	
	private float driverForm;
    private float teamSpeed;
    public float driverspeed;
    private float mistakeChance;
    private float crashChance;
    public int crashEnabled;
    public float finalDistance;
    public float distanceGap;
    private float engineSpeed;
    private float engineReliability;
    public float tyreSpeed;
    public float finishTime;

    public bool pitCalled;
    public string tyreChoice;
    public float refuelChoice;
    public int tyreCompoundSelector;


    public bool playerCar1;
    public bool playerCar2;
            
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
	public int isRetired = 0;
	public int isCrashed = 0;
    public int isEngineDied = 0;
	CircleCollider2D carCollider;
	SpriteRenderer carImage;
	public GameObject Initials;
	private float TheOtherCarFuel;
	Dictionary<string, float> Teams = new Dictionary<string, float> ();
	Dictionary<int, float> LapTimes = new Dictionary<int, float> ();
    Dictionary<string, float> EngineSpeeds = new Dictionary<string, float>();
    Dictionary<string, float> EngineHP = new Dictionary<string, float>();
    Dictionary<string, float> TyreSpeed = new Dictionary<string, float>();
    Dictionary<string, float> TyreDurability = new Dictionary<string, float>();
    public float personalTimer;
	public float personalTimerTotal;
	public int raceDistance;
	public int raceFinished = 0;
	public int pitCounter = 0;
	public float debugTestSpeed;
    private bool raceStartChecker;
    private float accelSpeed;
    public string tyreCompound;
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
    public string enginename;


    private float wetness;
    private float wetCrashChance;
    private float wetTyrePenalty;
    private float wetMistakeChance;
    private bool wrongTyres;
    private float tempature;

    //public float overtakingskill;
    //private float overtakinghandicap;
    


    void Start () {
        
		//carRB = gameObject.GetComponent<Rigidbody2D>();


		personalTimerTotal = 0;
		personalTimer = 0;
        accelSpeed = 0;
        crashEnabled = 0;
        wrongTyres = false;

		//List of Team Performances
		Teams.Add ("Ferrari", 17);
		Teams.Add ("Mercedes", 18);
		Teams.Add ("Red Bull", 16);
		Teams.Add ("Renault", 11);
		Teams.Add ("McLaren", 11);
		Teams.Add ("Toro Rosso", 8);
		Teams.Add ("Williams", 8);
		Teams.Add ("Jaguar", 3);
		Teams.Add ("Sauber", 7);
		Teams.Add ("Force India", 10);
        Teams.Add("HAAS", 10);
        Teams.Add("Minardi", 1);

        //List of Engine Speeds
        EngineSpeeds.Add("Ferrari", 12);
        EngineSpeeds.Add("Mercedes", 13);
        EngineSpeeds.Add("Renault", 11);
        EngineSpeeds.Add("Cosworth", 7);
        EngineSpeeds.Add("Honda", 9);

        //List of Engine Reliabilities
        EngineHP.Add("Ferrari", 0.6f);
        EngineHP.Add("Mercedes", 0.1f);
        EngineHP.Add("Renault", 0.2f);
        EngineHP.Add("Cosworth", 0.6f);
        EngineHP.Add("Honda", 0.6f);

        //List of Tyre Speeds
        TyreSpeed.Add("Hard", 3);        
        TyreSpeed.Add("Soft", 5);
        TyreSpeed.Add("Wet", 0);

        //List of Tyre Durabilities
        TyreDurability.Add("Hard", 80);        
        TyreDurability.Add("Soft", 40);
        TyreDurability.Add("Wet", 200);

        //How many laps is the race distance?
        raceDistance = GameObject.Find("racetrack").GetComponent<StartTheRace>().raceDuration;

        //Retrieve team and engine values according to the names
		teamSpeed = Teams [teamname];
        engineSpeed = EngineSpeeds[enginename];
        engineReliability = EngineHP[enginename];

        OnTrack = 1;

		//These for adding initials to the sprite
		Initials.GetComponent<TextMeshPro>().text = driverShort;
		Initials.GetComponent<TextMeshPro>().color = teamColor2;
		Instantiate (Initials, gameObject.transform.position, Quaternion.identity, gameObject.transform);

        //-------------
		//Starting strategy of the driver
		carHP = carHPMax;
        wetness = GameObject.Find("WeatherControl").GetComponent<WeatherControl>().trackWetness;

        if (playerCar1 == false && playerCar2 == false)
        {

            //Deciding the tyre compound based on randomization and wetness
            if (wetness < 30)
            {
                int randomTyrePicker = Random.Range(1, 3);

                if (randomTyrePicker == 1)
                {

                    tyreCompound = "Soft";
                    tyreHP = TyreDurability[tyreCompound];
                    tyreHPMax = TyreDurability[tyreCompound];
                    tyreSpeed = TyreSpeed[tyreCompound];

                }
                else if (randomTyrePicker == 2)
                {

                    tyreCompound = "Hard";
                    tyreHP = TyreDurability[tyreCompound];
                    tyreHPMax = TyreDurability[tyreCompound];
                    tyreSpeed = TyreSpeed[tyreCompound];

                }


            } else if (wetness > 30)
            {
                tyreCompound = "Wet";
                tyreHP = TyreDurability[tyreCompound];
                tyreHPMax = TyreDurability[tyreCompound];
                tyreSpeed = TyreSpeed[tyreCompound];


            }


            switch (Random.Range(1, 5))
            {

                case 1:
                    fuel = 25;                    
                    break;
                case 2:
                    fuel = 35;                    
                    break;
                case 3:
                    fuel = 40;                    
                    break;
                case 4:
                    fuel = 50;
                    break;
            }


            //Checking fuel to make it more than the team-mate ahead
            for (int i = 0; i < GameObject.Find("racetrack").GetComponent<StartTheRace>().Cars.Length; i++)
            {

                if (GameObject.Find("racetrack").GetComponent<StartTheRace>().Cars[i].GetComponent<Lapping>().OnTrack == 1)
                {
                    if (GameObject.Find("racetrack").GetComponent<StartTheRace>().Cars[i].GetComponent<Lapping>().drivername != drivername && GameObject.Find("racetrack").GetComponent<StartTheRace>().Cars[i].GetComponent<Lapping>().teamname == teamname)
                    {

                        TheOtherCarFuel = GameObject.Find("racetrack").GetComponent<StartTheRace>().Cars[i].GetComponent<Lapping>().fuel;
                        fuel = TheOtherCarFuel + 10f;

                    }

                }
            }

            ChangePaceAndRev();

        }
       
        if (playerCar1 == true)
        {


            fuel = float.Parse(GameObject.Find("DriverPanel1").GetComponent<DriverPanel>().refuelAmount.text);
            tyreCompoundSelector = GameObject.Find("DriverPanel1").GetComponent<DriverPanel>().tyreChoice.value;

            if (tyreCompoundSelector == 0) { tyreCompound = "Soft"; }
            if (tyreCompoundSelector == 1) { tyreCompound = "Hard"; }
            if (tyreCompoundSelector == 2) { tyreCompound = "Wet"; }

            tyreHP = TyreDurability[tyreCompound];
            tyreHPMax = TyreDurability[tyreCompound];
            tyreSpeed = TyreSpeed[tyreCompound];

        }

		

        if (playerCar2 == true)
        {

            fuel = float.Parse(GameObject.Find("DriverPanel2").GetComponent<DriverPanel>().refuelAmount.text);
            tyreCompoundSelector = GameObject.Find("DriverPanel2").GetComponent<DriverPanel>().tyreChoice.value;

            if (tyreCompoundSelector == 0) { tyreCompound = "Soft"; }
            if (tyreCompoundSelector == 1) { tyreCompound = "Hard"; }
            if (tyreCompoundSelector == 2) { tyreCompound = "Wet"; }

            tyreHP = TyreDurability[tyreCompound];
            tyreHPMax = TyreDurability[tyreCompound];
            tyreSpeed = TyreSpeed[tyreCompound];


        }





        //End of strategy part
        //------------

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
		mistakeChance = Random.Range (0f, 50f);

        //Retrieve the wetness of the track
        wetness = GameObject.Find("WeatherControl").GetComponent<WeatherControl>().trackWetness;

        //Modify crash and mistake chances according to wetness
        if (wetness > 40 && wetness < 75)
        {
            wetCrashChance = 1;
            wetMistakeChance = 1;

        } else if (wetness > 75)
        {
            wetCrashChance = 2;
            wetMistakeChance = 2;

        } else if (wetness == 100)
        {
            wetCrashChance = 3;
            wetMistakeChance = 3;
        } else
        {
            wetCrashChance = 0;
            wetMistakeChance = 0;
        }


        //Check tyres according to wetness
        if (wetness > -1 && wetness < 25)
        {
           if (tyreCompound == "Soft" || tyreCompound == "Hard")
            {
                wetTyrePenalty = 1 - (wetness * 0.03f);
                wrongTyres = false;
            } else
            {
                wetTyrePenalty = -4 - (wetness * 0.03f);
                wrongTyres = true;
            }
             
        } else if (wetness > 25 && wetness < 40)
        {

            if (tyreCompound == "Soft" || tyreCompound == "Hard")
            {
                wetTyrePenalty = 0.3f - (wetness * 0.03f);
                wrongTyres = false;
            }
            else
            {
                wetTyrePenalty = 0.7f - (wetness * 0.03f);
                wrongTyres = false;
            }

        } else if (wetness > 40 && wetness < 65)
        {
            if (tyreCompound == "Soft" || tyreCompound == "Hard")
            {
                wetTyrePenalty = -1.3f - (wetness * 0.03f);
                wrongTyres = false;
            }
            else
            {
                wetTyrePenalty = -0.1f - (wetness * 0.03f);
                wrongTyres = false;

            }

        } else if (wetness > 65 && wetness < 101)
        {
            if (tyreCompound == "Soft" || tyreCompound == "Hard")
            {
                wetTyrePenalty = -6 - (wetness * 0.03f);
                wrongTyres = true;
            }
            else
            {
                wetTyrePenalty = -1.5f - (wetness * 0.03f);
                wrongTyres = false;
            }

        }



            //Check whether the driver crashes or suffers an engine failure
            if (Random.Range (0f, 1000f) <= (crashRate+wetCrashChance) && crashEnabled == 1 && isPitting == 0 && fuel > 0 && carHP > 0 && isEngineDied == 0) {
            Debug.Log(drivername + " has crashed! Oops!");
            status = "retired";
            isCrashed = 1;
           	carHP = 0;
            carspeed = 0;
            accelSpeed = 0;
            					
		} else if (Random.Range (0f, 500f) <= engineReliability && isPitting == 0 && fuel > 0 && carHP > 0 && crashEnabled == 1 && isEngineDied == 0 && lap > 2)
        {
            Debug.Log(drivername + " has engine problem!");
            Instantiate(StartTheRace.smoke, gameObject.transform.position, Quaternion.identity, gameObject.transform);
            status = "retired";
            isEngineDied = 1;
            carHP = 0;
            carspeed = 0;
            accelSpeed = 0;


        }

        
        //Calculate driver's speed according to his skill level and his current form
		driverspeed = (driverskill * 0.1f) * driverForm;

        //If fuel and carHP are not 0, the car should move according to the speed calculated in this section
		if (fuel > 0 && carHP > 0) {

			if ((mistakeSkill + mistakeChance - wetMistakeChance) < 100 && isPitting == 0 && fuel > 0 && carHP > 0){
                                
                Debug.Log (drivername + " made a mistake!");
                Instantiate(StartTheRace.mistake, gameObject.transform.position + transform.up * 0.3f, Quaternion.identity, gameObject.transform);
                carHP = carHP - 10;
				driverForm = 0.1f;

			} 

            if (tyreHP < 10)
            {
                tyreSpeed = 1;

            }

            carspeed = ((tyreSpeed * 0.01f) - (fuel * 0.005f) + (carHP * 0.001f) + (driverspeed)) + (teamSpeed * 0.01f) + (engineSpeed * 0.05f ) + (paceNumber * 0.1f)+ (revNumber * 0.1f) + (wetTyrePenalty * 0.1f) + 0.5f + debugTestSpeed;
			if (carspeed < 0)
            {
                carspeed = 0.5f;

            }

		} else if (isRetired == 0) {
			carspeed = 0;
			carCollider = car.GetComponent<CircleCollider2D> ();
			carCollider.enabled = false;

            if (isCrashed == 1)
            {

                Debug.Log(drivername + "CRASHED!");
                status = "crashed";

                } else if (isEngineDied == 1) {
                    Debug.Log(drivername + "'s " + enginename + " engine failed!");
                    status = "engine";
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


    IEnumerator PitStopPlayer()
    {



        if (playerCar1 == true)
        {
            refuelChoice = float.Parse(GameObject.Find("DriverPanel1").GetComponent<DriverPanel>().refuelAmount.text);
            tyreCompoundSelector = GameObject.Find("DriverPanel1").GetComponent<DriverPanel>().tyreChoice.value;

        } else if (playerCar2 == true)
        {
            refuelChoice = float.Parse(GameObject.Find("DriverPanel2").GetComponent<DriverPanel>().refuelAmount.text);
            tyreCompoundSelector = GameObject.Find("DriverPanel2").GetComponent<DriverPanel>().tyreChoice.value;
            
        }
              

        if (tyreCompoundSelector == 0) { tyreCompound = "Soft"; }
        if (tyreCompoundSelector == 1) { tyreCompound = "Hard"; }
        if (tyreCompoundSelector == 2) { tyreCompound = "Wet"; }

        
        tyreHP = TyreDurability[tyreCompound];
        tyreHPMax = TyreDurability[tyreCompound];
        tyreSpeed = TyreSpeed[tyreCompound];


        carHP = carHP + 60;
        carHP = (carHP > carHPMax) ? carHPMax : carHP;
        status = "pitstop";

        float refuelTime = refuelChoice - fuel;


        if (refuelTime < 25f)
        {
            yield return new WaitForSeconds(17f);

        } else if (refuelTime > 25f && refuelTime < 50f)
        {

            yield return new WaitForSeconds(18f);

        } else if (refuelTime > 50f)
        {

            yield return new WaitForSeconds(19f);

        }

        fuel = refuelChoice; 

        Debug.Log(drivername + " is exiting pits!");

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
        CalculateSpeed();
        carImage = car.GetComponent<SpriteRenderer>();
        carImage.enabled = true;
        pitCounter += 1;
        isPitting = 0;
        distanceGap = distanceGap + GameObject.Find("racetrack").GetComponent<StartTheRace>().pitLaneGapDistance;
        status = "";


     
        if (playerCar1 == true)
        {
            GameObject.Find("DriverPanel1").GetComponent<DriverPanel>().pitcall.isOn = false;

        }
        else if (playerCar2 == true)
        {
            GameObject.Find("DriverPanel2").GetComponent<DriverPanel>().pitcall.isOn = false;
        }




    }




    IEnumerator PitStop(){

        paceNumber = 1;
        revNumber = 1;
        wetness = GameObject.Find("WeatherControl").GetComponent<WeatherControl>().trackWetness;
        if (wetness < 30)
        {
            int randomTyrePicker = Random.Range(1, 3);

            if (randomTyrePicker == 1)
            {

                tyreCompound = "Soft";
                tyreHP = TyreDurability[tyreCompound];
                tyreHPMax = TyreDurability[tyreCompound];
                tyreSpeed = TyreSpeed[tyreCompound];

            }
            else if (randomTyrePicker == 2)
            {

                tyreCompound = "Hard";
                tyreHP = TyreDurability[tyreCompound];
                tyreHPMax = TyreDurability[tyreCompound];
                tyreSpeed = TyreSpeed[tyreCompound];

            }

        } else if (wetness > 30)
        {
            tyreCompound = "Wet";
            tyreHP = TyreDurability[tyreCompound];
            tyreHPMax = TyreDurability[tyreCompound];
            tyreSpeed = TyreSpeed[tyreCompound];

        }
        
        carHP = carHP + 60;
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
			fuel = fuel + 50;
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
        distanceGap = distanceGap + GameObject.Find("racetrack").GetComponent<StartTheRace>().pitLaneGapDistance;
        status = "";
        ChangePaceAndRev();

		
			

	
	}


	IEnumerator CalculateWears(){


        tempature = GameObject.Find("WeatherControl").GetComponent<WeatherControl>().tempature;
        wearChecker = 1;
		tyreWear = 0f;
		fuelBurn = 0f;
		carWear = 0f;

        //Tempature affecting the tyre wear
        if (tempature > 28)
        {
            tyreWear = tyreWear + 0.2f;
        }


		switch (paceNumber) {

		case 1:
			tyreWear = tyreWear + 0.7f;
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
			tyreWear = tyreWear + 0.7f;
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
        if (playerCar1 == false && playerCar2 == false)
        {
            ChangePaceAndRev();
        }
            

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


    //AI picks random pace/rev values according to their car condition
    void ChangePaceAndRev() {

        if (carHP > 50 && fuel > 50)
        {
            paceNumber = Random.Range(3, 5);
            revNumber = Random.Range(2, 3);
        } else if (carHP < 30 && fuel < 30)
        {
            paceNumber = Random.Range(1, 3);
            revNumber = Random.Range(1, 2);
        }
        else
        {
            paceNumber = Random.Range(2, 4);
            revNumber = Random.Range(1, 3);
        }

        if (tyreHP < 20)
        {
            paceNumber = Random.Range(1, 2);
            revNumber = Random.Range(1, 2);

        }

        if (fuel < 10)
        {
            paceNumber = 1;
            revNumber = 1;

        }

    }


	void FixedUpdate () {

        
        if (isPitting == 0)
        {

            distanceGap = distanceGap + Vector2.Distance(car.transform.position, LastPosition);

        }

        if (fuel > 0 && carHP > 0)
        {
            accelSpeed = accelSpeed + 0.0001f;
            carspeed = carspeed + (accelSpeed/2);
            
        }


        if (raceFinished == 0) {
			personalTimerTotal = StartTheRace.raceTimer;
            distanceTraveled = (lap * 10000) + (checkpoints * 100) - (Vector2.Distance(car.transform.position, points[x].transform.position));
        } else if (raceFinished == 1)
        {
            distanceTraveled = finalDistance;

        }

       
		LastPosition = transform.position;


		if (Vector2.Distance(car.transform.position, points[x].transform.position) < 0.1f) {

            if (points[x].GetComponent<Checkpoints>().start == true && raceStartChecker == true) {

                if (lap == raceDistance || GameObject.Find("racetrack").GetComponent<EndRace>().leaderFinished == 1)
                {

                    Debug.Log(drivername + " finished the race!");
                    finishTime = StartTheRace.raceTimer;
                    finalDistance = distanceTraveled;
                    raceFinished = 1;
                    x = 0;
                    gameObject.transform.position = pitBox[pitBoxNumber].transform.position;
                    isPitting = 1;                    
                    GameObject.Find("racetrack").GetComponent<EndRace>().RaceFinish(gameObject);
                    accelSpeed = 0;
                }
                else if (raceFinished == 0)
                {
                    x = 0;
                    LapTimes.Add(lap, StartTheRace.raceTimer);
                    lap = lap + 1;
                    driverForm = Random.Range(0.1f, 0.6f);                
                }
                
            }


            if (GameObject.Find("racetrack").GetComponent<EndRace>().leaderFinished == 0 && lap < raceDistance && points[x].GetComponent<Checkpoints>().pitEnter == true)
            {
                if (fuel < 15 && playerCar1 == false && playerCar2 == false) {
                    Debug.Log(drivername + " entering pits!");
                    checkpoints = checkpoints + ((points.Length - x) - 1);
                    isPitting = 1;
                    wearChecker = 1;
                    carspeed = 0;
                    gameObject.transform.position = pitBox[pitBoxNumber].transform.position;
                    StartCoroutine(PitStop());
                    LapTimes.Add(lap, (StartTheRace.raceTimer+10));
                    personalTimer = 0;
                    lap = lap + 1;                    
                    driverForm = Random.Range(0.1f, 0.6f);
                    accelSpeed = 0;
                    CalculateSpeed();

                } else if (wrongTyres == true && playerCar1 == false && playerCar2 == false)
                {
                    Debug.Log(drivername + " entering pits!");
                    checkpoints = checkpoints + ((points.Length - x) - 1);
                    isPitting = 1;
                    wearChecker = 1;
                    carspeed = 0;
                    gameObject.transform.position = pitBox[pitBoxNumber].transform.position;
                    StartCoroutine(PitStop());
                    LapTimes.Add(lap, (StartTheRace.raceTimer + 10));
                    personalTimer = 0;
                    lap = lap + 1;
                    driverForm = Random.Range(0.1f, 0.6f);
                    accelSpeed = 0;
                    CalculateSpeed();

                }
                
                if (playerCar1 == true && pitCalled == true)
                {
                    Debug.Log(drivername + " entering pits!");
                    checkpoints = checkpoints + ((points.Length - x) - 1);
                    isPitting = 1;
                    wearChecker = 1;
                    carspeed = 0;
                    gameObject.transform.position = pitBox[pitBoxNumber].transform.position;
                    StartCoroutine(PitStopPlayer());
                    LapTimes.Add(lap, (StartTheRace.raceTimer + 10));
                    personalTimer = 0;
                    lap = lap + 1;
                    driverForm = Random.Range(0.1f, 0.6f);
                    accelSpeed = 0;
                    CalculateSpeed();

                }

                if (playerCar2 == true && pitCalled == true)
                {
                    Debug.Log(drivername + " entering pits!");
                    checkpoints = checkpoints + ((points.Length - x) - 1);
                    isPitting = 1;
                    wearChecker = 1;
                    carspeed = 0;
                    gameObject.transform.position = pitBox[pitBoxNumber].transform.position;
                    StartCoroutine(PitStopPlayer());
                    LapTimes.Add(lap, (StartTheRace.raceTimer + 10));
                    personalTimer = 0;
                    lap = lap + 1;
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
                        
            		
		}

		


        //If not in the pits, go towards the next checkpoint
        if (isPitting == 0) car.transform.position = Vector2.MoveTowards (car.transform.position, points[x].transform.position, carspeed * Time.deltaTime);


        //If wear is being applied, calculate it
		if (wearChecker == 0) {
			StartCoroutine (CalculateWears());		
		}

		}

}