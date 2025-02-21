using System.Collections;
using NUnit.Framework.Constraints;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float speed;
    [SerializeField] private float speedLimit;
    [SerializeField] private float jumpForce;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashSpeed;

    [SerializeField] private Transform moveIndicator;

    private Rigidbody rb;
    private bool isGrounded;
    private bool isDoubleJumping;
    private bool dashing = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //Adding MovePlayer as a listener to the OnMove event
        inputManager.OnMove.AddListener(MovePlayer);
        rb = GetComponent<Rigidbody>();
        inputManager.OnSpacePressed.AddListener(Jump);
        inputManager.OnDash.AddListener(direction => StartCoroutine(DashPlayer(direction)));
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


        if (direction.magnitude > 0) {
            // Calculate the desired force based on the direction the player is facing
            Vector3 desiredDirectionZ = moveIndicator.forward * direction.z * speed;
            Vector3 desiredDirectionX = moveIndicator.right * direction.x * speed;


            // Apply the force to the rigidbody in the desired direction
            rb.AddForce(desiredDirectionZ);
            rb.AddForce(desiredDirectionX);
            //make sure the player is facing the right direction
            // Limit the speed by clamping the velocity directly
            if ((rb.linearVelocity.magnitude > speedLimit) && !dashing)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * speedLimit;
            }
        } else {    // If the player is not moving, stop the player immediately to make it responsive 
            if (!dashing)
                rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }

    }
    private IEnumerator DashPlayer(Vector3 direction)
    {
        // Vector3 moveDirection = new(direction.x, direction.y, direction.z);
        // rb.AddForce(speed * moveIndicator.forward * moveDirection.z);
        // rb.AddForce(speed * moveIndicator.right * moveDirection.x);

            dashing = true;
            // Calculate the desired force based on the direction the player is facing
            Vector3 desiredDirectionZ = moveIndicator.forward * direction.z * dashSpeed;
            Vector3 desiredDirectionX = moveIndicator.right * direction.x * dashSpeed;


            // Apply the force to the rigidbody in the desired direction
            rb.AddForce(desiredDirectionZ, ForceMode.Impulse);
            rb.AddForce(desiredDirectionX, ForceMode.Impulse);
            //make sure the player is facing the right direction
            yield return new WaitForSeconds(dashDuration);

            Debug.Log("Dash ended");
            dashing = false;
            // rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);


    }
    private void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
        else if (!isDoubleJumping)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isDoubleJumping = true;
        }
        
    }
    // Check if the player is on the ground
    private void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.CompareTag("Ground"))
    {
        isGrounded = true;
        isDoubleJumping = false;
    }
}
}
