using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement")] [SerializeField]
    private float playerJump;

    [SerializeField] private float playerSpeed;

    [SerializeField] private Transform orientation;

    [SerializeField] private float groundDrag;


    [Header("Ground Check")] [SerializeField]
    private float playerHeight;

    [SerializeField] private LayerMask whatIsGround;

    private bool grounded;
    private float horizontal;

    private Vector3 moveDirection;

    private Rigidbody rb;

    private float vertical;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        SpeedControl();
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        moveDirection = orientation.forward * vertical + orientation.right * horizontal;

        rb.AddForce(moveDirection.normalized * playerSpeed * 10f, ForceMode.Force);
    }


    private void SpeedControl()
    {
        var flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > playerSpeed)
        {
            var limitedVel = flatVel.normalized * playerSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
}