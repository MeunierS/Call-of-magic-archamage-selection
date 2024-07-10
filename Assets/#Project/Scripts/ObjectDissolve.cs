using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ObjectDissolve : MonoBehaviour
{
    Material material;
    public float dissolveTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
        ResetDissolve();
        //StartDissolve();
        //Invoke("StartDissolve", 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDissolve(){
        StartCoroutine(Dissolve());
        StartCoroutine(ResetDissolve());
    }
    //coroutine
    IEnumerator Dissolve(){
        float timer = 0f;
        //yield return new WaitForSeconds(1f);
        while(timer < dissolveTime){
            material.SetFloat("_Dissolve", timer / dissolveTime);
            timer+= Time.deltaTime;
            yield return null;
        }
        material.SetFloat("_Dissolve", 1.5f);
    }
    IEnumerator ResetDissolve(){
    //will reset the Dissolve AND block possibility to Shoot & move until reset
        float timer = 0f;
        //if bot
        if (this.GetComponentInParent<Bot>() != null){
            //block bot shoot
            this.GetComponentInParent<Bot>().stateMachine.chaseState.cooldown = 10f;
            //block bot movement
            this.GetComponentInParent<Bot>().stateMachine.chaseState.agent.enabled = false;
        }
        //if player
        if (this.GetComponentInParent<PlayerControl>() != null){
            //block player shoot
            this.GetComponentInParent<PlayerControl>().cooldown = 10f;
            //block player movement
            this.GetComponentInParent<PlayerControl>().allowedToMove = false;
        }
        //yield return new WaitForSeconds(1f);
        while(timer < dissolveTime+.5f){
            timer+= Time.deltaTime;
            yield return null;
        }
        material.SetFloat("_Dissolve", 0f);
        //if bot
        if (this.GetComponentInParent<Bot>() != null){
            //reset bot
            this.GetComponentInParent<Bot>().stateMachine.chaseState.agent.enabled = true;
            this.GetComponentInParent<Bot>().stateMachine.chaseState.cooldown = .5f;
        }
        //if player
        if (this.GetComponentInParent<PlayerControl>() != null){
            //reset player
            this.GetComponentInParent<PlayerControl>().cooldown = .5f;
            this.GetComponentInParent<PlayerControl>().allowedToMove = true;
        }
    }
}
