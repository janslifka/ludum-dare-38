﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetMovement : MonoBehaviour
{
	public Transform planet;
	public float movementSpeed;

	public float angle;

	protected void MoveRight()
	{
		transform.RotateAround(planet.position, Vector3.forward, -movementSpeed * Time.deltaTime);
		angle -= movementSpeed * Time.deltaTime;
	}

	protected void MoveLeft()
	{
		transform.RotateAround(planet.position, Vector3.forward, movementSpeed * Time.deltaTime);
		angle += movementSpeed * Time.deltaTime;
	}
}
