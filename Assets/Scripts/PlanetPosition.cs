using UnityEngine;

public class PlanetPosition : MonoBehaviour
{
	public float distance;
	public float angle;

	void OnValidate()
	{
		angle = Mathf.Clamp(angle, -360, 360);
		var planetPosition = GameObject.Find("Planet").transform.position;
		transform.position = planetPosition + new Vector3(0, distance, 0);
		transform.rotation = Quaternion.identity;
		transform.RotateAround(planetPosition, Vector3.forward, angle);
	}
}
