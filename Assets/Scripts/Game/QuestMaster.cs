using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestMaster : MonoBehaviour {
	
	// this script needs to hold all the quests created and stuff. also needs to work with the Quest UI. 

	public List<Quest> quests; // this is all loaded in unity UI for now. 
	public List<Quest> applicableQuests; 



	// exp reward list | or replace this with a math fomural

	public List<int> expForQuest;


	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
	}




	// 
	public Quest questForHero(Hero someHero){
		applicableQuests.Clear();
		// try to figure out what quest to send this hero on.

		// 1st, are there any flags for this hero level/type

		foreach (Quest quest in quests) {
		
			if (quest.isFlaged) {
			
				applicableQuests.Add (quest);
			
			}

			// loop over
			if(applicableQuests.Count>0){

				return randomQuestPick ();


			}

		}


		//NYI // will add this once i add a UI 


		// next are there any quests of the recomend level range?

		// lets build a list of all quests the hero falls into range of, then pick one at random. 
		foreach (Quest quest in quests){
			//Debug.Log (quest.dngName);
			//Debug.Log(quest.min_level_advised);
			if( someHero.heroLevel >= quest.min_level_advised && someHero.heroLevel <= quest.max_level_advised && quest.isActive == true) {
				
				Debug.Log("think hero could go on this quest " + quest.dngName ); 
				applicableQuests.Add (quest);
			
			}
		
				

		}

		if (applicableQuests.Count != 0) {
			
			Debug.Log (applicableQuests);
		//	int somevalue = Random.Range (0, (applicableQuests.Count  )); // seems bad. 
		//	Debug.Log ("randonly picked:" + somevalue);
			return randomQuestPick(); // need to return one at random| curent code is error prone, need a try catch or something.

		}
		// last, is there a defult i should set it to?

		// finaly pass. 
		return null; 
	


	}


	Quest randomQuestPick(){
	
	
		int somevalue = Random.Range (0, (applicableQuests.Count  )); // seems bad. 
		Debug.Log ("randonly picked:" + somevalue);
		return applicableQuests [somevalue]; // need to return one at random| curent code is error prone, need a try catch or something.


	}

}
