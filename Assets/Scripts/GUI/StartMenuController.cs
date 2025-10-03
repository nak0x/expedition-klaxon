using UnityEngine;

public class StartMenuController : MonoBehaviour
{
    public void TriggerStart()
    {
        GameController.Instance.OnStartEvent?.Invoke();
    }
}
