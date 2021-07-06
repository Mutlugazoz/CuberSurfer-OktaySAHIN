using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void StateChangedDelegate(GameStates previousState, GameStates newState);
public delegate void TocuhEvent(Vector3 touchLocation);
public delegate void LostCollectiblesEvent(int remainingCollectibles);
public delegate void StandardEvent();

public class MyEventHandler : MonoBehaviour
{
    public event StateChangedDelegate GameStateChanged;

    public event TocuhEvent TouchStart;

    public event TocuhEvent Touch;

    public event TocuhEvent TouchEnd;
    public event LostCollectiblesEvent lostCollectibles;
    public event StandardEvent playerReachedToGoal;


    public void TriggerStateChange(GameStates previousState, GameStates newState)
    {
        if(GameStateChanged != null)
            GameStateChanged(previousState, newState);
    }

    public void TriggerTouchStart(Vector3 touchLocation)
    {
        if(TouchStart != null)
            TouchStart(touchLocation);
    }

    public void TriggerTouch(Vector3 touchLocation)
    {
        if(Touch != null)
            Touch(touchLocation);
    }

    public void TriggerTouchEnd(Vector3 touchLocation)
    {
        if(TouchEnd != null)
            TouchEnd(touchLocation);
    }

    public void TriggerLostCollectibles(int remainingCollectibles)
    {
        if(lostCollectibles != null)
            lostCollectibles(remainingCollectibles);
    }

    public void TriggerPlayerReachedGoal()
    {
        if(playerReachedToGoal != null)
            playerReachedToGoal();
    }
}
