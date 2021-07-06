using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    private PlayerController playerController;
    private bool gonnaFallRoutineRunning = false;
    private MyEventHandler eventHandler;
    private bool playerFailed = false;

    private void Start() {
        playerController = GetComponent<PlayerController>();
        eventHandler = FindObjectOfType<MyEventHandler>();
        eventHandler.GameStateChanged += TrackFail;
    }

    public List<Transform> collectedBoxes = new List<Transform>();
    public void TriggerOccured(Transform other)
    {
        if(!collectedBoxes.Contains(other))
        {
            ICollectible collectible = other.GetComponent<ICollectible>();
            if(collectible != null)
            {
                if(collectible.CanBeCollected())
                {
                    collectedBoxes.Add(other.transform);
                    collectible.GetCollected(transform, collectedBoxes.Count);
                    transform.position += Vector3.up;
                }
            }
        }
    }

    public void BoxIsConsumed(GameObject box)
    {
        if(collectedBoxes.Contains(box.transform))
        {
            collectedBoxes.Remove(box.transform);
            eventHandler.TriggerLostCollectibles(collectedBoxes.Count);
            Destroy(box);
            reArrangeBoxes();
        }
    } 

    public float HitToBarrierOccured(Transform collectibleTransform) {

        collectibleTransform.parent = null;

        if(collectedBoxes.Contains(collectibleTransform))
        {
            collectedBoxes.Remove(collectibleTransform);
            eventHandler.TriggerLostCollectibles(collectedBoxes.Count);
        }
            

        float timeToTravel_1AndHalf_Block = 1.5f / playerController.characterSpeed;

        if(!gonnaFallRoutineRunning)
        {
            StartCoroutine(FallAfterSecs(timeToTravel_1AndHalf_Block));
        }

        return timeToTravel_1AndHalf_Block;
    }

    private IEnumerator FallAfterSecs(float secs)
    {
        gonnaFallRoutineRunning = true;
        
        yield return new WaitForSeconds(secs);
        
        if(!playerFailed)
            reArrangeBoxes();

        gonnaFallRoutineRunning = false;
    }

    private void reArrangeBoxes()
    {
        transform.position = new Vector3(transform.position.x, collectedBoxes.Count, transform.position.z);

        for(int i = 1; i <= collectedBoxes.Count; i++)
        {
            collectedBoxes[i - 1].localPosition = new Vector3(0, -i, 0);
        }
    }

    private void TrackFail(GameStates previousState, GameStates newState)
    {
        if(newState == GameStates.Fail)
            playerFailed = true;
    }
}
