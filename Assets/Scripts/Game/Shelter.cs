﻿using UnityEngine;

public class Shelter : ActiveObject
{
	public Color highlightColor;
	public Color standardColor;
	public SpriteRenderer spriteRenderer;

	public override void Highlight()
	{
		spriteRenderer.color = highlightColor;
	}

	public override void Unhighlight()
	{
		spriteRenderer.color = standardColor;
	}

	public override void Use()
	{
		PlayerController.instance.Shelter();
	}

	void Start()
	{
		spriteRenderer.color = standardColor;
	}
}
