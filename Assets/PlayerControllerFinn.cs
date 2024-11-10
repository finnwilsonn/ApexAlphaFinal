using UnityEngine;

public class PlayerControllerFinn : MonoBehaviour
{
    // public Variables
    private float maxSpeed = 15f;    // car speed
    public float currentSpeed = 0f; // make sure current speed starts from 0
    public float accelerationTime = 0.5f; // time it takes to reach max speed (seconds)
    private float decelerationRate = 2.5f; // rate at which the car decelerates
    public float brakeRate = 15f; // how quick car brakes
    public int turnSpeed = 75;  // how quick the car turns 
    public float KPH;               // speed of the car in kilometers per hour
    private float horizontalInput;
    private float forwardInput;
    private Rigidbody carRb; // mention to rigid body component on car

    // starting position for when car falls off track
    private Vector3 startingPosition;

    [Header("Audio Reference")]
    public engineaudio engineAudioScript; // this is the engine audio script

    private void Start()
    {
        // take note of the position the car is in at the start of the game
        startingPosition = transform.position;

        // get engine audio script component
        engineAudioScript = GetComponentInChildren<engineaudio>();

        // rigidbody
        carRb = GetComponent<Rigidbody>();
        carRb.centerOfMass = new Vector3(0, -0.5f, 0); // change the center of mass to keep car on the ground
    }

    void Update()
    {
        // get player inputs
        horizontalInput = -Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        // accelerate forward if there is input
        if (forwardInput > 0)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed, Time.deltaTime * accelerationTime);
        }
        else if (currentSpeed > 0) // when there is no input, decelerate car 
        {
            currentSpeed -= decelerationRate * Time.deltaTime;
            currentSpeed = Mathf.Max(currentSpeed, 0); // ensure speed of car stays above 0
        }

        // get brake input for 's' and 'down arrow'
        if (Input.GetKey(KeyCode.S))
        {
            currentSpeed -= brakeRate * Time.deltaTime; // make the brake rate apply to the current speed
            currentSpeed = Mathf.Max(currentSpeed, 0); // make sure speed doesn't go below 0
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            currentSpeed -= brakeRate * Time.deltaTime; // make the brake rate apply to the current speed
            currentSpeed = Mathf.Max(currentSpeed, 0); // make sure speed doesn't go below 0
        }

        // convert the current speed
        KPH = currentSpeed * 5f;

        // pass on this KPH to the engine audio script so it can be used to determine the pitch
        engineAudioScript.engineSpeed = Mathf.Lerp(
            engineAudioScript.minimumPitch,
            engineAudioScript.maximumPitch,
            KPH / 100f
        );

        // call move and steer methods
        Move();
        Steer();
    }

    void Move()
    {
        // move the car forward 
        if (currentSpeed > 0) // only move the car forward when the speed is more than 0
        {
            transform.Translate(Vector3.right * Time.deltaTime * currentSpeed);
        }
    }

    void Steer()
    {
        // make car rotate left and right
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            // Update starting position to this checkpoint's position
            startingPosition = other.transform.position;
        }
    }

    // this method is for detecting collisions with the terrain
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain")) // if player collides with the terrain
        {
            // reset their speed to 0 and their position to the starting position
            transform.position = startingPosition;
            currentSpeed = 0;
        }
    }
}
