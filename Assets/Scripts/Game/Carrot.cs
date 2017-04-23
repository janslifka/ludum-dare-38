using System.Collections;
using UnityEngine;

public class Carrot : ActiveObject
{
	public enum State
	{
		Seed,
		Young,
		Grown,
		Bloom
	}

	public Sprite seedSprite;
	public Sprite youngSprite;
	public Sprite grownSprite;
	public Sprite bloomSprite;

	public float timeDelta;

	public float seedTime;
	public float youngTime;
	public float grownTime;
	public float bloomTime;

	public State state;

	public Color highlightColor;
	public Color standardColor;

	new Collider2D collider2D;
	public SpriteRenderer spriteRenderer;

	public override void Use()
	{
		bool destroy = false;
		switch (state) {
			case State.Young:
				Inventory.instance.SmallCarrots++;
				destroy = true;
				break;
			case State.Grown:
				Inventory.instance.BigCarrots++;
				destroy = true;
				break;
			case State.Bloom:
				Inventory.instance.Seeds += Random.Range(2, 6);
				destroy = true;
				break;
		}

		if (destroy) {
			Destroy(gameObject);
		}
	}

	public override void Highlight()
	{
		spriteRenderer.color = highlightColor;
	}

	public override void Unhighlight()
	{
		spriteRenderer.color = standardColor;
	}

	void Start()
	{
		collider2D = GetComponent<Collider2D>();
		spriteRenderer.color = standardColor;

		SetState(State.Seed);
		StartCoroutine(LifeCycle());
	}

	void SetState(State toState)
	{
		if (toState == State.Seed) {
			collider2D.enabled = false;
		} else {
			collider2D.enabled = true;
		}

		state = toState;
		SetSprite();
	}

	void SetSprite()
	{
		Sprite sprite = null;

		switch (state) {
			case State.Seed:
				sprite = seedSprite;
				break;
			case State.Young:
				sprite = youngSprite;
				break;
			case State.Grown:
				sprite = grownSprite;
				break;
			case State.Bloom:
				sprite = bloomSprite;
				break;
		}

		spriteRenderer.sprite = sprite;
	}

	float RandomizeTime(float time)
	{
		return time + Random.Range(-timeDelta, timeDelta);
	}

	IEnumerator LifeCycle()
	{
		yield return new WaitForSeconds(RandomizeTime(seedTime));
		SetState(State.Young);
		yield return new WaitForSeconds(RandomizeTime(youngTime));
		SetState(State.Grown);
		yield return new WaitForSeconds(RandomizeTime(grownTime));
		SetState(State.Bloom);
		yield return new WaitForSeconds(RandomizeTime(bloomTime));
		Destroy(gameObject);
	}
}
