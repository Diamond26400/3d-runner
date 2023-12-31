using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Movement of the player
    public InputAction movementAction;

    // Animation 
    private Animator PlayerAnim;

    //Audio of the Game
    public AudioClip JumbClip;
    public AudioClip CrashClip;
    private AudioSource PlayerSound;

    // Particle System
    public ParticleSystem ExplosionParticle;
    public ParticleSystem DirtSplater;

    //Jumping mechnisim & rigid
    public float JumpForce;
    private Rigidbody playerRb;
    public float gravityModifier;
    public float doubleJumpForce = 10.0f;

    //bool statement
    public bool DoubleSpeed;
    public bool GameOver = false;
    public bool IsOnGround = true;
    public bool DoubleJumpUsed = false;




    void Start()
    {
        // RigidBody
        playerRb = GetComponent<Rigidbody>();

        //Animator
        PlayerAnim = GetComponent<Animator>();

        //Audio
        PlayerSound = GetComponent<AudioSource>();

        // Enable the input action
        movementAction.Enable();

        // Physics modifier
        Physics.gravity *= gravityModifier;
    }
    void Update()
    {
        //Trigger Movement Action
        if (movementAction.triggered && IsOnGround && !GameOver)
        {
            // Spacebar is pressed
            playerRb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            IsOnGround = false;
            PlayerAnim.SetTrigger("Jump_trig");
            DirtSplater.Stop();
            PlayerSound.PlayOneShot(JumbClip, 1.0f);
        }
        if (movementAction.triggered)
        {
            DoubleSpeed = true;
            PlayerAnim.SetFloat("Speed_Multiplier", 2.0f);
        }
        else if (DoubleSpeed)
        {
            DoubleSpeed = false;
            PlayerAnim.SetFloat("Speed_Multiplier", 1.0f);
        }

    }
    // Collison Statement 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsOnGround = true;
            DirtSplater.Play();
        }
        // End Of Game Statment
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("GameOver!");
            GameOver = true;
            PlayerAnim.SetBool("Death_b", true);
            PlayerAnim.SetInteger("DeathType_int", 1);
            ExplosionParticle.Play();
            DirtSplater.Stop();
            PlayerSound.PlayOneShot(CrashClip, 1.0f);
            DoubleJumpUsed = false;
        }
        else if (movementAction.triggered && !IsOnGround && !DoubleJumpUsed)
        {
            DoubleJumpUsed = true;
            // Movement and animation
            playerRb.AddForce(Vector3.up * doubleJumpForce, ForceMode.Impulse);
            PlayerAnim.Play("Running_Jump", 3, 0f);
            PlayerSound.PlayOneShot(JumbClip, 1.0f);

        }

    }
}




