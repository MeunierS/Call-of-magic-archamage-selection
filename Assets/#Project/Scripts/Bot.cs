using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(NavMeshAgent))]
public class Bot : MonoBehaviour
{
    [SerializeField] public List<GameObject> targets;
    [HideInInspector] public GameObject target;
    [HideInInspector] public NavMeshAgent agent;
    public Transform waypoints;
    public Transform spawnpoints;
    [HideInInspector] public BotStateMachine stateMachine;
    public Projectile projectile;
    public float respawnTimer = 2.1f;
    public bool isRespawning = false;
    private Animator animator;
    [SerializeField]public int scoreTarget;
    private PlayerInputManager playerInputManager;

    public (bool see, GameObject tar) CanSeePlayer(){
        Vector3 botFacing = transform.forward;
        Vector3 botPos = transform.position;
        Vector3 offset = Vector3.up * 0.2f;
        RaycastHit hit;
        foreach (GameObject possibleTarget in targets)
        {
            target = possibleTarget;
            Vector3 botToPlayer = possibleTarget.transform.position - botPos;
            if (Physics.Raycast(botPos + offset, botToPlayer + offset, out hit, 30f)){
                if (hit.collider.CompareTag("Player") || hit.collider.CompareTag("Bot")){
                    if (Vector3.Angle(botFacing, botToPlayer)<= 45f){
                        return (true, target);
                    }
                }
            }
        }
        return (false, null);
    }
    // Start is called before the first frame update
    void Awake() {
        playerInputManager = FindObjectOfType<PlayerInputManager>();
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        stateMachine = new BotStateMachine(this);
        stateMachine.Initialize(stateMachine.patrolState);
        animator = GetComponentInChildren<Animator>();
        animator.SetBool("isMoving", true);
    }

    void OnEnable() {
        playerInputManager.onPlayerJoined += AddTarget;
    }

    void OnDisable() {
        playerInputManager.onPlayerJoined -= AddTarget;
    }

    public void AddTarget(PlayerInput player) {
        targets.Add(player.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
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
