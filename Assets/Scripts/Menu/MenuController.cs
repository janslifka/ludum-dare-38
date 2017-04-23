using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
	public Text bestTimeEasy;
	public Text bestTimeMedium;
	public Text bestTimeHard;

	void Start()
	{
		bestTimeEasy.text = FormatBestTime(PlayerPrefs.GetFloat("easy", 0));
		bestTimeMedium.text = FormatBestTime(PlayerPrefs.GetFloat("medium", 0));
		bestTimeHard.text = FormatBestTime(PlayerPrefs.GetFloat("hard", 0));
	}

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

	string FormatBestTime(float time)
	{
		var bestTime = "N/A";
		if (time > 0) {
			bestTime = FormatTime(time);
		}

		return "Best Time: " + bestTime;
	}

	string FormatTime(float time)
	{
		var elapsed = (int)time;
		var text = "";

		var min = elapsed / 60;
		var sec = elapsed % 60;

		//if (min > 0) {
		text += min;
		//}

		text += ":";

		if (sec < 10) {
			text += "0";
		}

		text += sec;

		return text;
	}
}
