using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.youtube.com/watch?v=13KrnisMf14

public class ParabolaPath : MonoBehaviour
{
    private LineRenderer linerender;

    [SerializeField]
    [Range(5, 50)]
    private int lineSegmentCount = 20;

    private List<Vector3> linePointList = new List<Vector3>();

    #region Singleton

    public static ParabolaPath Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    public void UpdateTrajectory(Vector3 force, Rigidbody rb, Vector3 startingPoint)
    {
        Vector3 velocity = (force / rb.mass) * Time.fixedDeltaTime;
        float flightTime = (2 * velocity.y) / Physics.gravity.y;
        float stepTime = flightTime / lineSegmentCount;

        linePointList.Clear();

        for(int i = 0; i < lineSegmentCount; i++)
        {
            float stepTimePassed = stepTime * i;

            Vector3 movementVector = new Vector3(velocity.x * stepTimePassed,
                                                velocity.y * stepTimePassed - .5f * Physics.gravity.y * stepTimePassed * stepTimePassed,
                                                velocity.z * stepTimePassed);
            RaycastHit hit;
            if(Physics.Raycast(startingPoint, -movementVector, out hit, movementVector.magnitude))
            {
                break;
            }
            linePointList.Add(-movementVector + startingPoint);

            linerender.positionCount = linePointList.Count;
            linerender.SetPositions(linePointList.ToArray());
        }
    }
}
