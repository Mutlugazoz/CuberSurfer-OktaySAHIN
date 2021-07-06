using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        ICollectible collectible = other.GetComponent<ICollectible>();

        if(collectible != null)
        {
            if(!collectible.CanBeCollected())
            {
                collectible.HitToBarrier();
                StartCoroutine(DisableColliderAfter1Frame());
            }
        }
    }

    private IEnumerator DisableColliderAfter1Frame()
    {
        yield return 0;
        Collider collider = GetComponent<Collider>();

        collider.enabled = false;
    }
}
