using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalableObject : MonoBehaviour
{
    Transform portalIn;
    Transform portalOut;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterPortal(Transform entrance, Transform exit){
        portalIn = entrance;
        portalOut = exit;
    }

    public void Teleport(){
        Vector3 localPosition = portalIn.InverseTransformPoint(transform.position);
        localPosition = Quaternion.Euler(0, 180, 0) * localPosition;
        transform.position = portalOut.TransformPoint(localPosition);

        Quaternion localRotation = Quaternion.Inverse(portalIn.rotation) * transform.rotation;
        localRotation = Quaternion.Euler(0, 180, 0) * localRotation;
        transform.rotation = portalOut.rotation * localRotation;

        Vector3 localVelocity = portalIn.InverseTransformDirection(rb.velocity);
        localVelocity = Quaternion.Euler(0, 180, 0) * localVelocity;
        rb.velocity = portalOut.TransformDirection(localVelocity);

        var tmp =portalIn;
        portalIn = portalOut;
        portalOut = tmp;
    }
}
