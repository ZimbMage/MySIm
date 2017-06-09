using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastBuild : MonoBehaviour {

	public GameObject house;
	public GameObject parent; 


	// Use this for initialization
	void Start () {
		Debug.Log ("build ray start");
	}
	
	// Update is called once per frame
	void Update () {

		// need to add alot more building logic. 
		// need to really write a building class. 

		if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 100) ) {
				Debug.DrawLine (ray.origin, hit.point);
				Debug.Log ("build ray hit: " + hit.point);
				Debug.Log ("build ray hit: " + hit.transform.tag);
				if (hit.transform.tag == "Floor") {
					
					Instantiate (house, hit.point, parent.transform.rotation, parent.transform);
													}
														}
			//public static Object Instantiate(Object original, Vector3 position, Quaternion rotation, Transform parent);
		}
	}
}

