using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour {

	public List<GameObject> buildings;  // buildings you can build.
	public List<GameObject> ghostBuildings; // their ghost art, should look simi transparent or something.

	public GameObject parent; // this is used to set objects parent.
	public GameObject UIpanal; // show hide build UI

	private bool ShowPanel; 

	private bool canBuild;
	private bool ghost; 
	int whatToBuild;
	private GameObject somebuilding;
	GameMaster gameMaster; 


	public List<GameObject> built_Buildings; // any paid for and built gameobject. 

	// Lots Of UI below, TO DO: move most of this ui Code to its own UI class. 
	// building UI
	public bool showBuildingInfo_UI = false; 
	public GameObject UI_panal_Built; // building info when clicked on.
	public Text buildingName_UI_TEXT; 


	// Hero UI

	public bool showHeroQuick_UI = false;
	public GameObject UI_Panal_Hero;
	public Text heroName_UI_Text;
	public Text heroLevel_UI_Text;


	// Use this for initialization
	void Start () {

		ghost = true;
		ShowPanel = false;
		UIpanal.SetActive (ShowPanel);
		gameMaster = this.gameObject.GetComponent<GameMaster> ();

		// load any pre built buildings into built_Buildings here.


	}


	void Update(){

		if (Input.GetKeyUp (KeyCode.B)) {
			Debug.Log ("build");
			ShowPanel = !ShowPanel;
		//	Debug.Log (ShowPanel);
			UIpanal.SetActive (ShowPanel);
			UIpanal.transform.position = Input.mousePosition;
								}

		preBuild ();


		if (Input.GetMouseButtonDown(0)){
		clickOnBuilding ();
		}



	}


	public void queUpBuild(int someint ){
	 	// int is passed on button press, int is the buillding i want to build. 
	    
		ShowPanel = !ShowPanel;
		UIpanal.SetActive (ShowPanel);
		whatToBuild = someint;


		canBuild = true;
		ghost =	false;



	}


	void preBuild(){

		if (ghost == false) {
			Debug.Log("what?");
			ghost = true;
			somebuilding	  = 	Instantiate (ghostBuildings [whatToBuild]);

		
		
		}


		if (canBuild == true) {
			
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 100)) {
			//	Debug.DrawLine (ray.origin, hit.point);
			//	Debug.Log ("build ray hit: " + hit.point);
			//	Debug.Log ("build ray hit: " + hit.transform.tag);
				somebuilding.transform.position = hit.point;

				if (Input.GetMouseButtonDown (0)) {
					canBuild = false;
					Destroy (somebuilding);
					buildBuilding (buildings [whatToBuild]);

					// now we need to update info, there is a new building. just get its gameobject.
													}

												}

							}

	

	}


	void buildBuilding(GameObject build){

		// need to add alot more building logic. 



			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);  // build right where i click.
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 100) ) {
				//Debug.DrawLine (ray.origin, hit.point);
				//Debug.Log ("build ray hit: " + hit.point);
				//Debug.Log ("build ray hit: " + hit.transform.tag);
				if (hit.transform.tag == "Floor") {

				GameObject obj = 		Instantiate (build, hit.point, build.transform.rotation, parent.transform);
				Debug.Log ("built something");
				built_Buildings.Add (obj);
				// obj is spawned and is added to list of buildings, its on its own now.  

			}

		}

	


	





	}	

	void clickOnBuilding(){

		Debug.Log ("called");

		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 100)) {
			if (hit.collider != null) {
				GameObject someHit = hit.collider.gameObject;
				Debug.Log ("hit something " );

				if(someHit.CompareTag("Building")){

					Debug.Log("cliked on a building");
					CloseAll_UI ();
					ShowBuildingInfoUI (someHit.GetComponent<Building>());


				}
				else if (someHit.CompareTag("Hero")){
					CloseAll_UI();
					Debug.Log("cliked on a hero");
					ShowHeroInfoUI(	someHit.gameObject.GetComponentInParent<Hero> ());


				}
				else{
					CloseAll_UI(); // should just close all. 
				}

			}

		}
	}



	public void ShowBuildingInfoUI(Building buildInfo ){
		showBuildingInfo_UI = true;
		//UI_panal_Built.transform.position = Input.mousePosition;
		UI_panal_Built.SetActive(true); // building info when clicked on.


		buildingName_UI_TEXT.text = buildInfo.buildingName; 




	}

	public void CloseBuildingInfoUI(){
	
		showBuildingInfo_UI = false;
		UI_panal_Built.SetActive(false); 

	}


	public void ShowHeroInfoUI(Hero heroInfo){
	
	 showHeroQuick_UI = true;
		UI_Panal_Hero.SetActive(true);
	
		heroName_UI_Text.text = heroInfo.heroName;

		heroLevel_UI_Text.text = ("Level "+heroInfo.heroLevel.ToString());



	}


	public void CloseHeroInfoUI(){
		showHeroQuick_UI = false;
		UI_Panal_Hero.SetActive(false);


	}

	public void CloseAll_UI(){
		CloseHeroInfoUI ();
		CloseBuildingInfoUI ();
	}

}
