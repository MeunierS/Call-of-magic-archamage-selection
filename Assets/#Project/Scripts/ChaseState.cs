using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : IState
{
    private NavMeshAgent agent;
    private Transform target;
    private Bot bot;
    private BotStateMachine stateMachine;

    public ChaseState(Bot bot, BotStateMachine stateMachine){
        this.bot = bot;
        this.stateMachine = stateMachine;
        agent = bot.agent;
        target = bot.target;
    }
    public void Enter(){
        Debug.Log("Entering Chase State");
    }
    public void Perform(){
        agent.SetDestination(target.position);
        if (!bot.CanSeePlayer()){
            stateMachine.TransistionTo(stateMachine.patrolState);
        }
    }
    public void Exit(){
        Debug.Log("Exiting Chase State");
    }
    
}
