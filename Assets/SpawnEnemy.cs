using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SpawnEnemy : MonoBehaviour
{
    public Transform target; // Assign the target GameObject in the Inspector
    private NavMeshAgent agent;
    private Coroutine damageCoroutine;

    void Start()
    {
        // Get the NavMeshAgent component
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // Check if the target is assigned
        if (target != null)
        {
            // Set the destination of the NavMeshAgent to the target's position
            agent.SetDestination(target.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object is tagged "wall"
        if (other.CompareTag("Fence"))
        {
            // Stop the agent
            agent.isStopped = true;
            // agent.velocity = Vector3.zero;

            WallHealth wallHealth = other.GetComponent<WallHealth>();
            if (wallHealth != null)
            {
                wallHealth.Enemy = this.GetComponent<NavMeshAgent>();
                // Start the coroutine to damage the wall
                damageCoroutine = StartCoroutine(DamageWallOverTime(wallHealth));
            }

            // Call the function to perform when colliding with the wall
            //PerformAction();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exittt");
        // Stop damaging the wall when the agent exits the trigger
        if (other.CompareTag("Fence") && damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
            agent.isStopped = false; // Resume movement if desired
        }
    }

    private IEnumerator DamageWallOverTime(WallHealth wallHealth)
    {
        while (wallHealth != null && wallHealth.currentHealth > 0)
        {
            wallHealth.TakeDamage(5f); // Damage value per interval
            yield return new WaitForSeconds(1f); // Wait for 1 second before damaging again
        }
    }

    private void PerformAction()
    {
        // Your custom action here
        Debug.Log("Collided with a wall! Performing action...");
        // Add any additional functionality you want here
    }
}