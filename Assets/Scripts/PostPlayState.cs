using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PostPlayState : IState
{
    private MyEventHandler eventHandler;

    public PostPlayState(MyEventHandler eventHandler)
    {
        this.eventHandler = eventHandler;
    }
    public void BeginState()
    {
        eventHandler.TouchStart += Restart;
    }

    public void DoState()
    {

    }

    public void EndState()
    {
        eventHandler.TouchStart -= Restart;
    }

    private void Restart(Vector3 touchLocation)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
