using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {

	/// lets keep track of lots of stuff here. 
	/// what day is it
	/// 
	public int gold = 100;   // for now no get/set. ui updates will have to be done difrently.should this even be here? // eh should be fine. ill write a delgate or something
	public int build_mat = 100; // used for building, only heros/mines contribuit it for now. 
	public int population = 0; // this will go up when game starts as per the senairo. re
	public int heroCount = 0; 
	public int day = 1; // should day be in game time?
	public int objid =0; // not sure ill need this. 

	public Text curentDay; 
	public Text resource; 

	GameTime gameClock; 
	BuildManager buildManager; 


	public delegate void delegateNewDay();
	//public delegateNewDay  newday;
	public static event delegateNewDay newday;
	public static event delegateNewDay playerEvent;  // can i do this| should i do this? 

	// any game objects here



	// Use this for initialization
	void Start () {
		gameClock = this.gameObject.GetComponent<GameTime> ();
		buildManager = this.gameObject.GetComponent<BuildManager> ();

	//	objid = 0;
		newday += update_UI_Day ;
		newday += update_UI_resource;
		update_UI_resource ();
	}



	public void advanceDay(){
	
		endOfDay();

		day = day + 1;

		// check for end of game?

		// do end of day stuff (own class?)

		startOfDay ();

		// then do start of day stuff ( again own class? also do i want to do end of day at mid night then new day at dawn?)
	
							}


	 void endOfDay(){
	

	 // not much, check for game end really if its last day. idk ill think of stuff. 


	}

	void startOfDay(){
		day++;
		if (newday != null) {
		// any script that wants to do something at start of day can subscibe to this event. 
			newday ();
		
		}
		Debug.Log("new day!");// all buildings care about this, how to let them all know?
		// get paid
		// try to spawn a hero


	}

	public void dailySetStuffTime(){ // crappy name..... // set heros on quests, and daily tasks at this time. do other player controled stuff at this time. 

		if (playerEvent != null) {
			playerEvent ();
		}

	}



	public int getobjid (){
		//int objid_toSend = objid;
		//objid = objid; 
		//	objid++;
		//return objid_toSend;

		//or?
		objid++;
		return objid - 1;
	
	}


	// UI update functions below. 

	public void update_UI_Day(){

		curentDay.text = ("Day:" + day.ToString ());

	}

	public void update_UI_resource(){

		resource.text = ("Gold:"+gold.ToString()+" | Ore:" + build_mat.ToString()+" | Population:" + population.ToString());
	
	}

	// objid
	public void setOBJID(int ID){

		objid = ID;
		// for loading a save file. 
	}

	// gain or lose resorcers below



	public void gainGold(int gain){
		gold = gold + gain;

	}

	public void spendGold(int spent){
	
		gold = gold - spent;

		if (gold < 0) {
		
			Debug.LogError("gold spent should not go negitive, did you mean to use lose gold?");
						}

							}

	public void loseGold(int loss){
	
		if (gold < loss) {
			gold = 0;
		} else
			gold = gold - loss;
	}


	public void gainBuild_mat(int gain){
		build_mat = build_mat + gain;
	

	}



	public void spendbuild_mat(int spent){

		build_mat = build_mat - spent;

		if (build_mat < 0) {

			Debug.LogError("build_mat spent should not go negitive, did you mean to use lose gold?");
		}

	}

	public void losebuild_mat(int loss){

		if (build_mat < loss) {
			build_mat = 0;
		} else
			build_mat = build_mat - loss;
	}




}
