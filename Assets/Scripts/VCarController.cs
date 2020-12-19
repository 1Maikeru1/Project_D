using UnityEngine;


[System.Serializable]
public class AxleInfo
{
	public WheelCollider leftWheel;
	public WheelCollider rightWheel;

	public Transform visLeftWheel;
	public Transform visRightWheel;

	public bool motor;
	public bool steering;
}

public class VCarController : MonoBehaviour
{	
	//public bool automatic=false;
	public AxleInfo [] carAxis = new AxleInfo[2];
	public WheelCollider [] wheelColliders;
	public float topSpeed;
	public float steerAngle;
	public float brakeSpeed;
	public Transform centerOfMass;
	[Range(0,1)]
	public float steerHelp = 0;
	public float KPH;

	private float wheelsRPM = 0, lastValue = 0, totalPower = 0;
	public float maxRPM , minRPM;
	public float[] gears;
	public float[] gearChangeSpeed;
	public AnimationCurve enginePower;
	public int gearNum = 1;
	[HideInInspector]public float engineRPM;
	[HideInInspector]public bool test; //engine sound boolean
	[HideInInspector]public bool reverse = false;
	private bool flag=false;
	

	public float minSpeedForSmoke;
	public float minAngleForSmoke;
	public ParticleSystem [] tireSmokeEffects;

	public float horInput;
	public float verInput;
	bool brakeBtn;
	Quaternion startHelmRotation;

	Rigidbody rb;
	bool onGround;
	float lastYRotation;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		rb.centerOfMass = centerOfMass.localPosition;
	}

	void FixedUpdate()
	{
		horInput = Input.GetAxis("Horizontal");
		verInput = Input.GetAxis("Vertical");
		brakeBtn = Input.GetButton("Jump");
		
		CheckonGround();
		Accelerate();
		EmitSmokeFromTires();
		steerHelpAssist();
		calculateEnginPower();
	}

	void Accelerate()
	{
		foreach (AxleInfo axle in carAxis)
		{
			if (axle.steering)
			{
				axle.leftWheel.steerAngle = steerAngle * horInput;
				axle.rightWheel.steerAngle = steerAngle * horInput;
			}
			if (axle.motor)
			{
				axle.leftWheel.motorTorque = totalPower/2;
				axle.rightWheel.motorTorque = totalPower/2;
			}
			if (brakeBtn)
			{
				axle.leftWheel.brakeTorque = brakeSpeed;
				axle.rightWheel.brakeTorque = brakeSpeed;
			}
			else
			{
				axle.leftWheel.brakeTorque = 0;
				axle.rightWheel.brakeTorque = 0;
			}
			VisualWheelsToColliders(axle.leftWheel, axle.visLeftWheel);
			VisualWheelsToColliders(axle.rightWheel, axle.visRightWheel);
		}
		KPH = rb.velocity.magnitude * 3.6f;
	}

	void VisualWheelsToColliders(WheelCollider col, Transform visWheel)
	{
		Vector3 position;
		Quaternion rotation;
		col.GetWorldPose(out position, out rotation);
		visWheel.position = position;
		visWheel.rotation = rotation;
	}

	void steerHelpAssist()
	{
		if(!onGround)
			return;

		if(Mathf.Abs(transform.rotation.eulerAngles.y - lastYRotation)< 10f)
		{
			float turnAdjust =(transform.rotation.eulerAngles.y - lastYRotation) * steerHelp;
			Quaternion rotatHelp = Quaternion.AngleAxis(turnAdjust, Vector3.up);
			rb.velocity = rotatHelp * rb.velocity;
		}
		lastYRotation = transform.rotation.eulerAngles.y;
	}

	void CheckonGround()
	{
		onGround = true;
		foreach(WheelCollider wheelCol in wheelColliders)
		{
			if(!wheelCol.isGrounded)
				onGround = false;
		}
	}
	void EmitSmokeFromTires()
	{
		if(KPH > minSpeedForSmoke)
		{
			float angle = Quaternion.Angle(Quaternion.LookRotation(rb.velocity, Vector3.up), Quaternion.LookRotation(transform.forward, Vector3.up));
			if(angle > minAngleForSmoke && angle < 110 && wheelColliders[3].isGrounded && wheelColliders[2].isGrounded) //onGround - некорректно работает!!!
			{
				SwithSmokeParticles(true);
			}
			else
			{
				SwithSmokeParticles(false);
			}
		}
		else
		{
			SwithSmokeParticles(false);
		}
	}
	
	void SwithSmokeParticles(bool _enable)
	{
		foreach(ParticleSystem ps in tireSmokeEffects)
		{
			ParticleSystem.EmissionModule psEm = ps.emission;
			psEm.enabled = _enable;
			rb.AddForce(transform.forward * (KPH / 400) * 4000);
		}
	}

	private void calculateEnginPower()
	{
		wheelRPM();
		if (verInput != 0 ){
			rb.drag = 0.005f; 
		}
		if (verInput == 0){
			rb.drag = 0.1f;
		}
		totalPower = 3.6f * enginePower.Evaluate(engineRPM) * (verInput);

		float velocity  = 0.0f;
		if (engineRPM >= maxRPM || flag ){
			engineRPM = Mathf.SmoothDamp(engineRPM, maxRPM - 500, ref velocity, 0.05f);

			flag = (engineRPM >= maxRPM - 450)?  true : false;
			test = (lastValue > engineRPM) ? true : false;
		}
		else {
			engineRPM = Mathf.SmoothDamp(engineRPM,1000 + (Mathf.Abs(wheelsRPM) * 3.6f * (gears[gearNum])), ref velocity , 0.09f);
			test = false;
		}
		if (engineRPM >= maxRPM + 1000) engineRPM = maxRPM + 1000; // clamp at max
		shifter();
	}

	private void wheelRPM()
	{
		float sum = 0;
		int R = 0;
		for(int i = 0; i<4; i++)
		{
			sum += wheelColliders[i].rpm;
			R++;
		}
		wheelsRPM = (R != 0) ? sum/R : 0;

		if(wheelsRPM < 0 && !reverse ){
			reverse = true;
		}
		else if(wheelsRPM > 0 && reverse){
			reverse = false;
		}
	}

	private bool checkGears(){
		if(KPH >= gearChangeSpeed[gearNum] ) return true;
		else return false;
	}

	private void shifter()
	{
		if(!onGround)return;

		//automatic
		if(engineRPM > maxRPM && gearNum < gears.Length-1 && !reverse && checkGears() )
		{
			gearNum ++;
		}
		if(engineRPM < minRPM && gearNum > 0)
		{
			gearNum --;
		}
	}
}
