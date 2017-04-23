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

	public void QuitGame()
	{
		Application.Quit();
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
		var bestTime = "?";
		if (time > 0) {
			bestTime = TimeUtils.FormatTime(time);
		}

		return "Best Time: " + bestTime;
	}
}
