using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

//https://www.youtube.com/watch?v=13KrnisMf14

public class ParabolaPath : MonoBehaviour
{
    public LineRenderer linerender;

    [SerializeField]
    [Range(5, 50)]
    private int lineSegmentCount = 20;
    [SerializeField]
    private CinemachineVirtualCamera aimCam;

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
                                                velocity.y * stepTimePassed - 1.2f * Physics.gravity.y * stepTimePassed * stepTimePassed,
                                                velocity.z * stepTimePassed);

            Vector3 cameraForward = Vector3.ProjectOnPlane(aimCam.transform.forward, Vector3.up);
            Quaternion rotationToCamera = Quaternion.LookRotation(cameraForward, Vector3.up);

            Vector3 rotateMovement = new Vector3(movementVector.x, 0.0f, movementVector.y); //seems wrong in this context, but changing to movementVector.z breaks it
            rotateMovement = rotationToCamera * rotateMovement;

            movementVector = new Vector3(rotateMovement.x, movementVector.y, -movementVector.z);


            linePointList.Add(-movementVector + startingPoint);

            linerender.positionCount = linePointList.Count;
            linerender.SetPositions(linePointList.ToArray());
        }
    }

    public void HideLine()
    {
        lineSegmentCount = 0;
    }
}
