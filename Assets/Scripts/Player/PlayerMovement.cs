using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller; 
    private Vector2 moveinput;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 6f; 
    [SerializeField] private float jumpHeight = 2f;
    private float gravity = -9.81f;

    public Vector3 velocity; 
    private bool isJumping = false;
    private bool isGrounded;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveinput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            isJumping = true;
        }
    }


    void Update()
    {
        isGrounded = controller.isGrounded;

        // check if the player is grounded
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Player movement
        Vector3 move = transform.right * moveinput.x + transform.forward * moveinput.y;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // check if the player jumps
        if(isJumping && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * gravity * -2f);
            isJumping = false;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
