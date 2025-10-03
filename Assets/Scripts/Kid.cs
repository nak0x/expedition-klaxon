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

        if (other.CompareTag("KidDestroyer"))
        {
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
    
    public void EjectAndDestroy()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb == null) rb = gameObject.AddComponent<Rigidbody>();

        rb.isKinematic = false;

        Vector3 forward = Vector3.forward;
        Vector3 lateral = Vector3.zero;
        if (lane == Lane.Left) lateral = Vector3.left;
        else if (lane == Lane.Right) lateral = Vector3.right;
        else lateral = Random.value > 0.5f ? Vector3.left : Vector3.right;

        Vector3 ejectDir = forward * 1.5f + lateral * 1.2f;
        ejectDir.Normalize();

        float punchForce = 15f;
        float upForce = 8f;

        Vector3 force = ejectDir * punchForce + Vector3.up * upForce;
        rb.AddForce(force, ForceMode.Impulse);

        // Ajoute une rotation pour effet non-linéaire
        rb.AddTorque(new Vector3(Random.Range(-300f, 300f), Random.Range(-100f, 100f), Random.Range(-80f, 80f)));

        speed = 0f;

        StartCoroutine(DestroyLater());
    }

    private System.Collections.IEnumerator DestroyLater()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

}
