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
    }
    public void Enter(){
        target = bot.CanSeePlayer().tar;
    }
    public void Perform(){
        agent.SetDestination(target.position);
        //todo insert bot shoot at target here
        if (!bot.CanSeePlayer().see){
            stateMachine.TransistionTo(stateMachine.patrolState);
        }
    }
    public void Exit(){
    }
    
}
