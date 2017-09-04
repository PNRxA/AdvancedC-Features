using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbitWithZoom : MonoBehaviour
{
    public Transform target; // The target point to rotate around
    public float distance = 5.0f; // The distance between the camera's point and the target
    public float sensitivity = 1f; // How sensitive the input is for rotating

    public float distanceMin = .5f; // Minimum allowed distance of camera
    public float distanceMax = 15f; // Maximum allowed distance of camera

    // Stored X and Y rotation in eulerAngles
    float x = .0f, y = .0f;

    // Use this for initialization
    void Start()
    {
        // Get the current axis of rotation on X and Y
        Vector3 angles = transform.eulerAngles;
        // Swap over X and Y because of axis
        x = angles.y;
        y = angles.x;
    }

    void Update()
    {
        // Get mouse scrollwheel input offset for changing distance
        float inputScroll = Input.GetAxis("Mouse ScrollWheel");
        distance = Mathf.Clamp(distance - inputScroll, distanceMin, distanceMax);
        Movement();
    }

    void LateUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            HideCursor(true);
            GetInput();
        }
        else
        {
            HideCursor(false);
        }
    }

    void HideCursor(bool isHiding)
    {
        // Is the cursor supposed to be hiding
        if (isHiding)
        {
            // Hide the cursor
            Cursor.visible = false;
        }
        else
        {
            // Unhide the cursor
            Cursor.visible = true;
        }
    }

    void GetInput()
    {
        // Gather X and Y mouse offset input to rotate camera (by sensitivity)
        x += Input.GetAxis("Mouse X") * sensitivity;
        // Opposite direction for Y because it is inverted
        y -= Input.GetAxis("Mouse Y") * sensitivity;
    }

    void Movement()
    {
        // Check if a target has been set
        if (target)
        {
            // Coonvert x and y rotation to Quaternion using euler
            Quaternion rotation = Quaternion.Euler(y, x, 0);

            // Calculate new position offset using rotation
            Vector3 negDistance = new Vector3(.0f, .0f, -distance);
            Vector3 position = rotation * negDistance + target.position;

            // Apply rotation and position to transform
            transform.rotation = rotation;
            transform.position = position;
        }
    }
}
