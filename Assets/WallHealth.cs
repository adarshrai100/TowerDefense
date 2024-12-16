using UnityEngine;
using UnityEngine.AI;

public class WallHealth : MonoBehaviour
{
    public float maxHealth = 100f; // Maximum health of the wall
    public float currentHealth;
    public NavMeshAgent Enemy;

    void Start()
    {
        // Initialize current health to max health
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log($"{gameObject.name} took {damage} damage. Current health: {currentHealth}");

        // Check if the wall is destroyed
        if (currentHealth <= 0)
        {
            if (Enemy != null)
                Enemy.isStopped = false;
            DestroyWall();
        }
    }

    private void DestroyWall()
    {
        Debug.Log($"{gameObject.name} is destroyed!");
        // You can add any additional effects here (like particle effects)
        Destroy(gameObject); // Destroy the wall GameObject
    }
}