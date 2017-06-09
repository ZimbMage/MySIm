using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCamra : MonoBehaviour {


	float ScrollSpeed  = 15f;
	float  ScrollEdge  = 0.001f;

	// to do : clamp camara movement
	private int HorizontalScroll  = 1;
	private int VerticalScroll  = 1;
	private int DiagonalScroll  = 1;

	float PanSpeed  = 10;
	float camraSpeed = .01f;

	Vector2 ZoomRange  = new Vector2(-8,16);
    float CurrentZoom  = 0;
	float ZoomZpeed  = 1;
	float ZoomRotation  = 1; // why? 

	private Vector3  InitPos ;
	private Vector3  InitRotation ;



	void Start()
	{
		//Instantiate(Arrow, Vector3.zero, Quaternion.identity);

		InitPos = transform.position;
		InitRotation = transform.eulerAngles;

	}

	void Update ()
	{
		//PAN
		if ( Input.GetKey("mouse 2") )
		{
			//(Input.mousePosition.x - Screen.width * 0.5)/(Screen.width * 0.5)

			transform.Translate(Vector3.right * camraSpeed * PanSpeed * (Input.mousePosition.x - Screen.width * 0.5f)/(Screen.width * 0.5f), Space.World);
			transform.Translate(Vector3.forward * camraSpeed * PanSpeed * (Input.mousePosition.y - Screen.height * 0.5f)/(Screen.height * 0.5f), Space.World);

		}
		else
		{
			if ( Input.GetKey("d") ) // || Input.mousePosition.x >= Screen.width * (1 - ScrollEdge) )
			{
				transform.Translate(Vector3.right * camraSpeed * ScrollSpeed, Space.World);
			}
			else if ( Input.GetKey("a") )// || Input.mousePosition.x <= Screen.width * ScrollEdge )
			{
				transform.Translate(Vector3.right * camraSpeed * -ScrollSpeed, Space.World);
			}

			if ( Input.GetKey("w") ) //|| Input.mousePosition.y >= Screen.height * (1 - ScrollEdge) )
			{
				transform.Translate(Vector3.forward * camraSpeed * ScrollSpeed, Space.World);
			}
			else if ( Input.GetKey("s"))// || Input.mousePosition.y <= Screen.height * ScrollEdge )
			{
				transform.Translate(Vector3.forward * camraSpeed * -ScrollSpeed, Space.World);
			}
		}

		//ZOOM IN/OUT

		CurrentZoom -= Input.GetAxis("Mouse ScrollWheel") * camraSpeed * 1000 * ZoomZpeed;

		CurrentZoom = Mathf.Clamp(CurrentZoom,ZoomRange.x,ZoomRange.y);


		Vector3 newpos = transform.position;
		newpos.y -= (transform.position.y - (InitPos.y + CurrentZoom)) * 0.1f; // why does this work while 'transform.position.x += 5.0f;' doesn't?
		transform.position = newpos;


		//transform.position.y -= (transform.position.y - (InitPos.y + CurrentZoom)) * 0.1f;

	//	Vector3 newpos2 = transform.eulerAngles;
	//	newpos2.x -= (transform.eulerAngles.x  - (InitRotation.x + CurrentZoom * ZoomRotation)) * 0.1f;
		//sets the rotaion, do i need any of this?
	//	transform.eulerAngles = newpos2;

		//transform.eulerAngles.x -= (transform.eulerAngles.x - (InitRotation.x + CurrentZoom * ZoomRotation)) * 0.1f;
		// Vector3 v = transform.eulerAngles;
		//v.y += Input.GetAxis("Horizontal") * 5;
		//transform.eulerAngles = v;
	
	}
}
