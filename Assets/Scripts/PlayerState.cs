using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
	public Slider hungerSlider;
	public Slider energySlider;

	public Image hungerFill;
	public Image energyFill;

	public Gradient hungerGradient;
	public Gradient energyColor;

	public float energyGain;
	public float starving;

	float hunger = 75;
	float energy = 100;

	public float Energy {
		get { return energy; }
		set {
			energy = Mathf.Clamp(value, 0, 100);
			UpdateUI();
		}
	}

	public float Hunger {
		get { return hunger; }
		set {
			hunger = Mathf.Clamp(value, 0, 100);
			UpdateUI();
		}
	}

	void Start()
	{
		UpdateUI();
		InvokeRepeating("AddEnergy", 1, 1);
		InvokeRepeating("Starving", 5, 5);
	}

	void AddEnergy()
	{
		if (Mathf.Approximately(Input.GetAxis("Horizontal"), 0) || !(Energy > 0)) {
			Energy += energyGain;	
		}
	}

	void Starving()
	{
		Hunger -= starving;
	}

	void UpdateUI()
	{
		var energyValue = Energy / 100f;
		var hungerValue = Hunger / 100f;

		hungerSlider.value = Hunger / 100f;
		energySlider.value = energyValue;

		hungerFill.color = hungerGradient.Evaluate(hungerValue);
		energyFill.color = energyColor.Evaluate(energyValue);
	}

}
