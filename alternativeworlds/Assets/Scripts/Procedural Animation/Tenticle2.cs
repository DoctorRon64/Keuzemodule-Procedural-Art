using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tenticle2 : MonoBehaviour
{
    [SerializeField] private int length = 5;
    [SerializeField] private LineRenderer lnRnder;
    private Vector3[] segmentPoses;
    private Vector3[] segmentV;

    [SerializeField] private Transform targetDir;
    [SerializeField] private float targetDistance = 20f;
    [SerializeField] private float smoothSpeed = 20f;
    
    [SerializeField] private float wiggleSpeed = 20f;
    [SerializeField] private float wiggleMagnitude = 20f;
    [SerializeField] private Transform wiggleDir;

    [SerializeField] private List<Transform> bodyParts;
    [SerializeField] private GameObject bodyPartPrefab;
    [SerializeField] private int numBodyParts = 5;
    [SerializeField] private BodyRotation bodyRotationScript; 
    
    private void Awake()
    {
        lnRnder.positionCount = length;
        segmentPoses = new Vector3[length];
        segmentV = new Vector3[length];

        bodyParts.Clear();

        for (int i = 0; i < length; i++)
        {
            segmentPoses[i] = targetDir.position - i * targetDir.right * targetDistance;

            if (i <= 0) continue;
            if (i - 1 < bodyParts.Count)
            {
                bodyParts[i - 1].position = segmentPoses[i];
            }
            else
            {
                GameObject newBodyPart = Instantiate(bodyPartPrefab, transform);
                bodyParts.Add(newBodyPart.transform);
                
                BodyRotation bodyRotationScript = newBodyPart.GetComponent<BodyRotation>();
                if (bodyRotationScript == null) continue;
                if (i == 1)
                {
                    bodyRotationScript.target = targetDir;
                }
                else
                {
                    bodyRotationScript.target = bodyParts[i - 2];
                }
            }
        }
    }

    private void Update()
    {
        wiggleDir.localRotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.time * wiggleSpeed) * wiggleMagnitude);

        segmentPoses[0] = targetDir.position;

        for (int i = 1; i < segmentPoses.Length; i++)
        {
            Vector3 targetPos = segmentPoses[i - 1] + (segmentPoses[i] - segmentPoses[i - 1]).normalized * targetDistance;
            segmentPoses[i] = Vector3.SmoothDamp(segmentPoses[i], targetPos, ref segmentV[i], smoothSpeed);
            bodyParts[i - 1].transform.position = segmentPoses[i];
        }

        lnRnder.SetPositions(segmentPoses);
    }
}
