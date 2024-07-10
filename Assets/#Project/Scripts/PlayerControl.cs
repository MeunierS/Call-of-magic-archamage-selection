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
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //rotate player with camera
        transform.rotation = myCamera.transform.rotation;

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
            //moving = movement != Vector2.zero;
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
        //* this works
        if (cooldown <= 0){
            cooldown = 0.5f;
            Projectile instantiatedProjectile = Instantiate(projectile, transform.position + transform.forward * 1f, Camera.main.transform.rotation, null);
            instantiatedProjectile.GetComponent<Rigidbody>().velocity= Camera.main.transform.forward * 20;
        }
        //! this doesn't
        // projectile.Initialize();
        // projectile.transform.position = transform.position + Vector3.up *1.5f;
        // Debug.Log(projectile.transform.position);
        //projectile.transform.Translate(Camera.main.transform.forward * 20 * Time.deltaTime);
        //projectile.GetComponent<Rigidbody>().velocity= Camera.main.transform.forward * 20;
    }
    void OnJump(InputAction.CallbackContext ctx){
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
        //yield return new WaitForSeconds(1f);
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
}
