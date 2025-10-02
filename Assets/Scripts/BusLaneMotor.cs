using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusLaneMotor : MonoBehaviour
{
    [Header("Anchors (référence AnchorsController)")]
    public AnchorsController anchors;

    [Header("Mouvement")]
    [Tooltip("Durée de l'interpolation (0 = instantané).")]
    [SerializeField] private float moveDuration = 0.2f;
    [SerializeField] private AnimationCurve moveCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private void Start()
    {
        if (anchors != null && anchors.centerAnchor != null)
            SnapTo(anchors.centerAnchor); // spawn par défaut au centre
    }

    // Méthodes publiques appelées par PlayerController
    public void MoveLeft()
    {
        if (anchors != null && anchors.leftAnchor != null)
            MoveTo(anchors.leftAnchor.position);
    }

    public void MoveCenter()
    {
        if (anchors != null && anchors.centerAnchor != null)
            MoveTo(anchors.centerAnchor.position);
    }

    public void MoveRight()
    {
        if (anchors != null && anchors.rightAnchor != null)
            MoveTo(anchors.rightAnchor.position);
    }

    // --- Implémentation ---

    private void SnapTo(Transform target)
    {
        if (!target) return;
        transform.position = target.position;
    }

    private void MoveTo(Vector3 worldTarget)
    {
        StopAllCoroutines();
        if (moveDuration <= 0f)
        {
            transform.position = worldTarget;
        }
        else
        {
            StartCoroutine(MoveRoutine(worldTarget));
        }
    }

    private System.Collections.IEnumerator MoveRoutine(Vector3 worldTarget)
    {
        Vector3 start = transform.position;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / Mathf.Max(0.0001f, moveDuration);
            float k = moveCurve.Evaluate(Mathf.Clamp01(t));
            transform.position = Vector3.Lerp(start, worldTarget, k);
            yield return null;
        }

        transform.position = worldTarget;
    }
}
