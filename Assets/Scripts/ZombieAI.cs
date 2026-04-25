using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    [Header("Stats")]
    public float attackRange = 1.5f;
    public int damage = 15;
    public float attackRate = 1f;

    private float nextAttackTime = 0f;
    private NavMeshAgent agent;
    private transform player;

    public enum ZombieState { Chasing, Attacking, Dead }
    public ZombieState state = ZombieState.Chasing;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavmeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == ZombieState.Dead) return;

        float distFromPlr = Vector3.Distance(transform.position, player.position);

        if (distFromPlr <= attackRange) 
        {
            state = ZombieState.Attacking;
            agent.SetDestination(transform.position;) // stop moving when attacking.
            Attack();
        } 
        else 
        {
            state = ZombieState.Chasing;
            agent.SetDestination(player.position); // chase again.
        }
    }

    void Attack()
    {
        if (Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + attackRate;
            Debug.Log("zombie attacks")

            // hook to player health later...
        }
    }

    public void onDeath()
    {
        state = ZombieState.Dead;
        agent.SetDestination(transform.position)
        Debug.log("Zombie died.");
        Destroy(GameObject, 2f); // room for animation, if needed
    }
}
