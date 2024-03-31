using System;
using UnityEngine;

public class RotateToTarget : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 15f; // Increased rotation speed
    [SerializeField] private float moveSpeed = 3f;

    private void Update()
    {
        RotateTowardsMouse();
        MoveTowardsMouse();
    }

    private void RotateTowardsMouse()
    {
        Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = cursorPos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Debug angle
        Debug.Log("Angle: " + angle);

        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime); // Used Quaternion.Lerp
    }

    private void MoveTowardsMouse()
    {
        Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = Vector2.MoveTowards(transform.position, cursorPos, moveSpeed * Time.deltaTime);
    }
}