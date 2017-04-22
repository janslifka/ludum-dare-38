using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Carrot : MonoBehaviour
{
	public enum State {
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

	SpriteRenderer spriteRenderer;

	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();

		SetSprite();

		StartCoroutine(LifeCycle());
	}

	void SetState(State toState)
	{
		state = toState;
		SetSprite();
	}

	void SetSprite()
	{
		Sprite sprite = null;

		switch(state) {
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
