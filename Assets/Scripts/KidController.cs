using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidController : MonoBehaviour
{
    public static KidController Instance { get; private set; }

    private readonly HashSet<Kid> kidsInZone = new HashSet<Kid>();

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
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

    public void NotifyEnterZone(Kid kid) => kidsInZone.Add(kid);
    public void NotifyExitZone(Kid kid) => kidsInZone.Remove(kid);

    private void HandleUserAction(UserActions action)
    {
        // Quand tu appuies sur Q/S/D : on vérifie s’il y a un kid dans la zone sur la lane visée
        switch (action)
        {
            case UserActions.MoveLeft:
                LogIfAnyOnLane(Lane.Left);
                break;
            case UserActions.MoveCenter:
                LogIfAnyOnLane(Lane.Center);
                break;
            case UserActions.MoveRight:
                LogIfAnyOnLane(Lane.Right);
                break;
        }
    }

    private void LogIfAnyOnLane(Lane lane)
    {
        foreach (var kid in kidsInZone)
        {
            if (kid != null && kid.lane == lane)
            {
                Debug.Log($"[KidController] Touche lane={lane} : kid présent dans la DetectionLine ✔");
                kid.EjectAndDestroy();
                GameController.Instance.AddScore(1);
                return;
            }
        }
        Debug.Log($"[KidController] Touche lane={lane} : aucun kid dans la DetectionLine.");
    }
    
    private void KillKid(Kid kid)
    {
        if (kid != null)
        {
            Destroy(kid.gameObject);
        }
    }
}
