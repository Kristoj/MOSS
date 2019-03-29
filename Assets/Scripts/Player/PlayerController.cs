#pragma warning disable 0649
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {
    
    [Header("General Settings")]
    [SerializeField] public float moveSpeed = 3f;                  // Default speed when moving
    private float speed;                                            // Stores the current speed value
    [SerializeField] private float jumpForce = 12f;                 // Velocity applied when player jumps
    [SerializeField] private LayerMask groundMask;                  // List of every layer that is considered to be ground
    [HideInInspector] public Vector3 velocity;                                       // Stores the player velocity

    [Header("Ground Movement")]
    [SerializeField] private float groundAcceleration = 50f;        // Acceleration amount when grounded
    [SerializeField] private float groundDeacceleration = 9f;       // Deacceleration amount when ghrounded

    [Header("Air Movement")]
    [SerializeField] private float airAcceleration = 15f;           // Acceleration amount when airborne
    [SerializeField] private float gravity = 33f;                   // Gravity scale

    // States
    [HideInInspector] public bool isGrounded;                       // Is player on the ground ?

    // Misc
    private CharacterController controller;                         // Character controller for the player

    void Awake() {
        controller = GetComponent<CharacterController>();
        speed = moveSpeed;
    }

    void Update() {
        PlayerInput.Update();       // TODO : This belongs somewhere else!
        isGrounded = IsGrounded;
        CheckInput();
        Accelerate();
        ApplyVelocity();
        Deaccelerate();
        ApplyGravity();
    }

    // Accelerate player based on user input
    void Accelerate() {
        speed = moveSpeed;                                                                              // This should be moved when something that affects movement speed will be implemented
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));   // Get movement input
        Vector3 wishDir = transform.TransformDirection(input);                                          // Get a direction vector from the input
        float acceleration = isGrounded == true ? groundAcceleration : airAcceleration;                 // Get a acceleration value depending whether player is grounded or not
        velocity += wishDir * acceleration * Time.deltaTime;                                            // Add the wishdir to our velocity
        
        // Clamp velocity
        float yVelocity = velocity.y;
        velocity.y = 0;
        velocity = Vector3.ClampMagnitude(velocity, speed);
        velocity.y = yVelocity;
    }

    // Apply velocity to the character controller
    void ApplyVelocity() {
        controller.Move(velocity * Time.deltaTime);
    }

    // Deaccelerates player velocity constantly
    void Deaccelerate() {
        if (isGrounded) {
            velocity = Vector3.Lerp(velocity, Vector3.zero, groundDeacceleration * Time.deltaTime);
        }
    }

    // Apply gravity to the player when airborne
    void ApplyGravity() {
        // If airborne apply gravity
        if (!isGrounded) {
            velocity.y -= gravity * Time.deltaTime;
        } else {
            if (velocity.y < 0) {
                velocity.y = 0;
            }
        }
    }

    void CheckInput() {
        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            Jump();
        }
    }

    // Jumping
    void Jump() {
        velocity.y = jumpForce;
    }

    // Returns True/False depending whether player is grounded
    bool IsGrounded {
        get {
            Ray ray = new Ray(transform.position + new Vector3(0, controller.height * 0.5f), -transform.up);
            if (Physics.SphereCast(ray, controller.radius, controller.center.y - (controller.radius * .5f), groundMask, QueryTriggerInteraction.Ignore)) {
                return true;
            }
            return false;
        }
    }
}
