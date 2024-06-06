using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;

public class EnemyAI : MonoBehaviour
{
    public GameObject Player;
    //ANIMATOR VARIABLES
    public Animator anima; //The animator attached to the enemy

    //Navmesh variables
    public NavMeshAgent enemynavmesh;
    public float ChaseSpeed = 1.0f;

    public Transform player;
    private NavMeshAgent agent;
    public LayerMask WhatIsGround, WhatIsPlayer;
    public float health = 100;
    public int enemyDamage=60;
    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;

    bool isDead=false;
    //public float attackDist; //At what dist from player can it attack?

    //Gore
    public ParticleSystem GoreParticle;
    public GameObject GoreModel;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    // Start is called before the first frame update

    //Decal
    public DecalProjector DecalProjectorPrefab;

    void Start()
    {
        player = GameObject.Find("Q3Player").transform;
        agent = GetComponent<NavMeshAgent>();

        //Assign navmesh speed based on variable
        enemynavmesh = GetComponent<NavMeshAgent>();
        enemynavmesh.speed = ChaseSpeed;
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
           // Debug.Log("Player in sight range");
            ChasePlayer();
        }
        if(playerInSightRange&&playerInAttackRange){
            //Debug.Log("Player in attack range");
            AttackPlayer();
        }
        if(health<=0){
            isDead=true;
            Die();
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
        //Change to RUN animation
        anima.SetBool("isChasing", true);
        anima.SetBool("Attack",false);
    }

   
    void AttackPlayer()
    {
        //Debug.Log("Enemy is attacking");
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        if (!isDead)
        {
            transform.LookAt(player);
            //Quaternion.RotateTowards(this.transform.rotation, Player.transform.rotation, 111.0f) ;
            anima.SetBool("Attack", true);
            //var health_script = Player.GetComponent<player_health>();
           //health_script.Instadeath();
        }

        // if(!AlreadyAttacked){

        //     //Logica pentru a da cu mainile in player


        //     AlreadyAttacked=true;
        //     Invoke(nameof(ResetAttack),timeBetweenAttacks);
        // }
    }

    void ExecuteDie()
    {
        if (playerInAttackRange)
        {
            Debug.Log("Execute die ");
            var health_script = Player.GetComponent<player_health>();
            health_script.playerHealth-=enemyDamage;
            if(health_script.playerHealth<=0){
                health_script.PlayerDeath();
            }
            anima.SetBool("Attack", false);
        }

        }

   // public void TakeDamage(int damage){
      //  health-=damage;
      //  if (health<=0){
            //Destroy(gameObject);
       // }
   // }

    void Die(){
        agent.enabled=false;
        enemynavmesh.enabled=false;
        transform.LookAt(null);
        attackRange=0;
        this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
        if (health > -1)
        {
            anima.SetBool("Die", true);
        }
        if (health < -1)
        {

            Instantiate(GoreModel,new Vector3(this.gameObject.transform.position.x,this.gameObject.transform.position.y+1.5f,this.gameObject.transform.position.z),Quaternion.identity);
            Instantiate(GoreParticle, this.gameObject.transform.position, Quaternion.identity);
            
            //DECAL STUFF
            // Creates a new material instance for the DecalProjector.
            var myDecalInstance = Instantiate(DecalProjectorPrefab,new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y+1, this.gameObject.transform.position.z),DecalProjectorPrefab.transform.rotation);
            myDecalInstance.material = new Material(DecalProjectorPrefab.material);


            Destroy(this.gameObject);
        }
            Debug.Log("Enemy killed");

        }

        void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
    
}
