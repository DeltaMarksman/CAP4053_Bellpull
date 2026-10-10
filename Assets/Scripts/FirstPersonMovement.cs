using UnityEngine;

public class FirstPersonPOV : MonoBehaviour
{

    public float moveSpeed = 4f;
    public float mouseSensitivity = 2f;
    public Camera playerCamera;

    private CharacterController controller;
    private float cameraRotation;
    private float verticalVelocity;
    void Start()
    {
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        cameraRotation -= mouseY;
        cameraRotation = Mathf.Clamp(cameraRotation, -90f, 90f);

        playerCamera.transform.localRotation =
            Quaternion.Euler(cameraRotation, 0, 0);

        // Walk with WASD
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        move = Vector3.ClampMagnitude(move, 1f);

        //  gravity
        if (controller.isGrounded && verticalVelocity < 0)
            verticalVelocity = -2f;

        verticalVelocity += -9.81f * Time.deltaTime;

        controller.Move(
            (move * moveSpeed + Vector3.up * verticalVelocity)
            * Time.deltaTime
        );

        //  mouse with Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // Lock mouse when clicking the game
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
