using UnityEngine;
using UnityEngine.UI;

public class ColorTool : MonoBehaviour
{
	public SaveC CSave;
	public Slider colorRed;
	public Slider colorGreen;
	public Slider colorBlue;
	public Image colorPanel;
	public Text coinsCol;

	public GameObject car;
	public void Start()
	{
		colorRed.value = CSave.sv.cr;
		colorGreen.value = CSave.sv.cg;
		colorBlue.value = CSave.sv.cb;
		colorUpdate();
	}

	public void colorUpdate()
	{
		colorPanel.color = new Color(colorRed.value, colorGreen.value, colorBlue.value);
	}

	public void buyColor()
	{
		if(CSave.sv.coins > 250)
		{
			car.GetComponent<Renderer>().material.color = new Color(colorRed.value, colorGreen.value, colorBlue.value);
			CSave.sv.cr = colorRed.value;
			CSave.sv.cg = colorGreen.value;
			CSave.sv.cb = colorBlue.value;
			CSave.sv.coins = CSave.sv.coins - 250;
			coinsCol.text = CSave.sv.coins.ToString("0");
			CSave.Save();
		}
	}
}
