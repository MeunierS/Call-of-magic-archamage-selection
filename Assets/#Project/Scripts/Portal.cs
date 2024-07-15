using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] Transform otherPortal;
    List<PortalableObject> portalableObjects = new();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (PortalableObject obj in portalableObjects)
        {
            Vector3 localPosition = transform.InverseTransformPoint(obj.transform.position);
            if (localPosition.z > 0){
                obj.Teleport();
            }
        }
    }

    void OnTriggerEnter(Collider other){
        PortalableObject obj = other.GetComponent<PortalableObject>();
        if(obj != null){
            portalableObjects.Add(obj);
            obj.EnterPortal(transform, otherPortal);
        }
    }
    void OnTriggerExit(Collider other){
        PortalableObject obj = other.GetComponent<PortalableObject>();
        if (portalableObjects.Contains(obj)){
            portalableObjects.Remove(obj);
        }
    }
}
