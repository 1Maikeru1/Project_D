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
	public int index;

	public int byValue;

	public GameObject[] colorMaterial;
	public void Start()
	{
		colorRed.value = CSave.sv.cr[index];
		colorGreen.value = CSave.sv.cg[index];
		colorBlue.value = CSave.sv.cb[index];
		colorUpdate();
	}

	public void colorm(int i)
	{
		index = i;
	}

	public void colorUpdate()
	{
		colorPanel.color = new Color(colorRed.value, colorGreen.value, colorBlue.value);
	}

	public void buyColor()
	{
		if(CSave.sv.coins >= byValue)
		{
			colorMaterial[index].GetComponent<Renderer>().material.color = new Color(colorRed.value, colorGreen.value, colorBlue.value);
			CSave.sv.cr[index] = colorRed.value;
			CSave.sv.cg[index] = colorGreen.value;
			CSave.sv.cb[index] = colorBlue.value;
			CSave.sv.coins = CSave.sv.coins - byValue;
			coinsCol.text = CSave.sv.coins.ToString("0");
			CSave.Save();
		}
	}
}
