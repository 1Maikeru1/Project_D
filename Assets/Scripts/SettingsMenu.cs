using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SettingsMenu : MonoBehaviour
{
	public Dropdown settingG;
	public Dropdown settingR;
	public Toggle settingSF;
	Resolution [] res;

	void Start()
	{
		//Quality Settings
		settingG.AddOptions(QualitySettings.names.ToList());
		settingG.value = QualitySettings.GetQualityLevel();

		//Resolution setting
		Resolution [] resolutions = Screen.resolutions;
		res = resolutions.Distinct().ToArray();
		string[] strRes = new string[res.Length];
		for (int i = 0; i < res.Length; i++)
		{
			strRes[i] = res[i].ToString();
			//strRes[i] = res[i].width.ToString() + "x" + res[i].height.ToString();
		}
		settingR.AddOptions(strRes.ToList());
		settingSF.isOn = Screen.fullScreen;
	}

	public void setG()
	{
		QualitySettings.SetQualityLevel(settingG.value);
	}

	public void setR()
	{
		Screen.SetResolution(res[settingR.value].width, res[settingR.value].height, Screen.fullScreen);
	}

	public void setSF()
	{
		Screen.fullScreen = settingSF.isOn;
	}
	void Update()
	{
		
	}
}
