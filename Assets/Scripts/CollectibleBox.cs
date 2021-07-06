using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleBox : MonoBehaviour, ICollectible
{
    private bool isCollected = false;

    private void Start() {
        if(transform.parent != null)
            isCollected = true;
    }
    
    public bool CanBeCollected()
    {
        return !isCollected;
    }

    public void GetCollected(Transform parent, int height)
    {
        transform.parent = parent;
        transform.localPosition = new Vector3(0, -height, 0);
        transform.localEulerAngles = Vector3.zero;
        isCollected = true;
    }
    public void GetConsumed(){
        if(transform.parent != null && isCollected)
        {
            Collector collector = transform.parent.GetComponent<Collector>();
            
            collector.BoxIsConsumed(gameObject);

        }
    }

    public void HitToBarrier()
    {
        if(transform.parent != null && isCollected)
        {
            Collector collector = transform.parent.GetComponent<Collector>();
            if(collector != null)
                StartCoroutine(ActivateRigidBodyCoroutine(collector.HitToBarrierOccured(transform)));

        }
    }

    private void OnTriggerEnter(Collider other) {
        if(transform.parent != null && isCollected)
        {
            Collector collector = transform.parent.GetComponent<Collector>();
            if(collector != null)
            {
                collector.TriggerOccured(other.transform);
            }
                

        }
    }

    private IEnumerator ActivateRigidBodyCoroutine(float duration)
    {
        yield return new WaitForSeconds(duration);

        Rigidbody rb = GetComponent<Rigidbody>();
        if(rb != null)
            rb.isKinematic = false;

        Collider collider = GetComponent<Collider>();
        if(collider != null)
            collider.isTrigger = false;
    }

}
