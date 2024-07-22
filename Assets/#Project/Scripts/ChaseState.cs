using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : IState
{
    public NavMeshAgent agent;
    private Transform target;
    private Bot bot;
    private BotStateMachine stateMachine;
    public float cooldown = 1f;
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
        //bot shoot at target
        Shoot();
        if (!bot.CanSeePlayer().see){
            stateMachine.TransistionTo(stateMachine.patrolState);
        }
    }
    public void Exit(){
    }
    public void Shoot(){
        if (cooldown > 0){
            cooldown -= Time.deltaTime;
        }
        if (cooldown <= 0){
            cooldown = 1f;
            Projectile instantiatedProjectile = GameObject.Instantiate(bot.projectile, bot.transform.position + bot.transform.forward * 1f + Vector3.up, bot.transform.rotation, bot.transform);
            instantiatedProjectile.GetComponent<Rigidbody>().velocity= bot.transform.forward * 20;
        }
    }
}
