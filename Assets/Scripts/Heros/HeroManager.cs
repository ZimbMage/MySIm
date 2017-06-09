using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroManager : MonoBehaviour {


	QuestMaster quests; // need to know what quests are on the map. 
	GameMaster master; 
	BuildManager buildings; 

	public List<Hero> Heros; // list of all heros. 
	public List<GameObject> locationsToVist; // all locations i want a hero to walk to today. 
	public List<GameObject> locationsToVist_withQuest; // same list as above but with a quest location as last entry. 

	// heros need to vist all stores then go on daily quest, if they are active. 

	// Use this for initialization
	void Start () {
		quests	= this.gameObject.GetComponent<QuestMaster> ();
		master = this.gameObject.GetComponent<GameMaster> ();
		buildings = this.gameObject.GetComponent<BuildManager> ();
		GameMaster.playerEvent += setQuestsForActiveHeros;

	}
	
	// Update is called once per frame
	void Update () {
		// i dont think i need any update calls. 
	}




	public void add_a_hero(Hero hero){
	
		Heros.Add (hero); // add new heros to list. 


	
	}

	// once a day send out heros to go do their quest. 


	void setQuestsForActiveHeros(){
		locationsToVist.Clear();
		// loop through all buildings. // only need to do this once. 
		foreach(GameObject building in buildings.built_Buildings){
			Building built = building.GetComponent<Building> ();
			if (built.hero_can_vist) {
				locationsToVist.Add (building);  // this builds the buildings in town to vist every day. 
			}
		}


		Debug.Log ("test quest hero set");
		// (string value in pets)
		foreach (Hero hero in Heros){ // look at each hero, ask questMaster script if it has a quest i should send this guy on. 
			locationsToVist_withQuest.Clear ();
			locationsToVist_withQuest.AddRange (locationsToVist);

			Debug.Log (hero.heroName);
			Quest quest =  	quests.questForHero (hero);
			if (quest != null) {
				locationsToVist_withQuest.Add (quest.cityGate);
				hero.PlacesToGoToday (locationsToVist_withQuest, true); // right... only passed the gate. 
				hero.quest = quest;
			} 
			else {
				hero.PlacesToGoToday (locationsToVist, false);
			}
		
		}
		locationsToVist.Clear ();
		locationsToVist_withQuest.Clear ();
	}


	// Send the hero, what quest to go do, along with what building to vist. 

}
