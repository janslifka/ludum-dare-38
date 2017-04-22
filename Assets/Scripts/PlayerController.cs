using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PlanetMovement
{
	void Update()
	{
		if (Input.GetAxis("Horizontal") > 0) {
			MoveRight();
		} else if (Input.GetAxis("Horizontal") < 0) {
			MoveLeft();
		}
	}
}
