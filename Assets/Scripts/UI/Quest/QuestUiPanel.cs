﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUiPanel : MonoBehaviour {


	public Quest quest; // quest script. 

	public bool isQuestActive; // if the quest is not active do not show up. 

	public Text questName; 
	public Text questLevel; 
	public Text mapPrecent;
	public Text questRewardType;
	public Text isCompleat;  // is this quest compleat. 

	//public GameObject Flag_button_UI;
	public Button Flag_Button_UI;
	public ColorBlock startColor; 


	// Use this for initialization
	void Start () {

		startColor = Flag_Button_UI.colors;
	//	ButtonColor ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}



	public void refreshInfo(){
	
		if (quest.isQuestActive ()) {

			this.gameObject.SetActive (true);

			mapPrecent.text = ("Map%:" +quest.curentMapedPercent());
			// calls below are somewhat un safe if quest script changes too much.
			questName.text = quest.dngName;
			questLevel.text = ("level: "+quest.dnglevel.ToString ());

			questRewardType.text = "REWARD NYI";
			isCompleat.text = ("complete?:"+ quest.complete.ToString());

			ButtonColor ();
		}
		else {
		
			this.gameObject.SetActive (false);

		}
		Debug.Log (Flag_Button_UI.colors.normalColor); 

	}

	//why is this so hard?
	public void ButtonColor(){
		ColorBlock block = Flag_Button_UI.colors;
		Color col = Color.black;
		if (quest.isFlaged) {
			

		//	block.normalColor = Flag_Button_UI.colors.pressedColor;
			block.normalColor = Color.green;
			Flag_Button_UI.colors	 = block;	
			Debug.Log (Flag_Button_UI.colors.normalColor); 
			//	Flag_Button_UI. = Co;
		
		} 

		else {
			Flag_Button_UI.colors = startColor;	
		}

				//block.normalColor = new  Color (0f, 0f, 0f);
		// set every thing back to normal. 
	


	}

	//Debug.Log (Flag_Button_UI.colors.normalColor); 
}
