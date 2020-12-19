using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game_Menu : MonoBehaviour
{
	[SerializeField] private GameObject[] MenuPanel = new GameObject[2];
	public float kol;
	public int coink;
	public float min = 1;
	public float sec = 40;
	public float msec = 1;
	public bool startg = false;
	public Text klipText;
	public Text timerText;
	public Text timeText;
	public Text coinKol;
	public SaveC CSave;


	public void LookAndSelectMenu(int index)
	{
		foreach (GameObject m in MenuPanel)
		{
			m.SetActive(false);
		}
		MenuPanel[index].SetActive(true);
	}

	public void BakcMenu()
	{
		SceneManager.LoadScene("MainScene");
		Time.timeScale = 1;
	}

	public void Restart()
	{
		SceneManager.LoadScene("SampleScene");
		Time.timeScale = 1;
	}
	public void GameExit()
	{
		Application.Quit();
	}

	void FixedUpdate()
	{
		if(startg)
			msec-=0.02f;
		
		if(msec <= 0.01)
		{
			sec-=1;
			msec=1;
		}

		if(sec == 0)
		{
			min-=1;
			sec=60;
		}
		timerText.text = $"{min}:{sec}";
		if(startg && sec == 1 && min == 0)
		{
			startg = false;

		}
	}

}
