using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour {

	GameMaster master;
	HeroManager manager; 
	public Quest quest; 

	//basic stat info. 
	public int maxHP = 50;
	public int HP = 50;
	public int stam = 6; // how long is a hero willing to stay out in the field before returning.  
	public int maxStam = 6; // how long is a hero willing to stay out in the field before returning.  

	public int healRate = 4; // lower numbers are faster, do not set to zero. 

	public int str = 5;
	public int dex = 5;
	public int wis = 5;
	public int Int = 5; 


	public int heroLevel = 1;
	public int curentEXP = 0; 
	public int EXP_TO_NEXT_LEVEL = 50;  //

	public string role = "";  // set this up an emun later 
	public string heroName = "Place holder bob";

	public int objID = -99; 
	public bool NewHero= true; // if true at start roll random starting stats? i guess. 

	public bool in_town = true; // is the hero in town? 
	public bool ReadyForQuests = true; // is the hero ready to do stuff. 


	public bool active = true;
	int disabled = 5 ; //when knocked out set how many days will be inactive for
	int disabled_count = 0 ; 
	//----------

	public GameObject heroGraphic; // hide this when out of town. 
	public float speed = 6f;

	// lists for moveing around. 
	public List<GameObject> placesToGo; 
	public List<GameObject> placesToReturn; // mostly just home, theKEEP and then home, or a tavern or w/e


	public bool idle = true; // if idleing, just walk around town, or something. 
	public bool goingOnAQuestToday = false; 
	public bool returning = false;

	// Use this for initialization


	void Start () {
		// i need the game master? only if i want to have an objID // yeah lets get the gameMaster, also grab HeroManager. 

		GameObject	Master = GameObject.FindGameObjectsWithTag("GameMaster")[0];
		master = Master.GetComponent<GameMaster> ();
		manager = Master.GetComponent<HeroManager> ();
		this.gameObject.tag = "Hero";
		heroGraphic.tag = "Hero"; /// wooo, need to always update this when i create a herographic

		if(NewHero){
		setHeroStats (); // need to only call this if hero is new
			objID = master.getobjid();
			// set its graphic to heroGraphic
		}

		manager.add_a_hero (this);


		GameMaster.newday += endOfDayStuff;
	}
	
	// Update is called once per frame
	void Update () {
	
		// some function to have them walk around the map | wow its time to write this code. awsomne! for now filler? 
		if (in_town) {
			heroMovement ();
		}
			// i will have a list of places to go, builds and then a gate to exit at. 


		// move from building to building till you leave for your quest 

		// when hero returns.. do stuff?  then go home when you get back .


	}
		






	void setHeroStats(){
	
		// when a hero is 1st created, need a way to load its stats and name,if it doesnt have any to roll and set them. 
	
		// setp one check a look up for this hero? -> wait what? how will i find this hero in a look up if its a new hero. 


	}


	public void setHeroStats(int ID){ // if a function calls this, make this hero one from a set id? 

		// load all data to this hero from database? idk. 


	}






	void endOfDayStuff(){

		// heal some HP. do any clean up stuff. 
		if (in_town) {
			if (active) {
				dailyHeal ();
			} else {
		


				if (disabled_count >= disabled) {
					// hero is now active again.
					active = true; 
					HP = maxHP; // healed to full. 
				}


			}
		}
		else {
			Debug.Log ("hero is out way to late " + heroName); 
		}
	}



public void dailyHeal ()  // heal for a percent of heros max HP
	{

		// heal some amount
		int	heal = maxHP / healRate;
		if (heal < 1) {
			heal = 1;
		}
		HP = HP + heal;
		if (HP > maxHP) {
			HP = maxHP;
		}
		stam = maxStam; // refill stam every day. 

	}



	public void PlacesToGoToday(List<GameObject> points, bool leavetown){

		if(in_town && ReadyForQuests){
		goingOnAQuestToday = leavetown;
			placesToGo.AddRange (points);
		//	placesToGo = points; // this is just setting a refrence to one list, i want to make a copy of it. 

		
			// set all the places to go in order. huh... how to know if it ends with a quest. 
		// if you dont leave town just end of doing ideal loop after visting stuff.
			idle = false; 
			ReadyForQuests = false;
		}

	}







	public void heroMovement(){
		if (in_town && idle == false && returning == false) {  // hero is in the town, and has stuff todo today. 
		//	Debug.Log("walking with places to go");
			int Left_in_List = placesToGo.Count;

			// im in town, where should i go? 
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, placesToGo [0].transform.position, step);
			// after each step check if i got to the point, if last point go on a quest if im going on a quest today.
			var heading = placesToGo [0].transform.position - transform.position;
			if (heading.sqrMagnitude < 1) {
				Debug.Log ("arived");
				Debug.Log (heroName);
				this.placesToGo.RemoveAt (0); // remove the location we were walking to. (could also just tick up the value)
				if (Left_in_List == 1) {
					Debug.Log ("this was the last location");
					if (goingOnAQuestToday) {
						
						this.in_town = false; // now i just need to send this hero on a quest. 
						// hide game object. 
						Hide_HeroGraphic();

						quest.addHeroToQuest (this);

						goingOnAQuestToday = false; // may want to know who went on quests. 

						//this.returnFromOutOfTown();
					}


									}

			}
		} else if (in_town && idle && returning == false) { // hero already did stuff today, go home? or walkaround, idk or care. 
		
		//	Debug.Log("idleing");
			
		}
		else if (in_town && ReadyForQuests == false && returning == true) {  // after quest clean up walks, go home/ or any after quest walking

			int Left_in_List = placesToGo.Count;
			//Debug.Log (Left_in_List);
			float speed = 3f;
			// im in town, where should i go? 
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, placesToGo [0].transform.position, step);
			// after each step check if i got to the point, if last point go on a quest if im going on a quest today.
			var heading = placesToGo [0].transform.position - transform.position;
			if (heading.sqrMagnitude < 1) {
				Debug.Log ("arived");
				Debug.Log (heroName);
				placesToGo.RemoveAt (0); // remove the location we were walking to. (could also just tick up the value)
				if (Left_in_List == 1) {
					Debug.Log ("this was the last location");
					ReadyForQuests = true; // now you are ready for a new day. 
					idle = true;
					returning = false;
				}

			}
		}
		else{ // hero is not in town, dont do anything till he gets back. 
		//	Debug.Log("else");
		}

	}



	public void returnFromOutOfTown()
		{
		returning = true;
		in_town = true;
		Show_HeroGraphic ();
		ReadyForQuests = false; // dont take new path orders till you get home.
		placesToGo.AddRange( placesToReturn);   // (create this when ever a new hero is created.);

		// try to level up. 
		levelCheck();

	}



	public void gainExp(int expGained){
	
	
		curentEXP = curentEXP + expGained;


	}


	public void levelCheck(){
	
	
		if (curentEXP >= EXP_TO_NEXT_LEVEL && heroLevel<manager.levelCap) {
		
		
			// do level up stuff. 

			//level up
			heroLevel = heroLevel+1;  // should i do this 1st or last?  

			// increase stats
			maxHP= maxHP+ 10; // need to figure out some system
			HP = HP + 10; // heal for the ammount added. 	
			// increase HP every time, rest IDK, roll for it i guess, works for now. 

			// code below is awful rewrite once real system is planeds. 
			if (Random.Range (1, 100) > 50) {
				maxStam = maxStam + 1;
			}

			if (Random.Range (1, 100) > 50) {
				str = str + 1;
			}
			if (Random.Range (1, 100) > 50) {
				dex = dex + 1;
			}
			if (Random.Range (1, 100) > 50) {
				wis = wis + 1;
			}
			if (Random.Range (1, 100) > 50) {
				Int = Int + 1;
			}
			/// end awful code. 




			// increase EXP to next level
			EXP_TO_NEXT_LEVEL= manager.EXP_For_Next_Level[heroLevel];



		}



	}







	// ui hooks 


	public void Show_HeroGraphic(){
	
	
		heroGraphic.SetActive (true);

	
	}

	public void Hide_HeroGraphic(){
		heroGraphic.SetActive (false);
	}





}
