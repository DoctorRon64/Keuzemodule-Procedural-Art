using System;
using UnityEngine;

public class RotateToTarget : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 15f;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float stopDistance = 1f;

    private void Update()
    {
        RotateTowardsMouse();
        MoveTowardsMouse();
    }

    private void RotateTowardsMouse()
    {
        if (isPlayerNotToClose()) return;
        
        Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = cursorPos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime); // Used Quaternion.Lerp
    }

    private void MoveTowardsMouse()
    {
        if (isPlayerNotToClose()) return;

        Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = Vector2.MoveTowards(transform.position, cursorPos, moveSpeed * Time.deltaTime);
    }

    private bool isPlayerNotToClose()
    {
        Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursorPos.z = 0;
        float distanceToMouse = Vector2.Distance(transform.position, cursorPos);
        return distanceToMouse <= stopDistance;
    }
}