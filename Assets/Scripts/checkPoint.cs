using UnityEngine;
using System;
using UnityEngine.UI;


public class checkPoint : MonoBehaviour
{
	private GameObject Player;
	private VCarController VCar;
	public Text text;
	public GameObject panel1;
	public GameObject panel2;
	public Game_Menu gameMenu;
	double i = 1.0;

	void OnTriggerEnter(Collider other)
	{
		if(this.gameObject.name == "check")
		{
			gameMenu.kol+=1;
			text.text = $"{gameMenu.kol}/6";
			this.gameObject.SetActive (false);
		}
		if(this.gameObject.name == "fin")
		{
			for(double j = 0.01; j < 0.35; j+=0.05)
			{
				Invoke("timeFin", (float)j);
			}
			panel1.SetActive(false);
			panel2.SetActive(true);
			gameMenu.klipText.text = $"Клипы {gameMenu.kol.ToString()}/6";
			gameMenu.timeText.text = $"Время: {gameMenu.min}:{gameMenu.sec}";
			if(gameMenu.kol>4)
			{
				gameMenu.CSave.sv.coins = gameMenu.CSave.sv.coins + 100;
				gameMenu.coinKol.text = "100";
			}else if(gameMenu.kol>2 && gameMenu.kol<5)
			{
				gameMenu.CSave.sv.coins = gameMenu.CSave.sv.coins + 50;
				gameMenu.coinKol.text = "50";
			}
			else
			{
				gameMenu.coinKol.text = "0";
			}
			gameMenu.CSave.Save();
		}
	}

	void Start()
	{
		Player = GameObject.FindGameObjectWithTag ("Player");
		VCar = Player.GetComponent<VCarController> ();
		gameMenu.startg = true;
	}

	void Update()
	{
		if(gameMenu.startg)
		{
			if(gameMenu.sec == 2 && gameMenu.min == 0)
			{
				if((bool)panel1.activeInHierarchy)
				{
					for(double j = 0.01; j < 0.35; j+=0.05)
					{
						Invoke("timeFin", (float)j);
					}
					panel1.SetActive(false);
					panel2.SetActive(true);
					gameMenu.klipText.text = $"Клипы {gameMenu.kol.ToString()}/6";
					gameMenu.timeText.text = "Закончилось время";
					gameMenu.coinKol.text = "0";
				}
			}
		}
	}
	void timeFin()
	{
		if(i>0.5)
		{
			i-=0.1;
			Time.timeScale = (float)i;
		}
		else
		{
			Time.timeScale = 0;
		}
	}
}