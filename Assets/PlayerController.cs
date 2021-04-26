using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent( typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    PlayerInput playerInput;
    Rigidbody2D rigidbody;

    public float jogSpeed = 5;
    public float sprintSpeed = 7.5f;
    public float acceleration = 1;

    private Vector2 moveInput;
    private bool isSprinting;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        ConfigureInputs();
    }

    void FixedUpdate()
    {
        ApplyInput();
    }

    public void ConfigureInputs()
    {
        playerInput = new PlayerInput();

        playerInput.Player.Enable();
        playerInput.Player.Move.performed += Move;
        playerInput.Player.Move.canceled += Move;
        playerInput.Player.Sprint.started += Sprint;
        playerInput.Player.Sprint.canceled += Sprint;
        playerInput.Player.Look.performed += Look;
        playerInput.Player.Look.canceled += Look;
        playerInput.Player.Throw.performed += Throw;
        playerInput.Player.Throw.canceled += Throw;
    }

    private void Move(InputAction.CallbackContext _context)
    {
        moveInput = _context.ReadValue<Vector2>();
    }

    private void Sprint(InputAction.CallbackContext _context)
    {
        isSprinting = _context.ReadValue<float>() > 0.5f;
    }

    private void Look(InputAction.CallbackContext _context)
    {
    }

    private void Throw(InputAction.CallbackContext _context)
    {
    }
    
    private void ApplyInput()
    {
        // TODO: Find vector to add in order to reach max speed
        // rigidbody.velocity = (rigidbody.velocity + moveInput).magnitude < maxSpeed ? rigidbody.velocity + moveInput : maxSpeed;

        float maxSpeed = isSprinting ? sprintSpeed : jogSpeed;
        if ((rigidbody.velocity + moveInput).magnitude < maxSpeed)
        {
            rigidbody.velocity += moveInput * acceleration;
        }
    }
}
