using UnityEngine;
using UnityEngine.UI;


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
	public AxleInfo [] carAxis = new AxleInfo[2];
	public WheelCollider [] wheelColliders;
	public float topSpeed;
	public float steerAngle;
	public float brakeSpeed;
	public Transform centerOfMass;
	[Range(0,1)]
	public float steerHelp = 0;
	public float KPH;
	public Text kph;

	public float minSpeedForSmoke;
	public float minAngleForSmoke;
	public ParticleSystem [] tireSmokeEffects;

	float horInput;
	float verInput;
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
		kph.text = KPH.ToString("0");
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
				axle.leftWheel.motorTorque = topSpeed * verInput;
				axle.rightWheel.motorTorque = topSpeed * verInput;
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
			if(angle > minAngleForSmoke && angle < 110) //if(angle > minAngleForSmoke && angle < 160 && onGround) - некорректно работает!!!
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
		}
	}
}
