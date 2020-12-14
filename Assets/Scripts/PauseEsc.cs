using UnityEngine;

public class PauseEsc : MonoBehaviour
{
	private bool paused = false;
	public GameObject panel;
	public GameObject pan;

	void Start ()
	{
	
	}
	
	void Update ()
	{
		if((bool)pan.activeInHierarchy)
		{
			if (Input.GetKeyDown (KeyCode.Escape))
			{
				if (!paused)
				{
					Time.timeScale = 0;
					paused = true;
					panel.SetActive (true);
				}
				else
				{
					Time.timeScale = 1;
					paused = false;
					panel.SetActive (false);
				}
			}
		}
	}
}
