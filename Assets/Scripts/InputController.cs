using UnityEngine;
using Utils;

public class InputController : SingletonMonoBehaviour<InputController>
{
    public delegate void InputEvent(string userAction);
    public event InputEvent OnUserAction;

    public delegate void InputSpecificEvent();
    public event InputSpecificEvent OnUserPause;
}
