using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private MyEventHandler eventHandler;
    private bool playerReached = false;
    // Start is called before the first frame update
    void Start()
    {
        eventHandler = FindObjectOfType<MyEventHandler>();
    }
    
    private void OnTriggerEnter(Collider other) {
        PlayerController playerController = other.transform.root.GetComponent<PlayerController>();

        if(playerController != null && !playerReached)
        {
            playerReached = true;
            eventHandler.TriggerPlayerReachedGoal();
        }
    }
}
