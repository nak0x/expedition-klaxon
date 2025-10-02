using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private BusLaneMotor motor;

    private void Awake()
    {
        motor = GetComponent<BusLaneMotor>();
        if (motor == null)
            Debug.LogError("[PlayerController] BusLaneMotor manquant sur le même GameObject.");
    }

    private void OnEnable()
    {
        if (InputController.Instance != null)
            InputController.Instance.OnUserAction += HandleUserAction;
    }

    private void OnDisable()
    {
        if (InputController.Instance != null)
            InputController.Instance.OnUserAction -= HandleUserAction;
    }

    private void HandleUserAction(UserActions action)
    {
        if (motor == null) return;

        switch (action)
        {
            case UserActions.MoveLeft:
                Debug.Log("LEFT");
                motor.MoveLeft();
                break;

            case UserActions.MoveCenter:
                Debug.Log("CENTER");
                motor.MoveCenter();
                break;

            case UserActions.MoveRight:
                Debug.Log("RIGHT");
                motor.MoveRight();
                break;

            case UserActions.TogglePause:
                // à compléter
                break;
        }
    }
}
