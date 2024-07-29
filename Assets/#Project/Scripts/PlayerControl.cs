using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    private PlayerInput playerInput;
    public float jumpHeight;
    public Projectile projectile;
    [SerializeField] private float speed;
    private Camera myCamera;
    private Vector3 forward, right;
    public float cooldown = 1f;
    public float respawnTimer = 2.1f;
    private Rigidbody rb;
    public bool allowedToMove = true;
    public bool isRespawning = false;
    public Transform spawnpoints;
    [HideInInspector]public int scoreTarget = 6;
    private Animator animator;

    void Awake(){
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        myCamera = playerInput.camera;
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 feet = transform.position + Vector3.down;
        //test if jumping
        if (Physics.Raycast(feet, Vector3.down, 0.1f)){
            animator.SetBool("isJumping", false);
        }
        else{
            animator.SetBool("isJumping", true);
        }

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

            Vector2 movement = playerInput.actions["Move"].ReadValue<Vector2>();
            if(movement != Vector2.zero){
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
    }
    void OnDisable()
    {
    }
    public void OnShoot(){
        if (cooldown <= 0){
            cooldown = 1f;
            Projectile instantiatedProjectile = Instantiate(projectile, transform.position + transform.forward * 1f, myCamera.transform.rotation, transform);
            instantiatedProjectile.GetComponent<Rigidbody>().velocity= myCamera.transform.forward * 20;
        }
    }
    public void OnJump(){
        Vector3 feet = transform.position + Vector3.down;
        if (!Physics.Raycast(feet, Vector3.down, 0.1f)){
            return;
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
        Vector3 feet = transform.position + Vector3.down;
        //test if on a wall
        animator.SetBool("isOnWall", false);
        if (Physics.Raycast(feet, Vector3.down, 0.1f)){
            return;
        }
        else if(other.CompareTag("Bot")){
            return;
        }
        else{
            animator.SetBool("isOnWall", true);
            return;
        }
    }
}
