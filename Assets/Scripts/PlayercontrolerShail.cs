using UnityEngine;

public class PlayercontrolerShail : MonoBehaviour
{
    // public Variables
    private float maxSpeed = 18f;    // car speed
    public float currentSpeed = 0f; // current speed to start at 0
    public float accelerationTime = 0.5f; // time taken to reach max speed 
    private float decelerationRate = 2.5f; // deceleration rate
    public float brakeRate = 15f; // car brakes
    public int turnSpeed = 75;  // car turnspeed
    public float KPH;               // speed of the car in kph
    private float horizontalInput; // horizontal input
    private float forwardInput; // forward input
    private Rigidbody carRb; //  rigid body component on car

    // starting position for when car falls off track
    private Vector3 startingPosition;

    [Header("Audio Reference")]
    public engineaudio engineAudioScript; // this is the engine audio script

    private void Start()
    {
        // take note of car postion at start
        startingPosition = transform.position;

        // engine audio script component
        engineAudioScript = GetComponentInChildren<engineaudio>();

        // rigidbody
        carRb = GetComponent<Rigidbody>();
        carRb.centerOfMass = new Vector3(0, -0.5f, 0); // mass to keep car on the ground
    }

    void Update()
    {
        //  player inputs
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        // accelerate forward  input
        if (forwardInput > 0)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed, Time.deltaTime * accelerationTime);
        }
        else if (currentSpeed > 0) // decelerate car if no input
        {
            currentSpeed -= decelerationRate * Time.deltaTime;
            currentSpeed = Mathf.Max(currentSpeed, 0); // speed to not go below 0
        }

        // get brake input for 's' and 'down arrow'
        if (Input.GetKey(KeyCode.S))
        {
            currentSpeed -= brakeRate * Time.deltaTime; // brake rate apply to the current speed
            currentSpeed = Mathf.Max(currentSpeed, 0); // speed doesnt go below 0
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            currentSpeed -= brakeRate * Time.deltaTime; //  brake rate apply to the current speed
            currentSpeed = Mathf.Max(currentSpeed, 0); //  speed doesnt go below 0
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
            transform.Translate(-Vector3.forward * Time.deltaTime * currentSpeed);
        }
    }

    void Steer()
    {
        // make car go left and right
        transform.Rotate(-Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            // Update starting position to  checkpoint position
            startingPosition = other.transform.position;
        }
    }

    //  method for detecting collisions with the terrain
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain")) // if player collides with terrain
        {
            // reset speed to 0 and their position to the starting position
            transform.position = startingPosition;
            currentSpeed = 0;
        }
    }
}
