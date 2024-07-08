using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Bot : MonoBehaviour
{
    [SerializeField] public Transform target;
    [HideInInspector] public NavMeshAgent agent;
    public Transform waypoints;
    private BotStateMachine stateMachine;

    public bool CanSeePlayer(){
        Vector3 botFacing = transform.forward;
        Vector3 botPos = transform.position;
        Vector3 botToPlayer = target.position - botPos;
        Vector3 offset = Vector3.up * 0.1f;
        RaycastHit hit;
        if (Physics.Raycast(botPos + offset, botToPlayer + offset, out hit, 30f)){
            if (hit.collider.CompareTag("Player")){
                if (Vector3.Angle(botFacing, botToPlayer)<= 45f){
                    return true;
                }
            }
        }
        return false;
    }
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        stateMachine = new BotStateMachine(this);
        stateMachine.Initialize(stateMachine.patrolState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }
}
