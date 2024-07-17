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
    public Projectile projectile;
    [SerializeField] private float speed;
    private Camera myCamera;
    private Vector3 forward, right;
    public float cooldown = 0.5f;
    public float respawnTimer = 2.1f;
    private Rigidbody rb;
    public bool allowedToMove = true;
    public bool isRespawning = false;
    public Transform spawnpoints;
    [HideInInspector]public int scoreTarget = 6;
    [HideInInspector]public int personnalKill = 0;
    [HideInInspector]public int personnalDeath = 0;
    private Animator animator;
    private bool isOnWall;

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
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //rotate player with camera
        Quaternion modelRotation = new Quaternion (0, myCamera.transform.rotation.y, 0, myCamera.transform.rotation.w);
        transform.rotation = modelRotation;

        //shoot cooldown
        if (cooldown > 0){
            cooldown -= Time.deltaTime;
        }
        
        //movement
        if (allowedToMove){
            forward = myCamera.transform.forward;
            right = myCamera.transform.right;

            forward = Vector3.ProjectOnPlane(forward,Vector3.up).normalized;
            right = Vector3.ProjectOnPlane(right, Vector3.up).normalized;

            Vector2 movement = move.ReadValue<Vector2>();
            if(movement != null){
                animator.SetBool("isMoving", true);
            }
            else{
                animator.SetBool("isMoving", false);
            }

            Vector3 finalMovement = movement.x * right * speed + movement.y * forward * speed + rb.velocity.y * Vector3.up;
            rb.velocity = finalMovement;//  * speed;
        }
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
        if (cooldown <= 0){
            cooldown = 0.5f;
            Projectile instantiatedProjectile = Instantiate(projectile, transform.position + transform.forward * 1f, myCamera.transform.rotation, transform);
            instantiatedProjectile.GetComponent<Rigidbody>().velocity= myCamera.transform.forward * 20;
        }
    }
    void OnJump(InputAction.CallbackContext ctx){
        Vector3 feet = transform.position + Vector3.down;
        if (!Physics.Raycast(feet, Vector3.down, 0.1f)){
            animator.SetBool("isJumping", true);
            //test if on a wall
            if (isOnWall){
                animator.SetBool("isOnWall", true);
            }
            else{
                animator.SetBool("isOnWall", false);
            }
            return;
        }
        else{
            animator.SetBool("isJumping", false);
        }
        rb.velocity = Vector3.up  * jumpHeight;
    }
    public void StartRespawn(){
        StartCoroutine(Respawn());
        
    }
    //coroutine
    IEnumerator Respawn(){
        float timer = 0f;
        isRespawning = true;
        while(timer < respawnTimer){
            timer+= Time.deltaTime;
            yield return null;
        }
        int numSpawnpoints = spawnpoints.childCount;
        int rndIndex = Random.Range(0, numSpawnpoints);
        Transform target = spawnpoints.GetChild(rndIndex);
        transform.position = target.position;
        isRespawning = false;
    }
    void OnTriggerEnter(Collider other){
        isOnWall = false;
        Vector3 feet = transform.position + Vector3.down;
        if (!Physics.Raycast(feet, Vector3.down, 0.1f)){
            return;
        }
        else if(other.CompareTag("Bot")){
            return;
        }
        else{
            isOnWall = true;
            return;
        }
    }
}
