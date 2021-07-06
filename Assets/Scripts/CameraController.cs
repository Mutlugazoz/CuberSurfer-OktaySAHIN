using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class CameraController : MonoBehaviour
{

    public PathCreator path;
    public float xDistanceToPath = 4;
    public float fixedY = 5.22f;

    // Update is called once per frame
    private void LateUpdate()
    {
        Vector3 closestPointOnPath = path.path.GetClosestPointOnPath(transform.parent.position);

        Vector3 localClosestPointOnPath = transform.parent.InverseTransformPoint(closestPointOnPath);

        transform.localPosition = new Vector3( xDistanceToPath + localClosestPointOnPath.x, transform.localPosition.y, transform.localPosition.z);

        transform.position = new Vector3(transform.position.x, fixedY, transform.position.z);
    }
}
