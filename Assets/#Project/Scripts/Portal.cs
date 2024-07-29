using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] Portal[] otherPortal;
    public Transform arrival;
    [SerializeField] Cinemachine.CinemachineFreeLook cinemachine;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        Vector3 decal = Vector3.zero;
        Vector3 tpPosition = otherPortal[Random.Range(0, otherPortal.Length)].arrival.transform.position;
        if (other.CompareTag("Player"))
        {
            decal = tpPosition - other.transform.position;
            cinemachine.OnTargetObjectWarped(other.transform, decal);
        }
        other.GetComponentInParent<Rigidbody>().velocity = Vector3.zero;
        other.transform.position = tpPosition;

    }
}
