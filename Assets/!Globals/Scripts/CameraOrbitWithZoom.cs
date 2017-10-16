using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbitWithZoom : MonoBehaviour
{
    public Transform target; // The target point to rotate around
    public float distance = 5.0f; // The distance between the camera's point and the target
    public float sensitivity = 1f; // How sensitive the input is for rotating
    public bool move = false;

    public float distanceMin = .5f; // Minimum allowed distance of camera
    public float distanceMax = 15f; // Maximum allowed distance of camera

    [Header("Camera Collision")]
    public LayerMask ignoreLayers;
    public bool isEnabled = true;
    public float colRadius = 5f;

    // Stored X and Y rotation in eulerAngles
    float x = .0f, y = .0f;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, colRadius);
    }

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
        if (Input.GetMouseButton(1) || move)
        {
            HideCursor(true);
            GetInput();
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
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

            float dist = GetCollisionDistance();

            // Calculate new position offset using rotation
            Vector3 negDistance = new Vector3(.0f, .0f, -dist);
            Vector3 position = rotation * negDistance + target.position;

            // Apply rotation and position to transform
            transform.rotation = rotation;
            transform.position = position;
        }
    }

    float GetCollisionDistance()
    {
        float desireDist = distance;
        Quaternion rotation = Quaternion.Euler(y, x, 0);
        Vector3 back = rotation * new Vector3(0, 0, -distance);
        Ray targetRay = new Ray(target.position, back);
        RaycastHit hit;
        if (Physics.SphereCast(targetRay, colRadius, out hit, desireDist, ~ignoreLayers))
        {
            Vector3 point = hit.point + hit.normal * colRadius;
            desireDist = Vector3.Distance(target.position, point);
        }

        return desireDist;
    }
}
