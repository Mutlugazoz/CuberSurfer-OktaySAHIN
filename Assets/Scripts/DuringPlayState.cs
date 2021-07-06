using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class DuringPlayState :  IState
{
    public PathCreator pathCreator;
    private PlayerController playerController;
    private float speed = 5;
    private float distanceTravelled;
    private float screenPixelToFullySwerve;
    private float edgeDistanceOfPath;
    private GameObject target;
    private MyEventHandler eventHandler;
    private Vector3 lastFrameTouch;
    private float pathTrackingOffset_X = 0;
    private float distanceBetweenCamAndCharacter;

    public DuringPlayState(
        PlayerController playerController,
        PathCreator pathCreator, 
        GameObject target, 
        MyEventHandler eventHandler, 
        float speed, 
        float screenRatioToFullSwerve,
        float edgeDistanceOfPath,
        float distanceBetweenCamAndCharacter
        )
    {
        this.playerController = playerController;
        this.pathCreator = pathCreator;
        this.target = target;
        this.eventHandler = eventHandler;
        this.speed = speed;
        screenPixelToFullySwerve = Screen.width * screenRatioToFullSwerve;
        this.edgeDistanceOfPath = edgeDistanceOfPath;
        this.distanceBetweenCamAndCharacter = distanceBetweenCamAndCharacter;
    }

    public void BeginState()
    {
        eventHandler.TouchStart += SwerveStart;
        eventHandler.Touch += Swerve;
        eventHandler.lostCollectibles += TrackCollectiblesCount;
        eventHandler.playerReachedToGoal += TrackPlayerReachedGoal;

        if (pathCreator != null)
        {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(target.transform.position);
        }
    }

    public void DoState()
    {
        Move();
    }

    public void EndState()
    {
        eventHandler.TouchStart -= SwerveStart;
        eventHandler.Touch -= Swerve;
        eventHandler.lostCollectibles -= TrackCollectiblesCount;
        eventHandler.playerReachedToGoal -= TrackPlayerReachedGoal;
    }

    private void Move()
    {
        if (pathCreator != null)
        {
            distanceTravelled += speed * Time.deltaTime;
            target.transform.position = 
                pathCreator.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop) 
                + 
                target.transform.right * pathTrackingOffset_X
                +
                new Vector3(0, target.transform.position.y, 0);
                ;
            target.transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
            target.transform.eulerAngles = new Vector3(0, target.transform.eulerAngles.y, 0);
        }
    }

    private void SwerveStart(Vector3 touchLocation)
    {
        lastFrameTouch = touchLocation;
    }

    private void Swerve(Vector3 touchLocation)
    {

        Vector3 touchDiffBetwLastFrame = touchLocation - lastFrameTouch;

        pathTrackingOffset_X += Mathf.Clamp( touchDiffBetwLastFrame.x / screenPixelToFullySwerve, -1, 1) * edgeDistanceOfPath * 2;

        pathTrackingOffset_X = Mathf.Clamp(pathTrackingOffset_X, -edgeDistanceOfPath, edgeDistanceOfPath);

        lastFrameTouch = touchLocation;
    }

    private void TrackCollectiblesCount(int remainingCollectibles)
    {
        if(remainingCollectibles == 0)
        {
            playerController.switchState(GameStates.Fail);
        }
    }

    private void TrackPlayerReachedGoal()
    {
        playerController.switchState(GameStates.PostPlay);
    }
}
