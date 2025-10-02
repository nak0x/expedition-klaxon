using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private void OnEnable()
    {
        InputController.Instance.OnUserAction += HandleUserAction;
    }

    private void OnDisable()
    {
        InputController.Instance.OnUserAction -= HandleUserAction;
    }

    private void HandleUserAction(UserActions action)
    {
        switch (action)
        {
            case UserActions.MoveLeft:
                Debug.Log("Move Left triggered!");
                break;
            case UserActions.MoveRight:
                Debug.Log("Move Right triggered!");
                break;
            case UserActions.MoveCenter:
                Debug.Log("Move Center triggered!");
                break;
            case UserActions.TogglePause:
                Debug.Log("Pause toggled!");
                break;
        }
    }
}
