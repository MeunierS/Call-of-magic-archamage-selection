using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour
{
    public ProjectilePool pool;
    public GameObject explosion;
    public bool initialized = false;
    [HideInInspector]public UnityEvent<int> onHit;
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.forward * 20 * Time.deltaTime);
        if (transform.position.x > 100 || transform.position.y > 100 || transform.position.z > 100 || transform.position.x < -100 || transform.position.y < -100 || transform.position.z < -100){
            //this.Die();
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider other){
        //todo: other collider behavior
        //* 1) play explosion on impact
        GameObject explo = Instantiate(explosion, transform.position, transform.rotation);
        explo.GetComponent<ParticleSystem>().Play();
        Destroy(explo, 1f);
        //* 2) kill & respawn if player or bot
        //this.Die();
        Destroy(gameObject);
    }
    public void Initialize(){
        if(initialized) return;
        initialized = true;
        gameObject.SetActive(true);
    }
    public void Die(){
        gameObject.SetActive(false);
        initialized = false;
        pool.AddToPool(this);
    }
}
