using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
  public float Health = 100;
  public float HeadShotMultiplier = 1.5f;
  public float BodyShotMultiplier = 1.0f;
  public float LegShotMultiplier = 0.75f;
  public float zombieDamage = 20f;

  public bool canAttack = true;

  [SerializeField] float turnSpeed = 5;
  [SerializeField] float attackTime = 2f;

  private Animator animator;
  private NavMeshAgent agent;
  private CharacterController characterController; 

  int headshotPoints = 15; 
  int bodyshotPoints = 10; 
  int legshotPoints = 5; 
  int killPoints = 50; 

  Transform target; 

  private void Start() {
    animator = this.GetComponent<Animator>();
    agent = GetComponent<NavMeshAgent>();
    characterController = this.GetComponent<CharacterController>();

    target = GameObject.FindGameObjectWithTag("Player").transform;
    StartCoroutine(UpdateTarget());

  }

  //Am using this so the zombies can be trained and 
  // they won't rush me :sob:
  IEnumerator UpdateTarget(){
    while(animator.GetBool("isDead") == false){
      float distance = Vector3.Distance(transform.position, target.position);
      if (distance > 10){
        ChasePlayer(2); 
      }
      else if (distance > 2){
        ChasePlayer(1); 
      }
      yield return new WaitForSecondsRealtime(3.0f);
    }

    if(animator.GetBool("isDead") == true){
      Scoring.addPoints(killPoints);
    }
  }

  void Update() 
  {

    if (Health <= 0){
      animator.SetBool("isDead", true);
    }
    
    float distance = Vector3.Distance(transform.position, target.position);
    if (distance <= 2 && canAttack && !PlayerHealth.singleton.isDead)
    {
      AttackPlayer();
    } else if (PlayerHealth.singleton.isDead)
    {
      DisableEnemy();
    }

    animator.SetFloat("speed", agent.speed);

  }

  public void OnDeath(){
    this.GetComponent<CharacterController>().enabled = false;
    agent.isStopped = true; 
    Scoring.addPoints(killPoints);
    StartCoroutine(FadeToDestroy(0.2f));
  }
  IEnumerator FadeToDestroy(float timeToDestroy){
    yield return new WaitForSecondsRealtime(timeToDestroy); 
    // SkinnedMeshRenderer[] meshes = transform.GetComponentsInChildren<SkinnedMeshRenderer>();
    // Color[] colors = meshes.Select(mesh => mesh.materials[0].color).ToArray(); 
    // for (int i = 0; i < 10; i++){
    //   for (int j = 0; j < meshes.Length; j++){
    //     colors[j].a -= 0.1f; 
    //     meshes[j].materials[0].color = colors[j]; 
    //   }

    //   yield return new WaitForFixedUpdate(); 
    // }
    Destroy(gameObject);
  }
  
  public void OnHit(BodySection bodySection, float damage){
    switch(bodySection){
      case BodySection.Head: 
        Health -= HeadShotMultiplier* damage;
        Scoring.addPoints(headshotPoints);
        break; 
      case BodySection.Body: 
        Health -= BodyShotMultiplier* damage;
        Scoring.addPoints(bodyshotPoints);
        break; 
      case BodySection.Legs: 
        Health -= LegShotMultiplier *damage;
        Scoring.addPoints(legshotPoints);
        break;
    }

    //animator.SetTrigger("onHit");
  }

  void ChasePlayer(float speed){
      agent.updateRotation = true;
      agent.updatePosition = true;
      agent.speed = speed;
      agent.SetDestination(target.transform.position);
      animator.SetBool("isWalking", true);
      animator.SetBool("isAttacking", false);
  }

  void AttackPlayer()
  {
      agent.updateRotation = false; 
      Vector3 direction = target.position - transform.position;
      direction.y = 0;
      transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);
      agent.updatePosition = false;
      agent.isStopped = true; 
      animator.SetBool("isWalking", false);
      animator.SetBool("isAttacking", true);
      StartCoroutine(AttackTime());
  }

  void DisableEnemy()
  {
    canAttack = false; 
     animator.SetBool("isWalking", false);
     animator.SetBool("isAttacking", false);
     agent.isStopped = true;   
  }

  IEnumerator AttackTime()
  {
    canAttack = false; 
    yield return new WaitForSeconds(1f);
    PlayerHealth.singleton.DamagePlayer(zombieDamage);
    yield return new WaitForSeconds(attackTime);
    canAttack = true;
    animator.SetBool("isAttacking", false); 
  }

 
}
