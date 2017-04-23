using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
	public void StartEasy()
	{
		PlayerPrefs.SetString("difficulty", "easy");
		StartGame();
	}

	public void StartMedium()
	{
		PlayerPrefs.SetString("difficulty", "medium");
		StartGame();
	}

	public void StartHard()
	{
		PlayerPrefs.SetString("difficulty", "hard");
		StartGame();
	}

	void StartGame()
	{
		SceneManager.LoadScene("Game");
	}
}
