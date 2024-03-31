using System;
using UnityEngine;

public class Tenticle : MonoBehaviour
{
    [SerializeField] private int length = 5;
    [SerializeField] private LineRenderer lnRnder;
    private Vector3[] segmentPoses;
    private Vector3[] segmentV;

    [SerializeField] private Transform targetDir;
    [SerializeField] private float targetDistance = 20f;
    [SerializeField] private float smoothSpeed = 20f;
    [SerializeField] private float trailSpeed = 20f;
    
    [SerializeField] private float wiggleSpeed = 20f;
    [SerializeField] private float wiggleMagnitude = 20f;
    [SerializeField] private Transform wiggleDir;
    
    private void Awake()
    {
        lnRnder.positionCount = length;
        segmentPoses = new Vector3[length];
        segmentV = new Vector3[length];

        for (int i = 0; i < length; i++)
        {
            segmentPoses[i] = targetDir.position - i * targetDir.right * targetDistance;
        }
    }

    private void Update()
    {
        wiggleDir.localRotation = Quaternion.Euler(0, 0, MathF.Sin(Time.time * wiggleSpeed) * wiggleMagnitude);
        
        segmentPoses[0] = targetDir.position;

        for (int i = 1; i < segmentPoses.Length; i++)
        {
            segmentPoses[i] = Vector3.SmoothDamp(segmentPoses[i], segmentPoses[i - 1] + targetDir.right * targetDistance, ref segmentV[i], smoothSpeed + i / trailSpeed);
        }

        lnRnder.SetPositions(segmentPoses);
    }
}