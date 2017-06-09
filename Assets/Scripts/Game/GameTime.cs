using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTime : MonoBehaviour {

	public bool pauseGame = false; 
	public float curentTime; 
	private float tick; 
	private float LastUpdate;
	float hourUnit = 1.3f;
	GameMaster gamemaster;
	public Text time; 

	// event values below
	public int setHeroStuffTime = 9; // must be less then 24. 


	void Start () {
		// set clock 
		gamemaster = this.gameObject.GetComponent<GameMaster>();
	//	curentTime = 8.0f ;
		LastUpdate = 0.0f;
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButtonUp ("Pause")) {
		
			PauseGame ();
		}

		//Debug.Log (LastUpdate);
		tick = Time.time;

		// every 2.5 seconds, advance the Curent time 1 hr
		if (tick - LastUpdate >= hourUnit){
			//Debug.Log ("do stuff");
			curentTime = curentTime + 1;
			// useing a 24 hr clock for now. 

			// can set up any events at any time i want. 

			if (curentTime == setHeroStuffTime) {
			
				// need to fire a delgate? or reall just call heroManager. eh go tru game manger so it can be more flexable. 
				gamemaster.dailySetStuffTime();


			}

			// could any be missed? no idea. 
			if (curentTime >= 24) {
				curentTime = 0;
				gamemaster.advanceDay ();  }
			LastUpdate = tick;


									}

	//	if (Time.time >= 60f)
	//		Debug.Log ("its been 1 min");
		time.text = ("Time:"+curentTime.ToString()); 
	}


	public void PauseGame(){
		{
			Debug.Log ("P pressed");

			if (pauseGame == false) {
				Debug.Log ("pause");
				Time.timeScale = 0;
				pauseGame = !pauseGame;
			}
			else {
				Debug.Log("un pause");
				Time.timeScale = 1; 
				pauseGame = !pauseGame;
			}


		}




			
		}

	public void setPauseTrue(){

		Time.timeScale = 0;
		pauseGame = true;


	}

	public void setPauseFalse(){
	
		Time.timeScale = 1;
		pauseGame = false;

	}



}
