using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMaster : MonoBehaviour {

	/// <summary>
	///  going to try with just two events to start, HP shrine/ Stamp Founin
	/// </summary>


	public List<dngEvent> events;
	public List<dngEvent> pick_From_Me ; 

	public dngEvent defultEvent; 




	// Use this for initialization
	void Start () {
		
	}




	public void dngEvent(Hero thehero, int dngLeveL)
	{
		Debug.Log ("start of event pick");
		
		if (pick_From_Me != null) {
			pick_From_Me.Clear ();
		}

		//foreach (Quest quest in quests)
		Debug.Log("more event stuff");
		foreach (dngEvent theEvent in events) {
			if (theEvent.startLevel >= dngLeveL && theEvent.endLevel <= dngLeveL) {
				pick_From_Me.Add (theEvent);
			}
		}
			// loop over 
			Debug.Log("about to do some event");
			if (pick_From_Me.Count > 0) {
			
				int someRandomNumber = Random.Range (0, pick_From_Me.Count); // this is correct right?

				pick_From_Me [someRandomNumber].doTheEvent (thehero); // watch for index errors. 

			} else {
				Debug.Log ("had to use the defult event.");
				// need to do a defult event. 
				defultEvent.doTheEvent(thehero);
			}



		} 







}
