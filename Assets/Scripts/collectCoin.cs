using UnityEngine;
using UnityEngine.UI;
using System;

public class collectCoin : MonoBehaviour
{
	public SaveC CSave;

	public Text coinsCol;
	void Start()
	{
		coinsCol.text = CSave.sv.coins.ToString("0");
	}
}
