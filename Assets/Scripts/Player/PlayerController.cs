using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerStats playerStats;
    private CharacterController characterController;
    private Rigidbody playerRb;

    private Vector2 move;
    private Vector3 moveDirection;
    private float verticalVelocity;
    private float currentVelocity;

    [Header("Camera")]
    [SerializeField] private Transform camTransform;

    [Header("Movement")]
    [SerializeField] private float smoothTime = 0.05f; // Camera movement

    [Header("Gravity")]
    [SerializeField] private float gravityMulti = 1f;

    public InputAction moveAction;
    public InputAction jumpAction;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerRb = GetComponent<Rigidbody>();
        playerStats = GetComponent<PlayerStats>();
    }

    void Start()
    {
        playerRb.isKinematic = true; // Used to toggle ragdoll (set to true unless conditions meet for player to ragdoll)
    }

    void Update()
    {
        move = moveAction.ReadValue<Vector2>();

        HandleRotation();
        HandleMovement();
        HandleGravity();
    }

    void HandleRotation()
    {
        if (move.sqrMagnitude > 0.01f)
        {
            // Find angle relative to camera
            float targetAngle = Mathf.Atan2(move.x, move.y) * Mathf.Rad2Deg + camTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, smoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
    }

    void HandleMovement()
    {
        // Movement relative to camera direction
        Vector3 forward = new Vector3(camTransform.forward.x, 0, camTransform.forward.z).normalized;
        Vector3 right = new Vector3(camTransform.right.x, 0, camTransform.right.z).normalized;

        moveDirection = (forward * move.y + right * move.x).normalized;
    }

    void HandleGravity()
    {
        if (characterController.isGrounded)
        {
            if (verticalVelocity < 0)
                verticalVelocity = -1f;

            if (jumpAction.triggered)
                verticalVelocity = playerStats.jumpStrength;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * gravityMulti * Time.deltaTime;
        }

        // Apply movement and gravity
        Vector3 velocity = moveDirection * playerStats.moveSpeed;
        velocity.y = verticalVelocity;

        characterController.Move(velocity * Time.deltaTime);
    }

    void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
    }

    void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
    }

    public void EnableRagdoll()
    {
        characterController.enabled = false;
        playerRb.isKinematic = false;
    }

    public void DisableRagdoll()
    {
        characterController.enabled = true;
        playerRb.isKinematic = true;
    }
}

