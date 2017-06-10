using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUIMain : MonoBehaviour {

	public List<QuestUiPanel> QuestUiPanals;  // all quests for this map here. 
	public GameObject QuestUI; // main quest UI 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyUp (KeyCode.Q)) {
		
		
			// show hide quest UI
			ShowHideQuestUI();

		}

	}



	public void ShowHideQuestUI(){
	
	
		if (QuestUI.activeInHierarchy) {
		
			QuestUI.SetActive (false);

		} else {

			// set ui active, show and update all active quest panals

			// update quest panal UI
			UpdateQuestInfo_UI();

			//set UI active
			QuestUI.SetActive (true);


		}

	}




	public void UpdateQuestInfo_UI(){
	
		//foreach(GameObject building in buildings.built_Buildings){
		foreach(QuestUiPanel updateUI in QuestUiPanals){

			updateUI.refreshInfo ();

		}


	}



	public void ShowQuestUI(){

		QuestUI.SetActive (true);


	}

	public void HidquestUI(){

		QuestUI.SetActive (false);


	}

}
