using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : IState
{
    private NavMeshAgent agent;
    private Transform waypoints;
    private Bot bot;
    private BotStateMachine stateMachine;
    public bool isAtDestination { get {return agent.remainingDistance <= agent.stoppingDistance;} }
    public PatrolState(Bot bot, BotStateMachine stateMachine)
    {
        this.bot= bot; 
        this.stateMachine= stateMachine;
        agent = bot.agent;
        waypoints = bot.waypoints;
    }
    private void SelectDestination(){
        int numWaypoints = waypoints.childCount;
        int rndIndex = Random.Range(0, numWaypoints);
        Transform target = waypoints.GetChild(rndIndex);
        agent.SetDestination(target.position);
    }
    public void Enter(){
        SelectDestination();
    }
    public void Perform(){
        if (isAtDestination)
        {
            SelectDestination();
        }
        if (bot.CanSeePlayer().see){
            stateMachine.TransistionTo(stateMachine.chaseState);
        }
    }
    public void Exit(){
    }
}
