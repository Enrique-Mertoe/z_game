using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMak;
    private Vector3 velocity;
    private bool isGounded;
    private bool isMoving;
    public Vector3 latPosition = new Vector3(0f, 0f, 0f);
    
    public AudioSource footsStepsAudio;
    public AudioClip[] footstepSounds;
    public float footstepInterval = 0.5f;
    private float footstepTimer;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        footsStepsAudio = GetComponent<AudioSource>();
    }


    void Update()
    {
        isGounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMak);

        if (isGounded && velocity.y < 0)
            velocity.y = -2f;
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");

        var move = transform.right * x + transform.forward * z;
        controller.Move(move * (speed * Time.deltaTime));
        if (Input.GetButtonDown("Jump") && isGounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        if (latPosition != gameObject.transform.position && isGounded)
        {
            isMoving = true;
            // SoundManager.Instance.walkingSound.Play();
            footstepTimer -= Time.deltaTime;
            if (footstepTimer <= 0)
            {
                PlayFootstepSound();
                footstepTimer = footstepInterval;
            }
        }
        else
            isMoving = false;

        latPosition = gameObject.transform.position;

    }
    void PlayFootstepSound()
    {
        if (footstepSounds.Length > 0)
        {
            int index = Random.Range(0, footstepSounds.Length);
            footsStepsAudio.PlayOneShot(footstepSounds[index]);
        }
    }
}
