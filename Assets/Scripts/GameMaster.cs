﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
	public static GameMaster instance;

	public Text time;
	public GameObject deadPanel;
	public Text elapsedTime;

	public GameObject eaglePrefab;
	public int eagleCount;

	public GameObject carrotPrefab;
	public int carrotCount;

	public GameObject shelterPrefab;
	public int shelterCount;

	public GameObject treePrefab;
	public int minTrees;
	public int maxTrees;

	float startTime;

	public void Die()
	{
		deadPanel.SetActive(true);
		elapsedTime.text = FormatTime(Time.time);

		Time.timeScale = 0;
	}

	void OnValidate()
	{
		shelterCount = Mathf.Max(1, shelterCount);
	}

	void Awake()
	{
		instance = this;
	}

	void Start()
	{
		deadPanel.SetActive(false);

		SpawnObject(eaglePrefab, eagleCount, 50, 310);
		SpawnObject(carrotPrefab, carrotCount, 0, 360);
		SpawnObject(shelterPrefab, 1, -10, 10); // 1 shelter nearby starting location
		SpawnObject(shelterPrefab, shelterCount - 1, 0, 360);
		SpawnObject(treePrefab, Random.Range(minTrees, maxTrees), 0, 360);
	}

	void SpawnObject(GameObject prefab, int count, float positionFrom, float positionTo)
	{
		for (var i = 0; i < count; i++) {
			var obj = (GameObject)Instantiate(prefab, Vector3.zero, Quaternion.identity);
			var planetPosition = obj.GetComponent<PlanetPosition>();
			planetPosition.angle = Random.Range(positionFrom, positionTo);
			planetPosition.RecalculatePosition();
		}
	}

	void Update()
	{
		UpdateTimerText();
	}

	void UpdateTimerText()
	{
		time.text = FormatTime(Time.time);
	}

	string FormatTime(float time)
	{
		var elapsed = (int)time;
		var text = "";

		var min = elapsed / 60;
		var sec = elapsed % 60;

		if (min > 0) {
			text += min;
		}

		text += ":";

		if (sec < 10) {
			text += "0";
		}

		text += sec;

		return text;
	}
}
