using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : MonoBehaviour
{[SerializeField]private GameObject prefab;
    [Range(1, 100)][SerializeField]private int batch = 20;
    [SerializeField]private float cooldown = 1;
    [SerializeField] Transform position;
    [SerializeField]private ProjectilePool pool;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Create());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator Create(){
        while(true){
            for(int _ = 0; _ < batch; _++){
                Projectile movement = pool.Create();
                movement.Initialize();
                Vector3 spawnPoint = pool.transform.position;
                movement.transform.position= spawnPoint;
                movement.Die();
            }
        yield return new WaitForSeconds(cooldown);
        }
    }
}