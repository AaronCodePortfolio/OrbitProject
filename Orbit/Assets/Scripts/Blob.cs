using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Blob
{
	public float spin;
	public GameObject blobject;
	public Vector2 acceleration;

	public Blob(float spin, GameObject blobject, float accelerationX, float accelerationY)
	{
		this.spin = 0;
		this.blobject = blobject;
		this.acceleration = new Vector2(accelerationX, accelerationY);
	}

	public float Spin
	{
		get
		{
			return this.spin;
		}
		set
		{
			this.spin = value;
		}
	}

	public GameObject Blobject
	{
		get
		{
			return this.blobject;
		}
		set
		{
			this.blobject = value;
		}
	}

	public Vector2 Acceleration
	{
		get
		{
			return this.acceleration;
		}
		set
		{
			this.acceleration = value;
		}
	}

	public void ApplyAcceleration(Vector2 gravity)
	{
		acceleration.x += gravity.x / ((4/3) * Mathf.PI * (Mathf.Pow(blobject.transform.lossyScale.x, 3)));
		acceleration.y += gravity.y / ((4 / 3) * Mathf.PI * (Mathf.Pow(blobject.transform.lossyScale.x, 3)));
	}
}

