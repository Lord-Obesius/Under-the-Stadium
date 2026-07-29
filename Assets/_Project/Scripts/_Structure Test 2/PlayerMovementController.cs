using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Look")]
    public float mouseSensitivity = 0.1f;
    public bool canLookAround = true;
    public bool canMove = true;

    public Transform playerCamera;


    Rigidbody rb;

    PlayerControls controls;

    Vector2 moveInput;
    Vector2 lookInput;

    float cameraRotation = 0f;


    void Awake()
    {
        controls = new PlayerControls();

        controls.Player.Move.performed += ctx =>
            moveInput = ctx.ReadValue<Vector2>();

        controls.Player.Move.canceled += ctx =>
            moveInput = Vector2.zero;


        controls.Player.Look.performed += ctx =>
            lookInput = ctx.ReadValue<Vector2>();

        controls.Player.Look.canceled += ctx =>
            lookInput = Vector2.zero;
    }


    void OnEnable()
    {
        controls.Enable();
    }


    void OnDisable()
    {
        controls.Disable();
    }


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {
        Look();
    }


    void FixedUpdate()
    {
        Move();
    }


    void Move()
    {
        if (!canMove)
            return;

        Vector3 movement =
            transform.right * moveInput.x +
            transform.forward * moveInput.y;


        movement *= moveSpeed;


        Vector3 velocity = rb.linearVelocity;


        rb.linearVelocity = new Vector3(
            movement.x,
            velocity.y,
            movement.z
        );
    }


    void Look()
    {
        if (!canLookAround)
            return;

        float mouseX =
            lookInput.x * mouseSensitivity;

        float mouseY =
            lookInput.y * mouseSensitivity;


        cameraRotation -= mouseY;

        cameraRotation =
            Mathf.Clamp(cameraRotation, -90f, 90f);


        playerCamera.localRotation =
            Quaternion.Euler(
                cameraRotation,
                0f,
                0f
            );


        transform.Rotate(
            Vector3.up * mouseX
        );
    }
}