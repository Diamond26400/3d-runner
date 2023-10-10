using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputAction movementAction;
    private Rigidbody playerRb;
    private Animator PlayerAnim;
    public ParticleSystem ExplosionParticle;
    public ParticleSystem DirtSplater;
    public float JumpForce;
    public float gravityModifier;
    public bool IsOnGround = true;
    public bool GameOver = false;


    void Start()
    {

        playerRb = GetComponent<Rigidbody>();
        PlayerAnim = GetComponent<Animator>();
        // Enable the input action
        movementAction.Enable();
        Physics.gravity *= gravityModifier;
    }
    void Update()
    {
        if (movementAction.triggered && IsOnGround && !GameOver)
        {
            // Spacebar was pressed
            playerRb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            IsOnGround = false;
            PlayerAnim.SetTrigger("Jump_trig");
            DirtSplater.Stop();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsOnGround = true;
            DirtSplater.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("GameOver!");
            GameOver = true;
            PlayerAnim.SetBool("Death_b",true);
            PlayerAnim.SetInteger("DeathType_int", 1);
            ExplosionParticle.Play();
            DirtSplater.Stop();
        }

    }
}




