using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameController : MonoBehaviour {
	public GameObject cubePrefab;
	Vector3 cubePosition;
	public static GameObject selectedCube;
	static int airplaneX, airplaneY, originX = 0, originY = 8;
	public static int depotX = 15, depotY = 0;
	public static GameObject[,] allCube;
	float eachTurnTime, turnTimer;
	public static int airplaneCargo, airplaneCargoMax;
	static int cargoPerGain;
	public static int score;
	public static int moveX, moveY;
	public static bool activatePlane;
	public static int targetX, targetY;

	// Use this for initialization
	void Start () {
		eachTurnTime = 1.5f;
		turnTimer = eachTurnTime;

		activatePlane = false;

		airplaneCargo = 0;
		airplaneCargoMax = 90;
		cargoPerGain = 10;

		score = 0;

		moveX = 0;
		moveY = 0;

		allCube = new GameObject[16,9];

		//starting position for airplane

		airplaneX = originX;
		airplaneY = originY;

		targetX = airplaneX;
		targetY = airplaneY;

		for (int x = 0; x < 16; x++) {
			for (int y = 0; y < 9; y++) {
				cubePosition = new Vector3 (x*2, y*2, 0);
				allCube[x,y] = Instantiate(cubePrefab, cubePosition, Quaternion.identity);

				allCube[x,y].GetComponent<CubeBehavior>().x = x;
				allCube[x,y].GetComponent<CubeBehavior>().y = y;
				allCube[x,y].GetComponent<CubeBehavior>().isPlane = false;

				if (x == originX && y == originY) {
					allCube[x,y].GetComponent<Renderer>().material.color = Color.red;
					allCube[x,y].GetComponent<CubeBehavior>().isPlane = true;
				}

				if (x == depotX && y == depotY) { 
					allCube[x,y].GetComponent<Renderer>().material.color = Color.black;
					allCube[x, y].GetComponent<CubeBehavior>().isPlane = false;
				}
			}
		}
	}



	public static void PlaneActivation(){
		allCube[airplaneX,airplaneY].GetComponent<Renderer>().material.color = Color.green;
		selectedCube = allCube[airplaneX,airplaneY];
		selectedCube.GetComponent<CubeBehavior>().isPlane = true;
		activatePlane = true;
	}

	public static void PlaneDeactivation() { 
		allCube[airplaneX,airplaneY].GetComponent<Renderer>().material.color = Color.red;
		activatePlane = false;
	}



	void LoadCargo()
	{
		print ("AX:" + airplaneX + "AY:" + airplaneY);
		if (airplaneX == originX && airplaneY == originY)
		{
			airplaneCargo += cargoPerGain;

			if (airplaneCargo > airplaneCargoMax)
			{
				airplaneCargo = airplaneCargoMax;
			}
		}
	}

	void DeliverCargo() {
		if (airplaneX == depotX && airplaneY == depotY) {
			score += airplaneCargo;
			airplaneCargo = 0;
		}
	}

	static void CalculateMovement() {
		if (targetY > airplaneY)
		{
			moveY = 1;
		}

		else if (targetY < airplaneY)
		{
			moveY = -1;
		}

		else {
			moveY = 0;
		}

		if (targetX < airplaneX)
		{
			moveX = -1;
		}

		else if (targetX > airplaneX)
		{
			moveX = 1;
		}

		else {
			moveX = 0;
		}
	}

	public static void MoveAirplane() {

		CalculateMovement ();


		if (activatePlane) {
			allCube[airplaneX,airplaneY].GetComponent<Renderer>().material.color = Color.white;
			allCube[airplaneX,airplaneY].GetComponent<CubeBehavior>().isPlane = false;
			if (allCube[airplaneX,airplaneY].GetComponent<CubeBehavior>().x == depotX && allCube[airplaneX,airplaneY].GetComponent<CubeBehavior>().y == depotY) { 
			allCube[airplaneX,airplaneY].GetComponent<Renderer>().material.color = Color.black;
		}

			airplaneX = selectedCube.GetComponent<CubeBehavior>().x;
			airplaneY = selectedCube.GetComponent<CubeBehavior>().y;

			airplaneX += moveX;
			airplaneY += moveY;

			if (airplaneX >= 15) {
				airplaneX = 15;
			}
			else if (airplaneX < 0) {
				airplaneX = 0;
			}
		
			if (airplaneY >= 8) {
				airplaneY = 8;
			}
			else if (airplaneY < 0) {
				airplaneY = 0;
			}
			PlaneActivation();
		}
	}


	// Update is called once per frame
	void Update () {

		CalculateMovement ();

		if (Time.time > turnTimer) {
			MoveAirplane();

			LoadCargo();
			DeliverCargo();

			//debug
			print("Cargo: " + airplaneCargo + "Score: " + score);

			turnTimer += eachTurnTime;
			}
		}
	}





