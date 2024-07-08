using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction move;
    private InputAction jump;
    private InputAction shoot;
    [SerializeField] private float speed;
    private CharacterController characterController;
    private Camera myCamera;
    private Vector3 forward, right;
    //public bool moving { get; private set;}

    void Awake()
    {
        jump.performed += OnJump;
        shoot.performed += OnShoot;
    }
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        move = playerInput.actions["Move"];
        jump = playerInput.actions["Jump"];
        shoot = playerInput.actions["Shoot"];
        myCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        forward = myCamera.transform.forward;
        right = myCamera.transform.right;

        forward = Vector3.ProjectOnPlane(forward,Vector3.up).normalized;
        right = Vector3.ProjectOnPlane(right, Vector3.up).normalized;

        Vector2 movement = move.ReadValue<Vector2>();
        //moving = movement != Vector2.zero;
        Vector3 finalMovement = movement.x * right + movement.y * forward;

        characterController.SimpleMove(finalMovement * speed);
    }
    void OnEnable()
    {
        jump.Enable();
        shoot.Enable();
    }
    void OnDisable()
    {
        jump.Disable();
        shoot.Disable();
    }
    void OnShoot(InputAction.CallbackContext ctx){

    }
    void OnJump(InputAction.CallbackContext ctx){

    }
}
