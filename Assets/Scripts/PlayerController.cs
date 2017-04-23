using UnityEngine;

public class PlayerController : PlanetMovement
{
	public static PlayerController instance;

	public float sprintSpeed;
	public float movementEnergyCost;
	public float sprintEnergyCost;

	public int smallCarrotValue;
	public int bigCarrotValue;

	public LayerMask activeObjects;
	public GameObject carrotPrefab;

	ActiveObject highlighted;
	PlayerState playerState;

	float originalMovementSpeed;
	float originalEnergyCost;

	SpriteRenderer spriteRenderer;
	Sprite originalSprite;

	public bool sheltered;

	public bool Sheltered {
		get { return sheltered; }
	}

	public void Shelter()
	{
		sheltered = true;
		spriteRenderer.sprite = null;
	}

	void Awake()
	{
		instance = this;
	}

	protected override void Start()
	{
		base.Start();

		playerState = GetComponent<PlayerState>();
		spriteRenderer = GetComponent<SpriteRenderer>();

		originalSprite = spriteRenderer.sprite;

		originalMovementSpeed = movementSpeed;
		originalEnergyCost = movementEnergyCost;
	}

	void Update()
	{
		Move();
		HandleActiveItems();
		HandleInventoryItemsUsage();
	}

	void Move()
	{
		if (sheltered) return;

		if (Input.GetKeyDown(KeyCode.LeftShift)) {
			movementEnergyCost = sprintEnergyCost;
			movementSpeed = sprintSpeed;
		} else if (Input.GetKeyUp(KeyCode.LeftShift)) {
			movementEnergyCost = originalEnergyCost;
			movementSpeed = originalMovementSpeed;
		}

		if (playerState.Energy >= movementEnergyCost * Time.deltaTime) {
			if (Input.GetAxis("Horizontal") > 0) {
				MoveRight();
				playerState.Energy -= movementEnergyCost * Time.deltaTime;
			} else if (Input.GetAxis("Horizontal") < 0) {
				MoveLeft();
				playerState.Energy -= movementSpeed * Time.deltaTime;
			}
		}
	}

	void HandleActiveItems()
	{
		FindItemForSelection();

		if (Input.GetKeyDown(KeyCode.Space)) {
			if (sheltered) {
				sheltered = false;
				spriteRenderer.sprite = originalSprite;
			} else if (highlighted != null) {
				highlighted.Use();
				highlighted = null;
			}
		}
	}

	void HandleInventoryItemsUsage()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			PlantCarrot();
		}

		if (Input.GetKeyDown(KeyCode.Alpha2)) {
			EatSmallCarrot();
		}

		if (Input.GetKey(KeyCode.Alpha3)) {
			EatBigCarrot();
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

	void PlantCarrot()
	{
		if (Inventory.instance.Seeds > 0) {
			var carrot = Instantiate(carrotPrefab, planet.transform.position, Quaternion.identity);
			var carrotPosition = carrot.GetComponent<PlanetPosition>();
			carrotPosition.angle = angle;
			carrotPosition.RecalculatePosition();
			Inventory.instance.Seeds--;
		}
	}

	void EatSmallCarrot()
	{
		if (Inventory.instance.SmallCarrots > 0) {
			playerState.Hunger += smallCarrotValue;
			Inventory.instance.SmallCarrots--;
		}
	}

	void EatBigCarrot()
	{
		if (Inventory.instance.BigCarrots > 0) {
			playerState.Hunger += bigCarrotValue;
			Inventory.instance.BigCarrots--;
		}
	}

}
