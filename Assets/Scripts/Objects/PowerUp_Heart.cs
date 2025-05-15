using UnityEngine;

public class PowerUp_Heart : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("PlayerCollider"))
            return;

        Transform root = other.transform.root;
        PlayerCollisions playerCollisions = root.GetComponentInChildren<PlayerCollisions>();

        if (playerCollisions != null && playerCollisions.lifePoints != null)
        {
            LifePoints lifePoints = playerCollisions.lifePoints.GetComponent<LifePoints>();
            if (lifePoints != null)
            {
                lifePoints.Heal(1);
            }
        }

        // Call the base Collect() method so PowerUpLogic can respond
        GetComponent<PowerUp>()?.Collect();
    }
}
