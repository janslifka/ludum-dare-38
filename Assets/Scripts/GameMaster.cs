using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
	public static GameMaster instance;

	public Text time;
	public GameObject deadPanel;
	public Text elapsedTime;

	float startTime;

	public void Die()
	{
		deadPanel.SetActive(true);
		elapsedTime.text = FormatTime(Time.time);

		Time.timeScale = 0;
	}

	void Awake()
	{
		instance = this;
	}

	void Start()
	{
		deadPanel.SetActive(false);
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
