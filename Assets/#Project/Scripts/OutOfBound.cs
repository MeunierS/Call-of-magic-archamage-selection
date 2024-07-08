using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -3){
            Vector3 resetPosition;
            resetPosition.x= 0;
            resetPosition.y= 1;
            resetPosition.z= 0;
            transform.position = resetPosition;
        }
    }
}
