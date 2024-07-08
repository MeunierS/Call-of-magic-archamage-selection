using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotStateMachine
{
    public IState CurrentState { get; private set; }
    public ChaseState chaseState;
    public PatrolState patrolState;
    public BotStateMachine(Bot bot)
    {
        chaseState = new ChaseState(bot, this);
        patrolState = new PatrolState(bot, this);
    }
    public void Initialize(IState startingState){
        CurrentState = startingState;
        startingState.Enter();
    }
    public void Update(){
        CurrentState?.Perform();
    }
    public void TransistionTo(IState nextState){
        CurrentState?.Exit();
        CurrentState = nextState;
        nextState.Enter();
    }
}