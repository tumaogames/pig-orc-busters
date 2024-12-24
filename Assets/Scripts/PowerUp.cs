using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [Header("Lifetime Settings")]
    public float lifetime = 15f; // Time in seconds before the power-up is destroyed

    void Start()
    {
        // Schedule destruction after the lifetime expires
        Destroy(gameObject, lifetime);
    }
}
