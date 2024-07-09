using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    private Stack<Projectile> pool = new();
    [SerializeField]private GameObject prefab;
    [SerializeField]private int initialBatch = 50;
    private void Awake(){
        for(int _=0; _ < initialBatch; _++){
            GameObject newOne = Instantiate(prefab);
            newOne.GetComponent<Projectile>().pool = this;
            newOne.GetComponent<Projectile>().Die();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Projectile Create(Vector3 position, Quaternion rotation){
        if(pool.Count == 0){
            GameObject newOne = Instantiate(prefab, position, rotation);
            newOne.GetComponent<Projectile>().pool = this;
            return newOne.GetComponent<Projectile>();
        }
        Projectile movement = pool.Pop();
        movement.transform.position = position;
        movement.transform.rotation = rotation;
        return movement;
    }
    public Projectile Create() => Create(Vector3.zero, Quaternion.identity);
    public Projectile Create(Vector3 position) => Create(position, Quaternion.identity);
    public void AddToPool(Projectile movement){
        pool.Push(movement);
    }
}
