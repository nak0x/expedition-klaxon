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
        // Ici tu peux ajouter plusieurs touches
        if (Input.GetKeyDown(KeyCode.D))
        {
            OnUserAction?.Invoke(UserActions.MoveLeft);
        }

        if (Input.GetKeyDown(KeyCode.G)) // exemple pour gauche (AZERTY)
        {
            OnUserAction?.Invoke(UserActions.MoveRight);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnUserAction?.Invoke(UserActions.MoveCenter);
        }
    }
}
