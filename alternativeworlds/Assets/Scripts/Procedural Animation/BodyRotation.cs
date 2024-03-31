using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BodyRotation : MonoBehaviour
{
    public float speed;

    private Vector2 direction;
    public Transform target;

    private void Update()
    {
        direction = target.position - transform.position;
        float angle = MathF.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
    }
}
