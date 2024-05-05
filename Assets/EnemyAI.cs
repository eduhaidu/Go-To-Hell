using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    public LayerMask WhatIsGround, WhatIsPlayer;
    public float health;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool AlreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Q3Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        //Checks for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, WhatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, WhatIsPlayer);

        if(!playerInSightRange && !playerInAttackRange){
            Patroling();
        }
        if(playerInSightRange && !playerInAttackRange){
            Debug.Log("Player in sight range");
            ChasePlayer();
        }
        if(playerInSightRange&&playerInAttackRange){
            Debug.Log("Player in attack range");
            AttackPlayer();
        }
    }

    void Patroling(){
        if(!walkPointSet){
            SearchWalkPoint();
        }
        if(walkPointSet){
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude<1f){
            walkPointSet=false;
        }
    }
    
    void SearchWalkPoint(){
        //Calculate random point in range
        float randomZ=Random.Range(-walkPointRange, walkPointRange);
        float randomX=Random.Range(-walkPointRange, walkPointRange);

        walkPoint=new Vector3(transform.position.x+randomX,transform.position.y,transform.position.z+randomZ);

        if (Physics.Raycast(walkPoint,-transform.up,2f,WhatIsGround)){
            walkPointSet=true;
        }
    }

    void ChasePlayer(){
        agent.SetDestination(player.position);
    }

    void AttackPlayer(){
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if(!AlreadyAttacked){

            //Logica pentru a da cu mainile in player


            AlreadyAttacked=true;
            Invoke(nameof(ResetAttack),timeBetweenAttacks);
        }
    }
    void ResetAttack(){
        AlreadyAttacked=false;
    }

    public void TakeDamage(int damage){
        health-=damage;
        if (health<=0){
            Destroy(gameObject);
        }
    }

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
    
}
