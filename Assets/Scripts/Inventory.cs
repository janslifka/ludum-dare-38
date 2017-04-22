using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
	public static Inventory instance;

	public Text seedsAmmount;
	public Text smallCarrotsAmmount;
	public Text bigCarrotsAmmount;

	int seeds;
	int smallCarrots;
	int bigCarrots;

	public int Seeds {
		get { return seeds; }
		set {
			seeds = value;
			UpdateUI();
		}
	}

	public int SmallCarrots {
		get { return smallCarrots; }
		set {
			smallCarrots = value;
			UpdateUI();
		}
	}

	public int BigCarrots {
		get { return bigCarrots; }
		set {
			bigCarrots = value;
			UpdateUI();
		}
	}

	void Awake()
	{
		instance = this;
	}

	void Start()
	{
		seeds = 3;
		UpdateUI();
	}

	void UpdateUI()
	{
		seedsAmmount.text = seeds + "x";
		smallCarrotsAmmount.text = smallCarrots + "x";
		bigCarrotsAmmount.text = bigCarrots + "x";
	}
}
