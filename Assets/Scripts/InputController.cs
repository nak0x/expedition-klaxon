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
        // Gauche
        if (Input.GetKeyDown(KeyCode.F))
            OnUserAction?.Invoke(UserActions.MoveLeft);

        // Centre
        if (Input.GetKeyDown(KeyCode.Space))
            OnUserAction?.Invoke(UserActions.MoveCenter);

        // Droite
        if (Input.GetKeyDown(KeyCode.J))
            OnUserAction?.Invoke(UserActions.MoveRight);

        // Pause
        if (Input.GetKeyDown(KeyCode.Escape))
            OnUserAction?.Invoke(UserActions.TogglePause);
    }
}
