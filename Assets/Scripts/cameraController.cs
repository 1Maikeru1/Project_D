using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour {

	private GameObject Player;
	private VCarController RR;
	public GameObject cameralookAt,cameraPos;
	private float speed = 5;
	[Range (0, 50)] public float smothTime = 15;

	private void Start()
	{
		Player = GameObject.FindGameObjectWithTag ("Player");
		RR = Player.GetComponent<VCarController> ();
	}

	private void FixedUpdate()
	{
		follow ();
	}
	private void follow()
	{
		speed = RR.KPH / smothTime;
		gameObject.transform.position = Vector3.Lerp (transform.position, cameraPos.transform.position ,  Time.deltaTime * speed);
		gameObject.transform.LookAt (cameralookAt.gameObject.transform.position);
	}
}