using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{

	public void GameStart()
	{
		SceneManager.LoadScene("SampleScene");

	}


	public void GameExit()
	{
		Application.Quit();
	}
}
