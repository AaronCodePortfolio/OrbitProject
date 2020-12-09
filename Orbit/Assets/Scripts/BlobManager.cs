using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections;

public class BlobManager : MonoBehaviour
{
	private List<Blob> blobList = new List<Blob>();
	public GameObject blobPrefab;

    // Start is called before the first frame update
    void Start()
    {
		//Create the core blob
		GameObject core = Instantiate(blobPrefab, new Vector3(0, 0, 0), Quaternion.identity);
		core.name = "Core";
		core.transform.localScale = new Vector3(10, 10, 10);
		core.GetComponent<Renderer>().material.SetColor("_Color", Color.red);

		//Add that blob to the list
		blobList.Add(new Blob(0, core, 0.0f, 0.0f));

		//Create the floater blob
		GameObject floater = Instantiate(blobPrefab, new Vector3(0, 20, 0), Quaternion.identity);
		floater.name = "floater";

		floater.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);

		//Add that blob to the list
		blobList.Add(new Blob(0, floater, 5f, 0f));

		////Create the zorp blob
		//GameObject zorp = Instantiate(blobPrefab, new Vector3(29, 29, 0), Quaternion.identity);
		//zorp.name = "zorp";

		//zorp.GetComponent<Renderer>().material.SetColor("_Color", Color.green);

		////Add that blob to the list
		//blobList.Add(new Blob(0, zorp, 1f, 0f));
	}

    // Update is called once per frame
    void Update()
    {
		int looperMain;
		int looperGravity;
		Vector2 gravity;



		//For each blob in the list
		for(looperMain = 0; looperMain < blobList.Count; looperMain++)
		{
			//For each blob after the current blob in the list
			for(looperGravity = looperMain + 1; looperGravity < blobList.Count; looperGravity++)
			{
				gravity = GravityForce(blobList[looperMain], blobList[looperGravity]);

				blobList[looperGravity].ApplyAcceleration(gravity);
				blobList[looperMain].ApplyAcceleration(gravity * -1);
			}

			//print the blob's name and final acceleration
			Debug.Log("Name: " + blobList[looperMain].Blobject.name + " acceleration: " + blobList[looperMain].acceleration);
		}

		for (looperMain = 1; looperMain < blobList.Count; looperMain++)
		{
			//Determine if the blob is too far away from the core
			if(Mathf.Pow(blobList[looperMain].Blobject.transform.position.x, 2) + Mathf.Pow(blobList[looperMain].Blobject.transform.position.x, 2) > 1600)
			{
				blobList[looperMain].ApplyAcceleration(OuterGravityForce(blobList[looperMain].Blobject.transform.position));
			}

			//Move the blob
			//blobList[looperMain].Blobject.transform.Translate(blobList[looperMain].Acceleration.x, blobList[looperMain].Acceleration.y, 0);
			blobList[looperMain].Blobject.transform.Translate(blobList[looperMain].Acceleration.x, blobList[looperMain].Acceleration.y, 0);
		}
			
		//For each blob (except the last)
			//For each blob after it in the list
			//Check for collisions
			//Determine what one is larger
			//Add the smaller one's mass to the larger
			//Apply the average force of both objects
			//Use mass to update blob scale and 
			//Test if spin is high enough to launch 
			//Apply spin
			//Determine gravity pull's
			//For each blob
			//Move
	}

	private float Distance(Vector3 dif)
	{
		return Mathf.Sqrt(Mathf.Abs(dif.x * dif.x + dif.y * dif.y));
	}

	private Vector2 GravityForce(Blob blob1, Blob blob2)
	{
		Vector3 difference;
		float gravity;

		difference = blob1.Blobject.transform.position - blob2.Blobject.transform.position;

		gravity = 0.6f * (((4 / 3) * Mathf.PI * (Mathf.Pow(blob1.Blobject.transform.lossyScale.x, 3))) * ((4 / 3) * Mathf.PI * (Mathf.Pow(blob2.Blobject.transform.lossyScale.x, 3)))) / Mathf.Pow(Distance(difference), 2);


		return new Vector2(gravity * (difference.x / (Mathf.Abs(difference.x) + Mathf.Abs(difference.y))), gravity * (difference.y / (Mathf.Abs(difference.x) + Mathf.Abs(difference.y))));
	}


	////Calculate and return the position on the edge of a circle that intersects a line between the center of the circle and a point outside the circle
	//private Vector2 PointOnEdgeOfCircle(Vector2 center, Vector2 outer, float radius)
	//{
	//	float distanceToCircle;
	//	float slope;
	//	Vector2 circlePoint;

	//	distanceToCircle = Mathf.Sqrt(Mathf.Abs(((outer.x - center.x) * (outer.x - center.x)) + ((outer.y - center.y) * (outer.y - center.y)))) - radius;

	//	slope = Mathf.Abs((outer.y - center.y) / (outer.x - center.x));

	//	//Determine if the slope is 0, infinite, or else
	//	if (slope == 0)
	//	{
	//		circlePoint.x = outer.x - (distanceToCircle * (outer.x / Mathf.Abs(outer.x)));
	//		circlePoint.y = outer.y;
	//	}
	//	else if (float.IsInfinity(slope))
	//	{
	//		circlePoint.x = outer.x;
	//		circlePoint.y = outer.y - (distanceToCircle * (outer.y / Mathf.Abs(outer.y)));
	//	}
	//	else
	//	{
	//		circlePoint.x = outer.x - ((outer.x / Mathf.Abs(outer.x)) * (distanceToCircle / Mathf.Sqrt(1 + (slope * slope))));
	//		circlePoint.y = outer.y - ((outer.y / Mathf.Abs(outer.y)) * (slope * (distanceToCircle / Mathf.Sqrt(1 + (slope * slope)))));
	//	}

	//	return circlePoint;
	//}

	//private Vector2 CirclePointGravity(Vector2 center, Blob outer, float radius)
	//{
	//	Vector2 difference;
	//	Vector2 circlePoint;
	//	float gravity;

	//	circlePoint = PointOnEdgeOfCircle(center, outer.Blobject.transform.position, radius);

	//	difference = circlePoint - (Vector2)outer.Blobject.transform.position;

	//	gravity = 0.6f * (Distance(difference) * outer.Blobject.transform.lossyScale.x * 5) / Mathf.Pow(Distance(difference), 2);

	//	return new Vector2(gravity * (difference.x / (Mathf.Abs(difference.x) + Mathf.Abs(difference.y))), gravity * (difference.y / (Mathf.Abs(difference.x) + Mathf.Abs(difference.y))));
	//}

	private Vector2 OuterGravityForce(Vector2 blobPos)
	{

		//(x/|x|) * 0.025 * x^2 - (x/|x|)40
		return new Vector2(-(blobPos.x/Mathf.Abs(blobPos.x)) * 0.0025f * (blobPos.x * blobPos.x)/* + (blobPos.x / Mathf.Abs(blobPos.x)) * 40*/, -(blobPos.y / Mathf.Abs(blobPos.y)) * 0.0025f * (blobPos.y * blobPos.y) /*+ (blobPos.y / Mathf.Abs(blobPos.y)) * 40*/);
	}

	private float Tanh(float x)
	{
		return (2 / (1 + Mathf.Pow((float)Math.E, -2 * x))) - 1;
	}
}
