using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrePlayState : IState
{
    private PlayerController playerController;
    private MyEventHandler eventHandler;
    public PrePlayState(PlayerController playerController, MyEventHandler eventHandler)
    {
        this.playerController = playerController;
        this.eventHandler = eventHandler;
    }

    public void BeginState()
    {
        eventHandler.TouchStart += SwitchToNextState;
    }

    public void DoState()
    {

    }

    public void EndState()
    {
        eventHandler.TouchEnd -= SwitchToNextState;
    }

    private void SwitchToNextState(Vector3 touchLocation)
    {
        playerController.switchState(GameStates.DuringPlay);
    }
}
