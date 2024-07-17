using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;

public class MageAI : EnemyAI
{
    public GameObject SpellObject;
    public Transform SpellOrigin;
    protected override void Update(){
        base.Update();
        if (playerInSightRange&&playerInAttackRange){
            AttackPlayer();
        }
    }
    protected override void AttackPlayer()
    {
        //Debug.Log("Enemy is attacking");
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        if (!isDead)
        {
            transform.LookAt(player.position);
            transform.eulerAngles=new Vector3(0,transform.eulerAngles.y,0);
            //Quaternion.RotateTowards(this.transform.rotation, Player.transform.rotation, 111.0f) ;
            //anima.SetBool("Attack", true);

            if (!AlreadyAttacked){
                StartCoroutine(CastWithDelay());
            }
            //var health_script = Player.GetComponent<player_health>();
           //health_script.Instadeath();
        }

        // if(!AlreadyAttacked){

        //     //Logica pentru a da cu mainile in player


        //     AlreadyAttacked=true;
        //     Invoke(nameof(ResetAttack),timeBetweenAttacks);
        // }
    }

    IEnumerator CastWithDelay(){
        AlreadyAttacked=true;
        CastSpell();
        yield return new WaitForSeconds(timeBetweenAttacks);
        AlreadyAttacked=false;
    }
    public void CastSpell(){
        var spell = Instantiate(SpellObject,SpellOrigin.position,Quaternion.identity);
        spell.transform.forward=SpellOrigin.forward;
        spell.GetComponent<Rigidbody>().AddForce(transform.forward*32f,ForceMode.Impulse);
    }
    
}