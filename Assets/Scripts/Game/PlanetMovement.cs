using UnityEngine;

public class PlanetMovement : MonoBehaviour
{
	public float movementSpeed;
	public float angle;

	protected Transform planet;

	protected virtual void Start()
	{
		planet = GameObject.Find("Planet").transform;
	}

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
