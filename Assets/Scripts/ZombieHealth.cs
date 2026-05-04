using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    public int health = 60;

    private ZombieAI ai;
    private WaveSystem waveSystem;

    void Start()
    {
        ai = GetComponent<ZombieAI>();
        waveSystem = FindFirstObjectByType<WaveSystem>();
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        //Debug.Log(gameObject.name + " took " + amount + " damage. HP remaining: " + health);

        if (health <= 0) Die();
    }

    void Die()
    {
        //Debug.Log(gameObject.name + " died.");

        // Notify wave system
        if (waveSystem != null) waveSystem.OnZombieDied();

        // Notify AI to enter dead state
        if (ai != null) ai.onDeath();
        else Destroy(gameObject);
    }
}
