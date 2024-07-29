using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour
{
    public GameObject explosion;
    [HideInInspector]public UnityEvent<int> onHit;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 100 || transform.position.y > 100 || transform.position.z > 100 || transform.position.x < -100 || transform.position.y < -100 || transform.position.z < -100){
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider other){
        //* 1) play explosion on impact
        GameObject explo = Instantiate(explosion, transform.position, transform.rotation);
        explo.GetComponent<ParticleSystem>().Play();
        Destroy(explo, 1f);
        //* 2) kill & respawn if player or bot
        //Player
        if (other.CompareTag("Player")){
            if (!other.GetComponentInParent<PlayerControl>().isRespawning){
                //case a bot kill a player
                if(this.GetComponentInParent<Bot>() != null){
                    MatchData._kills[this.GetComponentInParent<Bot>().scoreTarget]++;
                    MatchData._deaths[other.GetComponentInParent<PlayerControl>().scoreTarget]++;
                }
                //todo future case a player kill another player
                if(this.GetComponentInParent<PlayerControl>() != null){
                    MatchData._kills[this.GetComponentInParent<PlayerControl>().scoreTarget]++;
                    MatchData._deaths[other.GetComponentInParent<PlayerControl>().scoreTarget]++;
                }
                //launch dissolve (+ block shooting + block movement)
                other.GetComponent<ObjectDissolve>().StartDissolve();
                //respawn victim somewhere else
                other.GetComponentInParent<PlayerControl>().StartRespawn();
            }
        }
        //Bot
        if (other.CompareTag("Bot")){
            if (!other.GetComponentInParent<Bot>().isRespawning){
                //case a bot kill a bot
                if(this.GetComponentInParent<Bot>() != null){
                    MatchData._kills[this.GetComponentInParent<Bot>().scoreTarget]++;
                    MatchData._deaths[other.GetComponentInParent<Bot>().scoreTarget]++;
                }
                //case a player kill a bot
                if(this.GetComponentInParent<PlayerControl>() != null){
                    MatchData._kills[this.GetComponentInParent<PlayerControl>().scoreTarget]++;
                    MatchData._deaths[other.GetComponentInParent<Bot>().scoreTarget]++;
                }
                //launch dissolve (+ block shooting + block movement)
                other.GetComponentInChildren<ObjectDissolve>().StartDissolve();
                //respawn victim somewhere else
                other.GetComponentInParent<Bot>().StartRespawn();
            }
        }
        Destroy(gameObject);
    }
}
