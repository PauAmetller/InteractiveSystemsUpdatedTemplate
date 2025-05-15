using System;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public Action onCollected;

    [Header("Idle Animation")]
    public float rotationSpeed = 3f; // degrees per second

    void Update()
    {
        // Rotate around all axes slowly
        transform.Rotate(new Vector3(15, 30, 45) * rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Trigger the collected event (notifies manager)
            onCollected?.Invoke();

            // Destroy this power-up
            Destroy(gameObject);
        }
    }
}
