using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectible
{
    bool CanBeCollected();
    void GetCollected(Transform parent, int height);
    void GetConsumed();
    void HitToBarrier();
}
