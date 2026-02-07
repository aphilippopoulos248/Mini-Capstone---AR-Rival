using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class DragonController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private FixedJoystick fixedJoystickHorizontal;  // Joystick for horizontal movement
    [SerializeField] private FixedJoystick fixedJoystickVertical;    // Joystick for vertical movement

    private Rigidbody rigidBody;

    public Rigidbody Rigidbody { get => rigidBody; set => rigidBody = value; }

    private void FixedUpdate()
    {
        if (rigidBody == null) return;

        // Get input from the horizontal joystick (X and Z axes)
        float xVal = fixedJoystickHorizontal.Horizontal;
        float zVal = fixedJoystickHorizontal.Vertical;

        // Get input from the vertical joystick (Y axis)
        float yVal = fixedJoystickVertical.Vertical;

        // Combine into a 3D movement vector
        Vector3 movement = new Vector3(xVal, yVal, zVal);

        // Apply movement based on the speed
        rigidBody.linearVelocity = movement * speed;

        // Rotate the dragon based on the horizontal joystick
        if (xVal != 0 || zVal != 0)
            rigidBody.transform.eulerAngles = new Vector3(
                transform.eulerAngles.x,
                Mathf.Atan2(xVal, zVal) * Mathf.Rad2Deg, // Horizontal rotation based on joystick
                transform.eulerAngles.z
            );
    }
}
