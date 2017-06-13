using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour {

	public QuestMaster questMaster; // used to calculate exp gain, may just replace with a formula. 
	public EventMaster eventMaster; // used for events
	public MonsterMaster monsterMaster; // used for monster fights? may just add a list of monsters to each dng and work from that. 


	//dungeon = {"dngID": 1, "dngName":"The Cave","length":10, "progress":0, "monsterRate":30, "eventRate":10 }
	public int dngID = 1;
	public string dngName = "cave";
	public int dnglevel = 1; // used for exp for running the dng. also should relate to monsters that spawn and boss in some way. 
	public int length = 5; // how many segments are there, more is longer.
	public int stamCost = 1;  // how much stam does each segemnt cost. 
	public int mapProgressStartOfDay = 0 ; // how much was maped at the start of the day. 
	public int mapProgress = 0; // how many segments are maped. 
	public int mapChance = 75; // higher numbers are easyer to map.  
	public int monsterRate = 30; // higher value means more likeyer to see a monster per segment. 
	public int eventRate = 10;  // same as above. 

	public bool hasBoss = false; // is there a boss monster at the end? 

	public int min_level_advised = 1; // min hero level recomended.
	public int max_level_advised = 3;// no point visting above this level i guess. 

	//public float tick; 
	public float travleTimeCost = 1f; // time to get to dng. 
	public float timecost = 1f; // how much time each segment costs, before any events like fights. 
	//float LastUpdate = 0.0f;

	public int reward = 1; // need a system to set up reward, case based i guess? | need to know all types of rewards. none, resorces,gold, tech, winning.
	public int rewardAmount = 100; // amount to reward if needed. 



	// would like some way to add rewards for any segment, like 1/2 cleared or something. 
	public List<bool> segmentBonus;								
	public List<int> segmentBonusType; // watch out for off by one errors here. 
	public List<int> segmentBonusAmount; 
	public List<bool> segmentBonusPaid; // after geting one, set this to true for that segment. 
	// now just need a way to load this progrmaticly :D 


	public bool isActive = true;  // active means heros can go here and fight n stuff. 
	public bool isFlaged = false; // player set flag to guide where you want heros to go. 

	public List<Hero> herosOnQuest; // not really used, can use to hit all heros in a dng with an event. 
	public bool complete = false; 

	// need some way to set up quest chains, should do it useing unity UI.
	public bool last_in_chain = false;
	public Quest nextInchain; // set using UI, or some other XML script.


	//need some sort of reward, and a system to award it. 
	// quest system. 
	// need to have Level-> Reward. 
	// need to have entire quest system 


	// location in city for to walk out of, gate, car, plane, portal, w/e
	public GameObject cityGate; // use this to send hero to correct location. 




	// Use this for initialization
	void Start () {
		GameMaster.newday += updateMapProgress;

	}
	
	// Update is called once per frame
	void Update () {



		
	}






	public IEnumerator  dngRun (Hero theHero){
		Debug.Log ("walking to dng");

		yield return new WaitForSeconds (travleTimeCost);
		Debug.Log ("Start of a dng run");

		//int segment = 0;
			// go for each segemnt. 

		//	Debug.Log (theHero.heroName + " " + segment);

		for (int segment = 1; length + 1 > segment; segment++) {
			yield return new WaitForSeconds(timecost);

			int randomNumber = Random.Range (1, 100);  // roll to see if there is an encounter. 
			bool mapped = false;


			if (segment <= mapProgressStartOfDay) {
				mapped = true; // this this part of the dng mapped? 

				// how much should mapped help? no stam cost? better odds? 
			}
	

			if (randomNumber >= 100 - monsterRate) {
			
				//Debug.Log (randomNumber + " random monster!");
					 
			}
			else if (randomNumber<=eventRate){
				
				Debug.Log("random Event");
				eventMaster.dngEvent(theHero,dnglevel);
			}
			else{
				//Debug.Log ("no event");
			}

			if (mapped == false) {
				
				// try to map 
				// ok, what how deep are we. hmm should map at end of the run only. 
				//lose one stam
				theHero.stam = theHero.stam - stamCost;
			}




			if (segment == length) {
				// this was the last loop, run map function now for sure. 
			// or just run it every loop?

				Debug.Log ("cleared the dmg");
				if (hasBoss == false) {
					mapDng (segment);
					Debug.Log ("dng complete, give rewards for that and flag as completed.");
					if (complete == false) {
						questComplete ();
					}
					// give exp for a full run. // no gold. 

					theHero.gainExp (questMaster.expForQuest [dnglevel]);


					break; // leave the dng. 
				}
				else {
				// do boss fight stuff here. 

				}
			}

			if (theHero.stam <= 0) {
				// going home due to stam, try to map. 
				mapDng (segment);
				// give the hero some exp for part of a full run. // no gold. 
				break; // out of stam, go home. 
			}



			Debug.Log (segment + " has been compleat");

		}

	
		sendHeroHome (theHero);

		//	Debug.Log (tick);
		//	Debug.Log (LastUpdate);






		//


	} // main quest code for running the dngs 






	public void addHeroToQuest(Hero hero){


		herosOnQuest.Add (hero);
		// hero has been added, should i return any value? no idea.
		Debug.Log("a hero has started a quest " + dngName + " " + hero.heroName ); 
		// well i guess we can start now, 1st wait for travle time? 
		// then play with the hero. 
		StartCoroutine(dngRun(hero)); // ha, wow. stuck in time there. )

	}

	public void sendHeroHome(Hero hero){
		// this seems like a bad way to figure out how to remove the correct hero. | but ok. 
		// remove hero from list, 
		// send em home 

		hero.returnFromOutOfTown(); 
		herosOnQuest.Remove (hero); // may want a try catch error here. 

	}




	public void questComplete(){

		// give reward
		giveReward(reward,rewardAmount);

		// open next in chain

		if (last_in_chain == false) {
			nextInchain.isActive = true; 
			Debug.Log ("update quest UI to show new quest option"); // wonder if you should be able to start on other quests before you compleat one in the chain. 
		}

		// set as compleat 
		complete = true;
		SetFlagInactive ();
	}


	// the brakets really got away from me here. 
	public void mapDng(int howFarIn){
	
		for (int i = 1; howFarIn+1 > i; i++) {
	
			if (i > mapProgress) {


				// roll a random num.
				int mapRoll =	Random.Range (1, 100);
				Debug.Log ("map roll: " + mapRoll + " " + howFarIn);
				if (mapRoll > 100 - mapChance) {
		
					// map roll pass, add 1 to the map. but put the map above how far in you got.
					if (mapProgress < howFarIn) {
						mapProgress = mapProgress + 1;
												}


												}	

									}
										}		
								}

	public void updateMapProgress(){
	
		Debug.Log ("Map Progress updated");
		mapProgressStartOfDay = mapProgress;

	}




	public void giveReward(int Reward, int RewardAmount){
	
		switch (Reward) {
		case 0:
			{Debug.Log ("no reward");
				break;}
		case 1:
			{break;}
		case 2:
			{break;}
		case 3:
			{break;}
		case 4:
			{break;}
		default:
			{
				Debug.Log ("invaild award ammount");
				break;}


		}

		Debug.Log ("reward paid");// flag this somewhere so no more rewards get paid for this segment. 

	
	}


	public bool IsQuestFlaged(){
	
		return isFlaged;

	}


	// UI hooks below. 

	public void SetFlag(){
	
		if (isFlaged) {
		
			isFlaged = false;

		} else {
		
			isFlaged = true;
		}
	
	}


	public void SetFlagActive(){
	
		isFlaged = true;


	}

	public void SetFlagInactive(){

		isFlaged = false;

	}





	public bool isQuestActive(){

		return isActive; 

	}

	public string curentMapedPercent(){

		double map = mapProgressStartOfDay;
		double mapLength = length;
		double percent = ((map / mapLength) *100);

		//percent = decimal.Round (percent,2 );

		Debug.Log ("map% =" + percent + " dng:" + dngName + " " + mapProgressStartOfDay + " " + length);
		 
		return percent.ToString ();

	}

}
