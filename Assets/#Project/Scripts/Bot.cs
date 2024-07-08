using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Bot : MonoBehaviour
{
    [SerializeField] public Transform[] targets;
    [HideInInspector] public Transform target;
    [HideInInspector] public NavMeshAgent agent;
    public Transform waypoints;
    private BotStateMachine stateMachine;

    public (bool see, Transform tar) CanSeePlayer(){
        Vector3 botFacing = transform.forward;
        Vector3 botPos = transform.position;
        Vector3 offset = Vector3.up * 0.2f;
        RaycastHit hit;
        foreach (Transform possibleTarget in targets)
        {
            target = possibleTarget;
            Vector3 botToPlayer = possibleTarget.position - botPos;
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
