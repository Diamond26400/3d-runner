using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputAction movementAction;
    private Rigidbody playerRb;
    public float JumpForce;
    public float gravityModifier;
    public bool IsOnGround = true;
    public bool GameOver = false;

    void Start()
    {

        playerRb = GetComponent<Rigidbody>();

        // Enable the input action
        movementAction.Enable();
        Physics.gravity *= gravityModifier;
    }
    void Update()
    {
        if (movementAction.triggered && IsOnGround)
        {
            // Spacebar was pressed
            playerRb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            IsOnGround = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsOnGround = true;
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameOver = true;
            Debug.Log("GameOver!");
        }

    }
}




