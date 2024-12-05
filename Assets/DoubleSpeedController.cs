using UnityEngine;

public class DoubleSpeedController : MonoBehaviour
{
    public float normalSpeed = 2f;  // Normal movement speed
    public float boostedSpeed = 4f;  // Speed when joystick is pressed
    private float currentSpeed;

    public OVRInput.Controller leftController = OVRInput.Controller.LTouch; // Left controller

    private CharacterController characterController;

    void Start()
    {
        // Set the initial speed to normal
        currentSpeed = normalSpeed;

        // Try to get the CharacterController if one is attached
        characterController = GetComponent<CharacterController>();
        if (characterController == null)
        {
            Debug.LogError("No CharacterController found! Please attach one to this GameObject.");
        }

        Debug.Log("Script Initialized. Normal Speed: " + normalSpeed);
    }

    void Update()
    {
        // Check if the joystick button is being pressed on the left controller
        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstick, leftController))
        {
            Debug.Log("Joystick Button Pressed on Left Controller");
            currentSpeed = boostedSpeed;
        }
        else
        {
            Debug.Log("Joystick Button Not Pressed. Returning to Normal Speed: " + normalSpeed);
            currentSpeed = normalSpeed;
        }

        // Get joystick movement input
        Vector2 input = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, leftController);
        if (input != Vector2.zero)
        {
            Debug.Log("Joystick Movement Detected: X=" + input.x + ", Y=" + input.y);
        }

        // Apply movement using the CharacterController
        Vector3 moveDirection = new Vector3(input.x, 0, input.y).normalized; // Normalize to prevent faster diagonal movement
        if (moveDirection != Vector3.zero)
        {
            Debug.Log("Moving. Speed: " + currentSpeed + ", Direction: " + moveDirection);
        }

        if (characterController != null)
        {
            characterController.Move(moveDirection * currentSpeed * Time.deltaTime);
        }
        else
        {
            // Fallback if no CharacterController is attached
            transform.Translate(moveDirection * currentSpeed * Time.deltaTime, Space.Self);
        }
    }
}