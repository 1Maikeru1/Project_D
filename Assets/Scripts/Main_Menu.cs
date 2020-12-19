using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main_Menu : MonoBehaviour
{
	public SaveC CSave;
	public Text txt;
	[SerializeField] private GameObject[] MenuPanel = new GameObject[2];
	[SerializeField] private GameObject[] ShopPanel = new GameObject[2];

	[SerializeField] private GameObject[] Yolka = new GameObject[2];
	[SerializeField] private GameObject[] Toys = new GameObject[2];

	[SerializeField] private GameObject[] buyButYolka = new GameObject[2];
	[SerializeField] private GameObject[] buyButToys = new GameObject[2];


	public void buyItems1(int index)
	{
		if (CSave.sv.coins >= 250 && CSave.sv.buyItem[index] == false)
		{
			foreach (GameObject m in Yolka)
			{
				m.SetActive(false);
			}
			clearActiveYolka();
			Yolka[index].SetActive(true);
			buyButYolka[index].SetActive(false);
			CSave.sv.buyItem[index]=true;
			CSave.sv.itemActive[index]=true;
			CSave.sv.coins -= 250;
			txt.text = CSave.sv.coins.ToString("0");
			CSave.Save();
		}
		else
		{
			foreach (GameObject m in Yolka)
			{
				m.SetActive(false);
				
			}
			clearActiveYolka();
			CSave.sv.itemActive[index]=true;
			Yolka[index].SetActive(true);
			CSave.Save();
		}
		
	}
	public void buyItems2(int index)
	{
		if (CSave.sv.coins >= 100 && CSave.sv.buyItem[index + 4] == false)
		{
			foreach (GameObject m in Toys)
			{
				m.SetActive(false);
			}
			clearActiveToys();
			Toys[index].SetActive(true);
			buyButToys[index].SetActive(false);
			CSave.sv.buyItem[index + 4]=true;
			CSave.sv.itemActive[index + 4]=true;
			CSave.sv.coins -= 100;
			txt.text = CSave.sv.coins.ToString("0");
			CSave.Save();
		}
		else
		{
			foreach (GameObject m in Toys)
			{
				m.SetActive(false);
			}
			clearActiveToys();
			CSave.sv.itemActive[index + 4]=true;
			Toys[index].SetActive(true);
			CSave.Save();
			
		}
	}

	public void LookAndSelectMenu(int index)
	{
		foreach (GameObject m in MenuPanel)
		{
			m.SetActive(false);
		}
		MenuPanel[index].SetActive(true);
	}

	public void LookAndSelectShop(int index)
	{
		foreach (GameObject m in ShopPanel)
		{
			m.SetActive(false);
		}
		ShopPanel[index].SetActive(true);
	}


	public void GameStart()
	{
		SceneManager.LoadScene("SampleScene");
	}

	public void GameExit()
	{
		Application.Quit();
	}

	void Start()
	{
		for(int i = 0; i < 5;i++)
		{
			if(CSave.sv.buyItem[i] == true && i < 4)
			{
				buyButYolka[i].SetActive(false);
			}
			if(CSave.sv.buyItem[i + 4] == true)
			{
				buyButToys[i].SetActive(false);
			}
		}

		foreach (GameObject m in Yolka)
		{
			m.SetActive(false);
		}
		foreach (GameObject m in Toys)
		{
			m.SetActive(false);
		}

		for (int i = 0; i < 5; i++)
		{
			if(CSave.sv.itemActive[i] == true && i < 4)
			{
				Yolka[i].SetActive(true);
			}
			if(CSave.sv.itemActive[i + 4] == true)
			{
				Toys[i].SetActive(true);
			}
		}
	}

	private void clearActiveYolka()
	{
		for (int i = 0; i < 4; i++)
		{
			CSave.sv.itemActive[i]=false;
		}
	}
	private void clearActiveToys()
	{
		for (int i = 0; i < 5; i++)
		{
			CSave.sv.itemActive[i+4]=false;
		}
	}
}
