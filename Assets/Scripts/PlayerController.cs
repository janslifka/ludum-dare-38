using UnityEngine;

public class PlayerController : PlanetMovement
{
	public LayerMask activeObjects;

	ActiveObject highlighted;

	void Update()
	{
		Move();
		FindItemForSelection();

		if (Input.GetKeyDown(KeyCode.Return) && highlighted != null) {
			highlighted.Use();
			highlighted = null;
		}
	}

	void Move()
	{
		if (Input.GetAxis("Horizontal") > 0) {
			MoveRight();
		} else if (Input.GetAxis("Horizontal") < 0) {
			MoveLeft();
		}
	}

	void FindItemForSelection()
	{
		var hitColliders = Physics2D.OverlapCircleAll(transform.position, 0.48f, activeObjects);

		GameObject closest = null;

		foreach (var hitCollider in hitColliders) {
			if (closest == null) {
				closest = hitCollider.gameObject;
			} else if (Vector3.Distance(closest.transform.position, transform.position) > Vector3.Distance(hitCollider.transform.position, transform.position)) {
				closest = hitCollider.gameObject;
			}
		}

		if (closest != null) {
			SelectItem(closest.GetComponent<ActiveObject>());
		} else if (highlighted != null) {
			highlighted.Unhighlight();
			highlighted = null;
		}
	}

	void SelectItem(ActiveObject item)
	{
		if (highlighted != null) {
			highlighted.Unhighlight();
		}

		item.Highlight();
		highlighted = item;
	}
}
