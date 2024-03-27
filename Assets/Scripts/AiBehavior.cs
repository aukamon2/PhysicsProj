using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AiBehavior : MonoBehaviour
{
    //public NavMeshAgent agent;

    public Transform player;
    public LayerMask Grounded, isPlayer;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Chase
    public float timeBetweenAttack;
    bool alreadyAttack;
    public GameObject projectile;

    public int health;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        //agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check if player in range
        playerInSightRange = Physics.CheckSphere(transform.position,sightRange,isPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, isPlayer);
       // if (!playerInSightRange && !playerInAttackRange) Patrolling();
        //if(playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
        
    }
    private void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();
        if (walkPointSet)
           // agent.SetDestination(walkPoint);
        
        //Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //if(distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x+randomX, transform.position.y, transform.position.z + randomZ);
        if(Physics.Raycast(walkPoint, -transform.up,2f, Grounded))
            walkPointSet = true;
    }
    private void ChasePlayer()
    {
        //agent.SetDestination(player.position);
    }
    private void AttackPlayer()
    {
        //agent.SetDestination(transform.position);
        transform.LookAt(player);
        if(!alreadyAttack)
        {
            

            alreadyAttack = true;
            Invoke(nameof(ResetAttack), timeBetweenAttack);
        }
    }
    private void ResetAttack()
    {
        alreadyAttack= false;

    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Invoke(nameof(DestroyEnemy), 0.5f);
        }
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
