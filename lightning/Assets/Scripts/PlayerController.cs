using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
   [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    
    private CharacterController controller;
    private Vector3 velocity;
    private float gravity = -9.81f;
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    
    void Update()
    {
        HandleMovement();
        HandleGravity();
    }
    
    void HandleMovement()
    {
        Vector2 input = Vector2.zero;
        
        // Direct keyboard access using new Input System
        var keyboard = Keyboard.current;
        if (keyboard != null)
        {
            if (keyboard.wKey.isPressed) input.y = 1f;
            if (keyboard.sKey.isPressed) input.y = -1f;
            if (keyboard.aKey.isPressed) input.x = -1f;
            if (keyboard.dKey.isPressed) input.x = 1f;
        }
        
        Vector3 direction = new Vector3(input.x, 0f, input.y).normalized;
        
        if (direction.magnitude >= 0.1f)
        {
            controller.Move(direction * moveSpeed * Time.deltaTime);
            
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
    
    void HandleGravity()
    {
        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;
            
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}