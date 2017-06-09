using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Building : MonoBehaviour {

	public int age = 0; // age in days. update every day at dawn. 
	public bool hero_can_vist= false; // not a building heros care about.
	public int objid = -99; 
	GameMaster gameMaster;
	public bool preBuilt = false; 
	public string buildingName = "someBuilding";

	// Use this for initialization
	void Start () {
		if (gameMaster == null){
			GameObject Master;
			Master = GameObject.FindGameObjectsWithTag("GameMaster")[0];
			gameMaster = Master.GetComponent<GameMaster> ();
			}
		// i'm a building. 
		Debug.Log("building spawned");
		this.gameObject.tag = "Building";

		if (preBuilt == false) {

			objid = gameMaster.getobjid ();
		}

		// what kind of building am? do i know here? do i care? 

		// do any delegate stuff here. 


		//delegateNewDay day = Master.GetComponent<delegateNewDay> ();
		GameMaster.newday += advanceDay;
	//gameMaster.
		//	public static event delegateNewDay newday;
	}
	
	// Update is called once per frame
	//void Update () {	}

	// Use this for when a building is sold or distroyed. 
	void OnDestroy() {
		print("Script was destroyed");
		GameMaster.newday -= advanceDay;
	}



	public void built (GameMaster master){

		gameMaster = master;
		objid = gameMaster.getobjid ();


	}



	public void advanceDay(){
		age++;
		Debug.Log ("building is now a day older");
		// do stuff baised of this? idk i think i may need to write more scripts below house. house really should of been called 'Building'

	}


}
