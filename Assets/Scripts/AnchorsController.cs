using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorsController : MonoBehaviour
{
    [Header("Lane Anchors")]
    public Transform leftAnchor;
    public Transform centerAnchor;
    public Transform rightAnchor;

    private void OnDrawGizmos()
    {
        if (leftAnchor != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(leftAnchor.position, 0.3f);
        }

        if (centerAnchor != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(centerAnchor.position, 0.3f);
        }

        if (rightAnchor != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(rightAnchor.position, 0.3f);
        }
    }
}
