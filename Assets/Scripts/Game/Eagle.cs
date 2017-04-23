using System.Collections;
using UnityEngine;

public class Eagle : PlanetMovement
{
	public float speedRandomness;
	public float huntingSpeed;
	public float descentSpeed;
	public float ascentSpeed;

	PlayerController playerController;

	bool moveLeft;
	bool hunting;
	bool returning;

	PlanetPosition planetPosition;

	float originalDistance;
	float originalSpeed;


	#region Unity Lifecycle

	protected override void Start()
	{
		base.Start();
		StartCoroutine(ChangeDirection());

		planetPosition = GetComponent<PlanetPosition>();
		angle = planetPosition.angle;

		movementSpeed = movementSpeed * (1 + Random.Range(-speedRandomness, speedRandomness));

		originalDistance = planetPosition.distance;
		originalSpeed = movementSpeed;
	}

	void Update()
	{
		if (hunting) {
			if (playerController.Sheltered) {
				hunting = false;
				returning = true;
			} else {
				Debug.DrawRay(transform.position, playerController.transform.position - transform.position, Color.red);

				var vectorA = planet.position - transform.position;
				var vectorB = planet.position - playerController.transform.position;
				var a = Vector3.Angle(vectorA, vectorB);

				if (a > 1) {
					var cross = Vector3.Cross(vectorA, vectorB);
					if (cross.z > 0) {
						MoveLeft();
					} else {
						MoveRight();
					}
				}

				MoveDown();

				if (Vector3.Distance(transform.position, playerController.transform.position) < 0.5f) {
					GameMaster.instance.Die();
				}
			}
		} else if (returning) {
			MoveUp();
		} else {
			if (moveLeft) {
				MoveLeft();
			} else {
				MoveRight();
			}
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (!hunting && other.gameObject.tag == "Player") {
			var player = other.gameObject.GetComponent<PlayerController>();

			if (!player.Sheltered) {
				StartHunt(player);
			}
		}
	}

	#endregion

	void MoveDown()
	{
		planetPosition.angle = angle;
		planetPosition.distance = Mathf.Max(8.2f, planetPosition.distance - descentSpeed * Time.deltaTime);
		planetPosition.RecalculatePosition();
	}

	void MoveUp()
	{
		planetPosition.distance = Mathf.Min(originalDistance, planetPosition.distance + ascentSpeed * Time.deltaTime);
		planetPosition.RecalculatePosition();

		if (Mathf.Approximately(planetPosition.distance, originalDistance)) {
			returning = false;
			movementSpeed = originalSpeed;
		}
	}

	void StartHunt(PlayerController player)
	{
		playerController = player;
		movementSpeed = huntingSpeed;
		hunting = true;
	}

	IEnumerator ChangeDirection()
	{
		for (;;) {
			yield return new WaitForSeconds(Random.Range(5, 10));
			moveLeft = Random.value < 0.5f;
		}
	}
}
