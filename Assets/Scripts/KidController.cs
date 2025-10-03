using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class KidController : SingletonMonoBehaviour<KidController>
{
    private readonly HashSet<Kid> kidsInZone = new HashSet<Kid>();

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

    public void NotifyEnterZone(Kid kid) => kidsInZone.Add(kid);
    public void NotifyExitZone(Kid kid) => kidsInZone.Remove(kid);

    private void HandleUserAction(UserActions action)
    {
        switch (action)
        {
            case UserActions.MoveLeft:
                DestroyKidIfOnLane(Lane.Left);
                break;
            case UserActions.MoveCenter:
                DestroyKidIfOnLane(Lane.Center);
                break;
            case UserActions.MoveRight:
                DestroyKidIfOnLane(Lane.Right);
                break;
        }
    }

    private void DestroyKidIfOnLane(Lane lane)
    {
        foreach (Kid kid in kidsInZone)
        {
            if (kid.lane != lane)
                return;
            kid.EjectAndDestroy();
            GameController.Instance.AddScore(1);
            return;
        }
    }
    
    private void KillKid(Kid kid)
    {
        if (kid != null)
        {
            Destroy(kid.gameObject);
        }
    }
}
