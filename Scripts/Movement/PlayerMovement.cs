using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController characterController;
    GameManager manager;
    public Transform cameraTransform;
    public float pitch = 0f;

    [SerializeField] private float speed = 7f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 2f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private AudioSource audioSource;
    [SerializeField] private AudioClip waterSound;
    [SerializeField] private AudioClip lavaSound;
    [SerializeField] private AudioClip spikeSound;
    [SerializeField] private AudioClip boulderSound;

    private Vector3 velocity;
    public bool isGrounded;
    [SerializeField] private float distance;
    public float hitNormal;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
        manager = GameManager.GetInstance;

        characterController.enabled = false;

        transform.position = manager.startingLocation;
        transform.rotation = manager.startingRotation;

        characterController.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;

        distance = characterController.radius + 0.2f;
    }

    void Update()
    {
        MovePlayer();
        Look();
    }

    void MovePlayer()
    {
        RaycastHit hit;
        Vector3 p1 = transform.position + Vector3.up * 0.1f;
        Vector3 p2 = p1 + Vector3.up * 0.4f;

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move = Vector3.ClampMagnitude(move, 1f);
        move = transform.TransformDirection(move * Time.deltaTime);
        characterController.Move(move * speed);

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        else if (!isGrounded)
        {
            //Check around the character in a 360, 10 times (increase if more accuracy is needed)
            for (int i = 0; i < 360; i += 36)
                {
                    //Check if anything with the platform layer touches this object
                if (Physics.CapsuleCast(p1, p2, 0, new Vector3(Mathf.Cos(i), 0, Mathf.Sin(i)), out hit, distance, groundMask))
                {
                    //If the object is touched by a platform, move the object away from it
                    characterController.Move(hit.normal * (distance - hit.distance));
                }
            }
        }            
    }

    void Look()
    {
        if (!manager.gamePaused)
        {
            float mousex = Input.GetAxis("Mouse X") * 3f;
            transform.Rotate(0, mousex, 0);

            pitch -= Input.GetAxis("Mouse Y") * 3f;
            pitch = Mathf.Clamp(pitch, -90f, 90f);
            cameraTransform.localRotation = Quaternion.Euler(pitch, 0, 0);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Water") || hit.gameObject.CompareTag("Lava") || hit.gameObject.CompareTag("Spike") || hit.gameObject.CompareTag("Boulder"))
        {
            if (hit.gameObject.tag == "Water") { audioSource.clip = waterSound; }
            else if (hit.gameObject.tag == "Lava") { audioSource.clip = lavaSound; }
            else if (hit.gameObject.tag == "Spike") { audioSource.clip = spikeSound; }
            else if (hit.gameObject.tag == "Boulder") { audioSource.clip = boulderSound; }

            audioSource.Play();
            transform.parent = null;
            characterController.enabled = false;
            characterController.transform.position = manager.startingLocation;
            characterController.transform.rotation = manager.startingRotation;
            characterController.enabled = true;
        }
    }

    public void setPlayerVelocity(float velocity)
    {
        this.velocity.y = velocity;
    }
}