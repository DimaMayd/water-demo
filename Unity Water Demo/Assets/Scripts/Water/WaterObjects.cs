using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ditzelgames;

public class WaterObjects : MonoBehaviour {

    public float airDrag = 1;
    public float waterDrag = 10;
    public float buoyancy = 0;
    public Transform[] floatPoints;

    protected new Rigidbody rigidbody;
    protected WaterPhysics waterPhysics;

    protected float waterLine;
    protected Vector3[] waterLinePoints;

    protected Vector3 centreOffset;
    public Vector3 center { get { return transform.position = centreOffset; } }

	// Use this for initialization
	void Awake () {
        waterPhysics = FindObjectOfType<WaterPhysics>();
        rigidbody = GetComponent<Rigidbody>();
        //rigidbody.useGravity = false;

        waterLinePoints = new Vector3[floatPoints.Length];
        for(int i = 0; i < floatPoints.Length; i++)
        {
            waterLinePoints[i] = floatPoints[i].position;
            centreOffset = PhysicsHelper.GetCenter(waterLinePoints) - transform.position;
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        var newWaterLine = 0f;
        var pointUnderWater = false;
        buoyancy = waterDrag - airDrag;

        for(int i = 0; i < floatPoints.Length; i++)
        {
            waterLinePoints[i] = floatPoints[i].position;
            //waterLinePoints[i].y = waterPhysics.GetHeight(floatPoints[i].position);

            newWaterLine += waterLinePoints[i].y / floatPoints.Length;
            if (waterLinePoints[i].y > floatPoints[i].position.y)
            {
                pointUnderWater = true;

            }
        }

        var waterLineDelta = newWaterLine - waterLine;
        newWaterLine = waterLine;
	}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (floatPoints == null)
        {
            return;
        }

        for(int i = 0; i < floatPoints.Length; i++)
        {
            if(floatPoints[i] == null)
            {
                continue;
            }

            if(waterPhysics != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawCube(waterLinePoints[i], Vector3.one * 0.3f);
            }

            Gizmos.color = Color.green;
            Gizmos.DrawSphere(floatPoints[i].position, 0.1f);
        }

        if (Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(new Vector3(center.x, waterLine, center.z), Vector3.one * 1f);
        }
    }
}






//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Ditzelgames;

//public class WaterObjects : MonoBehaviour
//{

//    public float airDrag = 1;
//    public float waterDrag = 10;
//    public Transform[] floatPoints;

//    protected new Rigidbody rigidbody;
//    protected WaterPhysics waterPhysics;

//    protected float waterLine;
//    protected Vector3[] waterLinePoints;

//    protected Vector3 centreOffset;
//    public Vector3 center { get { return transform.position = centreOffset; } }

//    // Use this for initialization
//    void Awake()
//    {
//        waterPhysics = FindObjectOfType<WaterPhysics>();
//        rigidbody = GetComponent<Rigidbody>();
//        //rigidbody.useGravity = false;

//        waterLinePoints = new Vector3[floatPoints.Length];
//        for (int i = 0; i < floatPoints.Length; i++)
//        {
//            waterLinePoints[i] = floatPoints[i].position;
//            centreOffset = PhysicsHelper.GetCenter(waterLinePoints) - transform.position;
//        }
//    }

//    // Update is called once per frame
//    void FixedUpdate()
//    {
//        var newWaterLine = 0f;
//        var pointUnderWater = false;

//        for (int i = 0; i < floatPoints.Length; i++)
//        {
//            waterLinePoints[i] = floatPoints[i].position;
//            //waterLinePoints[i].y = waterPhysics.GetHeight(floatPoints[i].position);

//            newWaterLine += waterLinePoints[i].y / floatPoints.Length;
//            if (waterLinePoints[i].y > floatPoints[i].position.y)
//                pointUnderWater = true;
//        }

//        var waterLineDelta = newWaterLine - waterLine;
//        newWaterLine = waterLine;
//    }

//    private void OnDrawGizmos()
//    {
//        Gizmos.color = Color.green;
//        if (floatPoints == null)
//        {
//            return;
//        }

//        for (int i = 0; i < floatPoints.Length; i++)
//        {
//            if (floatPoints[i] == null)
//            {
//                continue;
//            }

//            if (waterPhysics != null)
//            {
//                Gizmos.color = Color.red;
//                Gizmos.DrawCube(waterLinePoints[i], Vector3.one * 0.3f);
//            }

//            Gizmos.color = Color.green;
//            Gizmos.DrawSphere(floatPoints[i].position, 0.1f);
//        }

//        if (Application.isPlaying)
//        {
//            Gizmos.color = Color.red;
//            Gizmos.DrawCube(new Vector3(center.x, waterLine, center.z), Vector3.one * 1f);
//        }
//    }
//}
