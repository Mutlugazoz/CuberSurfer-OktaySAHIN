using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public List<GameObject> prePlayUIs;
    public List<GameObject> postPlayUIs;
    public List<GameObject> failUIs;
    private MyEventHandler eventHandler;

    private void Start() {
        eventHandler = FindObjectOfType<MyEventHandler>();
        eventHandler.GameStateChanged += TrackStateChange;
    }

    private void TrackStateChange(GameStates previousState, GameStates nextState)
    {
        switch(nextState)
        {
            case GameStates.DuringPlay:
                SwitchedToDuringPlay();
                break;

            case GameStates.PostPlay:
                SwitchedToPostPlay();
                break;

            case GameStates.Fail:
                SwitchedToFail();
                break;

            default:
                break;
        }
    }

    private void SwitchedToDuringPlay()
    {
        prePlayUIs.ForEach(delegate (GameObject go) {
            go.SetActive(false);
        });
    }

    private void SwitchedToPostPlay()
    {
        postPlayUIs.ForEach(delegate (GameObject go) {
            go.SetActive(true);
        });
    }

    private void SwitchedToFail()
    {
        failUIs.ForEach(delegate (GameObject go) {
            go.SetActive(true);
        });
    }
}
