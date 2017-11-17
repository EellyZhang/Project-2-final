using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;

public class CubeBehavior : MonoBehaviour {
	public int x, y;
	public bool isPlane;

	// Use this for initialization
	void Start () {
		GameController.activatePlane = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}


		
	void OnMouseDown() {
		//print("isPlane: "+isPlane);

		if (GameController.activatePlane) {
			if (isPlane)
			{
				GameController.PlaneDeactivation();
			}

			else {
				GameController.targetX = x;
				GameController.targetY = y;
				GameController.MoveAirplane();
			}
		}
		else if (isPlane) {
			GameController.PlaneActivation ();
			print("x: " + x + "....y: " + y);
		}

	}
}