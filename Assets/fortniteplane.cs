using UnityEngine;

public class fortnitplane : MonoBehaviour
{
    public float speed = 100.0f; // The speed of the plane.
    public float pitchSpeed = 2.0f; // The speed at which the plane pitches up and down.
    public float rollSpeed = 2.0f; // The speed at which the plane rolls left and right.
    public float yawSpeed = 2.0f; // The speed at which the plane yaws left and right.

    private float pitchInput = 0.0f; // The player's input for pitch.
    private float rollInput = 0.0f; // The player's input for roll.
    private float yawInput = 0.0f; // The player's input for yaw.

    private Rigidbody rb; // The plane's Rigidbody component.

    void Start()
    {
        // Get the plane's Rigidbody component.
        rb = GetComponent<Rigidbody>();

        // Lock the plane's rotation along the x and z axes.
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        // Get input from the player for pitch, roll, and yaw.
        pitchInput = Input.GetAxis("Vertical");
        rollInput = Input.GetAxis("Horizontal");
        yawInput = Input.GetAxis("Yaw");

        // Calculate the pitch, roll, and yaw angles based on the player's input.
        float pitchAngle = pitchInput * pitchSpeed;
        float rollAngle = rollInput * rollSpeed;
        float yawAngle = yawInput * yawSpeed;

        // Rotate the plane based on the pitch, roll, and yaw angles.
        Quaternion pitchRot = Quaternion.AngleAxis(pitchAngle, Vector3.right);
        Quaternion rollRot = Quaternion.AngleAxis(-rollAngle, Vector3.back);
        Quaternion yawRot = Quaternion.AngleAxis(yawAngle, Vector3.up);
        Quaternion rot = yawRot * pitchRot * rollRot;
        rb.MoveRotation(transform.rotation * rot);

        // Move the plane forward based on its speed.
        rb.AddForce(transform.forward * speed, ForceMode.Force);
    }
}
