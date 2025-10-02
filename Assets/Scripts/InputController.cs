using UnityEngine;
using Utils;

public enum UserActions
{
    TogglePause,
    MoveLeft,
    MoveRight,
    MoveCenter,
}

public class InputController : SingletonMonoBehaviour<InputController>
{
    public delegate void InputEvent(UserActions action);
    public event InputEvent OnUserAction;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
            OnUserAction?.Invoke(UserActions.MoveLeft);

        if (Input.GetKeyDown(KeyCode.G))
            OnUserAction?.Invoke(UserActions.MoveRight);

        if (Input.GetKeyDown(KeyCode.Space))
            OnUserAction?.Invoke(UserActions.MoveCenter);

        if(Input.GetKeyDown(KeyCode.Escape))
            OnUserAction?.Invoke(UserActions.TogglePause);
    }
}
