using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
	public static GameMaster instance;

	public DifficultyObject easy;
	public DifficultyObject medium;
	public DifficultyObject hard;

	public Text time;
	public GameObject deadPanel;
	public Text elapsedTime;
	public Text difficultyText;

	public GameObject eaglePrefab;
	public GameObject carrotPrefab;
	public GameObject shelterPrefab;
	public GameObject treePrefab;

	public int minTrees;
	public int maxTrees;

	float startTime;

	DifficultyObject currentDifficulty;

	string difficulty;

	public void Die()
	{
		deadPanel.SetActive(true);
		var elapsed = Time.time - startTime;
		elapsedTime.text = FormatTime(elapsed);
		difficultyText.text = difficulty;

		if (elapsed > PlayerPrefs.GetFloat(difficulty, 0)) {
			PlayerPrefs.SetFloat(difficulty, elapsed);
		}

		Time.timeScale = 0;
	}

	public void LoadMainMenu()
	{
		SceneManager.LoadScene("Menu");
	}

	public void TryAgain()
	{
		SceneManager.LoadScene("Game");
	}

	void Awake()
	{
		instance = this;
	}

	void Start()
	{
		Time.timeScale = 1;

		startTime = Time.time;

		ChooseDifficulty();

		deadPanel.SetActive(false);

		InitializeInventory();

		SpawnObject(eaglePrefab, currentDifficulty.eagles, 50, 310);
		SpawnObject(carrotPrefab, currentDifficulty.carrots, 0, 360);
		SpawnObject(shelterPrefab, 1, -10, 10); // 1 shelter nearby starting location
		SpawnObject(shelterPrefab, currentDifficulty.shelters - 1, 0, 360);
		SpawnObject(treePrefab, Random.Range(minTrees, maxTrees), 0, 360);
	}

	void ChooseDifficulty()
	{
		difficulty = PlayerPrefs.GetString("difficulty", "easy");

		if (difficulty != "easy" && difficulty != "medium" && difficulty != "hard") {
			difficulty = "easy";
		}

		if (difficulty == "easy") {
			currentDifficulty = easy;
		} else if (difficulty == "medium") {
			currentDifficulty = medium;
		} else {
			currentDifficulty = hard;
		}
	}

	void InitializeInventory()
	{
		Inventory.instance.Seeds = currentDifficulty.invSeeds;
		Inventory.instance.SmallCarrots = currentDifficulty.invSmallCarrots;
		Inventory.instance.BigCarrots = currentDifficulty.invBigCarrots;
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
		time.text = FormatTime(Time.time - startTime);
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
