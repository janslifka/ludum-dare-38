using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : PlanetMovement
{
	public float huntSpeed;

	bool moveLeft;


	PlayerController playerController;
	bool hunting;
	bool returning;
	Vector3 positionBeforeHunt;

	protected override void Start()
	{
		base.Start();
		StartCoroutine(ChangeDirection());
	}

	void Update()
	{
		if (hunting) {
			if (playerController.sheltered) {
				hunting = false;
				returning = true;
			} else {
				Debug.DrawRay(transform.position, playerController.transform.position - transform.position, Color.red);

				var translation = playerController.transform.position - transform.position;
				transform.Translate(translation.normalized * huntSpeed * Time.deltaTime);

				FixRotation();

				if (Vector3.Distance(transform.position, playerController.transform.position) < 0.3f) {
					GameMaster.instance.Die();
				}
			}
		} else if (returning) {
			var translation = positionBeforeHunt - transform.position;
			transform.Translate(translation.normalized * huntSpeed * Time.deltaTime);

			FixRotation();

			if (Vector3.Distance(transform.position, positionBeforeHunt) < 0.3f) {
				transform.position = positionBeforeHunt;
				returning = false;
			}
		} else {
			if (moveLeft) {
				MoveLeft();
			} else {
				MoveRight();
			}
		}
	}

	void FixRotation()
	{
		var direction = planet.position - transform.position;
		if (direction != Vector3.zero) {
			var newAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
			transform.rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (!hunting && other.gameObject.tag == "Player") {
			var player = other.gameObject.GetComponent<PlayerController>();

			if (!player.sheltered) {
				StartHunt(player);
			}
		}
	}

	void StartHunt(PlayerController player)
	{
		playerController = player;
		positionBeforeHunt = transform.position;
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
