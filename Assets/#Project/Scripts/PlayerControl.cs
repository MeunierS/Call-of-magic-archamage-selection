using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    public InputActionAsset actions;
    private InputAction move;
    private InputAction jump;
    public float jumpHeight;
    private InputAction shoot;
    [SerializeField] private float speed;
    private Camera myCamera;
    private Vector3 forward, right;
    //public bool moving { get; private set;}

    void Awake(){
        move = actions.FindActionMap("main").FindAction("Move");
        jump = actions.FindActionMap("main").FindAction("Jump");
        shoot = actions.FindActionMap("main").FindAction("Shoot");
        jump.performed += ctx => {OnJump(ctx);};
        shoot.performed += ctx => {OnShoot(ctx);};
    }
    // Start is called before the first frame update
    void Start()
    {
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

        transform.position += finalMovement * Time.deltaTime * speed;
    }
    void OnEnable()
    {
        actions.FindActionMap("main").Enable();
        jump.Enable();
        shoot.Enable();
    }
    void OnDisable()
    {
        actions.FindActionMap("main").Disable();
        jump.Disable();
        shoot.Disable();
    }
    void OnShoot(InputAction.CallbackContext ctx){
        Debug.Log("Shoot !");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit)){
                if(hit.collider.CompareTag("Bot")){
                    Debug.Log("Bot touched !");
                    //todo shoot mechanic here
                }
            }
    }
    void OnJump(InputAction.CallbackContext ctx){
        Debug.Log("Jump");
        if (transform.position.y > 0.6){
                return;
        }
        Vector3 jumpAction;
        jumpAction.y = jumpHeight;
        jumpAction.x = transform.position.x;
        jumpAction.z = transform.position.z;
        transform.position = jumpAction;
    }
}
