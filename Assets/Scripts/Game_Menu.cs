using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Menu : MonoBehaviour
{
	
	public void BakcMenu()
	{
		SceneManager.LoadScene("MainScene");
		Time.timeScale = 1;
	}


}
