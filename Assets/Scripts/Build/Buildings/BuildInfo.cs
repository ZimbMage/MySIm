using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;


public class BuildInfo : MonoBehaviour {

	// this script will hold all baisc info for buildable buildings. 
	// 			<building cost="100" id="1" know="True" limit="10" name="house" />
	//			<building cost="25" id="2" know="False" name="farm" />
	// ect

	// Use this for initialization
	void Start () {


		// load some XML


	}


	public int buildingCost(int id_toBuild){
	
	//take ID and find what building is trying to be built, then return the cost. 


		// if you fail to find what to return, return -99. 
		return -99;

	}


}
