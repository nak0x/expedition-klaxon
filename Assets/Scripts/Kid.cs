using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Lane { Left, Center, Right }

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Kid : MonoBehaviour
{
    [Header("State")]
    public Lane lane;
    public float speed = 8f;
    public Vector3 moveDirection = Vector3.back; // change selon ton axe

    public bool IsInDetectionZone { get; private set; }

    private void Awake()
    {
        // Rigidbody requis pour les triggers ; on le met en "kinematic"
        var rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
    }

    private void Update()
    {
        transform.position += moveDirection.normalized * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("DetectionLine"))
        {
            IsInDetectionZone = true;
            KidController.Instance?.NotifyEnterZone(this);
        }

        if (other.CompareTag("KidDestroyer")) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("DetectionLine"))
        {
            IsInDetectionZone = false;
            KidController.Instance?.NotifyExitZone(this);
        }
    }

    private void OnMouseDown()
    {
        // Clique sur l’enfant : on log UNIQUEMENT s’il est dans la zone
        if (IsInDetectionZone)
        {
            Debug.Log($"[Kid] Click OK: lane={lane}, in detection zone.");
        }
    }
}
