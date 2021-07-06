using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumer : MonoBehaviour
{
    private bool consumedOnce = false;

    private void OnTriggerEnter(Collider other) {

        ICollectible collectible = other.GetComponent<ICollectible>();
        if(collectible != null && !consumedOnce)
        {
            collectible.GetConsumed();
            consumedOnce = true;
        }
    }
}
