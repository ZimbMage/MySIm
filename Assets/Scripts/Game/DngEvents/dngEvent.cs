using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dngEvent : MonoBehaviour {


	public string	eventName =  "someEvent";
	public int eventNumber = 1; // what event is this. need this ID to hard code this system to work. 
	public int startLevel = 1;  // what level does this event start at, aka a level 5 event wont happen in dngs levels below 5. a lvl 1 event could always happen.
	public int endLevel = 10; // what level does this event stop showing up at. 

	public float healPercent = 50; // how much % to heal the hero for if a heal event. 



	// Use this for initialization
	void Start () {
		
	}


	public void doTheEvent(Hero thehero){


		// am i really about to write a case inside this function, could call it... naw

		if (eventNumber == 1) {
		
			// hero gets 50% hp back. 
			thehero.HealMe(healPercent);

		
		}

		if (eventNumber == 2) {
		
			// refill stam 
			thehero.regainStam();

		}

	
	}
	




}
