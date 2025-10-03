using System.Collections;
using UnityEngine;

public class BusLaneMotor : MonoBehaviour
{
    [Header("Anchors (référence AnchorsController)")]
    public AnchorsController anchors;

    [Header("Mouvement")]
    [Tooltip("Durée de l'interpolation (0 = instantané).")]
    [SerializeField] private float moveDuration = 0.2f;
    [SerializeField] private AnimationCurve moveCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    [Header("Punch settings")]
    [SerializeField] private float punchDistance = 0.6f;
    [SerializeField] private float punchDuration = 0.2f;

    private void Start()
    {
        if (anchors != null && anchors.centerAnchor != null)
            SnapTo(anchors.centerAnchor); // spawn par défaut au centre
    }

    // Méthodes publiques appelées par PlayerController
    public void MoveLeft()
    {
        if (anchors != null && anchors.leftAnchor != null)
        {
            if (Vector3.Distance(transform.position, anchors.leftAnchor.position) < 0.01f)
                PunchForward();
            else
                MoveTo(anchors.leftAnchor.position);
        }
    }

    public void MoveCenter()
    {
        if (anchors != null && anchors.centerAnchor != null)
        {
            if (Vector3.Distance(transform.position, anchors.centerAnchor.position) < 0.01f)
                PunchForward();
            else
                MoveTo(anchors.centerAnchor.position);
        }
    }

    public void MoveRight()
    {
        if (anchors != null && anchors.rightAnchor != null)
        {
            if (Vector3.Distance(transform.position, anchors.rightAnchor.position) < 0.01f)
                PunchForward();
            else
                MoveTo(anchors.rightAnchor.position);
        }
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

    private IEnumerator MoveRoutine(Vector3 worldTarget)
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

        // Ajoute un coup après le déplacement
        PunchForward();
    }

    public void PunchForward()
    {
        StopCoroutine(nameof(PunchRoutine));
        StartCoroutine(PunchRoutine());
    }

    private IEnumerator PunchRoutine()
    {
        Vector3 original = transform.position;
        Vector3 punch = original - transform.forward * punchDistance;

        float t = 0f;
        // Aller (0 -> 0.5)
        while (t < 0.5f)
        {
            t += Time.deltaTime / (punchDuration * 0.5f);
            transform.position = Vector3.Lerp(original, punch, t * 2f);
            yield return null;
        }

        t = 0f;
        // Retour (0.5 -> 1)
        while (t < 1f)
        {
            t += Time.deltaTime / (punchDuration * 0.5f);
            transform.position = Vector3.Lerp(punch, original, t);
            yield return null;
        }

        transform.position = original;
    }
}
