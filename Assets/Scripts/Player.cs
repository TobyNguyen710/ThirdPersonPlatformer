using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float speed;
    [SerializeField] private float speedLimit;
    [SerializeField] private float jumpForce;

    [SerializeField] private Transform moveIndicator;

    private Rigidbody rb;
    private bool isGrounded;
    private bool doubleJumpable;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //Adding MovePlayer as a listener to the OnMove event
        inputManager.OnMove.AddListener(MovePlayer);
        rb = GetComponent<Rigidbody>();
        inputManager.OnSpacePressed.AddListener(Jump);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void MovePlayer(Vector3 direction)
    {
        // Vector3 moveDirection = new(direction.x, direction.y, direction.z);
        // rb.AddForce(speed * moveIndicator.forward * moveDirection.z);
        // rb.AddForce(speed * moveIndicator.right * moveDirection.x);


        // Calculate the desired force based on the direction the player is facing
        Vector3 desiredDirectionZ = moveIndicator.forward * direction.z * speed;
        Vector3 desiredDirectionX = moveIndicator.right * direction.x * speed;


        // Apply the force to the rigidbody in the desired direction
        rb.AddForce(desiredDirectionZ);
        rb.AddForce(desiredDirectionX);
        //make sure the player is facing the right direction
        // Limit the speed by clamping the velocity directly
        if (rb.linearVelocity.magnitude > speedLimit)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * speedLimit;
        }
    }
    private void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
        else if (doubleJumpable)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            doubleJumpable = false;
        }
        
    }
    // Check if the player is on the ground
    private void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.CompareTag("Ground"))
    {
        isGrounded = true;
        doubleJumpable = true;
    }
}
}
