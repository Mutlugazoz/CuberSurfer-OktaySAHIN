using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PlayerController : MonoBehaviour
{
    public PathCreator pathCreator;
    public float characterSpeed = 5;
    public float edgeDistanceOfPath = 4;
    [Range(0, 1)]
    public float screenRatioToFullSwerve = 0.35f;
    public float distanceBetweenCamAndCharacter = 10;
    public Vector3 cameraTrackingOffset;
    private IState prePlayState;
    private IState duringPlayState;
    private IState postPlayState;
    private IState failState;
    private IState currentStatePointer;
    private GameStates currentStateEnum = GameStates.PrePlay;
    private MyEventHandler eventHandler;

    // Start is called before the first frame update
    void Start()
    {
        eventHandler = FindObjectOfType<MyEventHandler>();

        prePlayState = new PrePlayState(this, eventHandler);
        duringPlayState = new DuringPlayState(
            this,
            pathCreator, 
            gameObject, 
            eventHandler, 
            characterSpeed, 
            screenRatioToFullSwerve, 
            edgeDistanceOfPath,
            distanceBetweenCamAndCharacter
            );
        postPlayState = new PostPlayState(eventHandler);
        failState = new FailState(eventHandler);

        currentStatePointer = prePlayState;
        currentStatePointer.BeginState();
    }

    void Update()
    {
        currentStatePointer.DoState();
    }

    public void switchState(GameStates newState) 
    {
        if(newState != currentStateEnum) 
        {
            currentStatePointer.EndState();

            switch(newState) 
            {
                case GameStates.PrePlay:
                    currentStatePointer = prePlayState;
                    break;

                case GameStates.DuringPlay:
                    currentStatePointer = duringPlayState;
                    break;

                case GameStates.PostPlay:
                    currentStatePointer = postPlayState;
                    break;

                case GameStates.Fail:
                    currentStatePointer = failState;
                    break;

                default:
                    break;
            }

            eventHandler.TriggerStateChange(currentStateEnum, newState);
            currentStateEnum = newState;

            currentStatePointer.BeginState();

        }


    }
}
