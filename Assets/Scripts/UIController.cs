using System;
using UnityEngine;
using Utils;

public class UIController : SingletonMonoBehaviour<UIController>
{
    public void TriggerStart()
    {
        GameController.Instance.OnStartEvent?.Invoke();
    }
}
