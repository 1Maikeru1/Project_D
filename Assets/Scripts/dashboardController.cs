using UnityEngine;
using UnityEngine.UI;


public class dashboardController : MonoBehaviour
{
	private GameObject Player;
	private VCarController VCarC;
	Quaternion neeedle;

	public float startPositionRPM = 119f, endPositionRPM = -119f;
	private float desirdPosition;
	public Transform rpm;
	public Text kph;
	public Text gear;

	void Start()
	{
		Player = GameObject.FindGameObjectWithTag ("Player");
		VCarC = Player.GetComponent<VCarController> ();
		neeedle = rpm.localRotation;
	}

	void FixedUpdate()
	{
		kph.text = VCarC.KPH.ToString("0");
		updateNeedle();
		changerGear();
	}

	void updateNeedle()
	{
		desirdPosition = startPositionRPM - endPositionRPM;
		float temp = VCarC.engineRPM / 10000;
		rpm.transform.eulerAngles = new Vector3(0,0,(startPositionRPM - temp * desirdPosition));
	}

	public void changerGear()
	{
		gear.text =(!VCarC.reverse) ? (VCarC.gearNum + 1).ToString () : "R";
		if(VCarC.gearNum == 0 && VCarC.KPH <= 0.01)
			gear.text = "N";
	}
}
