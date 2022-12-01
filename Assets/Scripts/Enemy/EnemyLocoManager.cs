using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLocoManager : MonoBehaviour
{   
    NavMeshAgent navMeshAgent;
    public Rigidbody enemyRigidBody;
    EnemyManager enemyManager;
    public LayerMask detectionLayer;
    EnemyAnimatorManager enemyAnimManager;

    public float distanceFromTarget;
    public float stoppingDistance = 2;

    public float rotationSpeed = 15;

    public CharacterStats currentTarget;
    // Start is called before the first frame update
    private void Awake()
    {
        enemyManager = GetComponent<EnemyManager>();
        enemyAnimManager = GetComponent<EnemyAnimatorManager>();
        navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        enemyRigidBody = GetComponent<Rigidbody>();
    }
    private void Start(){
        navMeshAgent.enabled = false;
        enemyRigidBody.isKinematic = false;
    }

   public void HandleDetection(){
    Collider[] colliders = Physics.OverlapSphere(transform.position, enemyManager.detectionRadius, detectionLayer);  

    for(int i = 0; i < colliders.Length; ++i){
        CharacterStats characterStats = colliders[i].transform.GetComponent<CharacterStats>();
        if (characterStats == null){
            characterStats = colliders[i].transform.GetComponentInChildren<CharacterStats>();
        }
        if (characterStats == null){
         characterStats = colliders[i].transform.GetComponentInParent<CharacterStats>();
        }
        if(characterStats != null){
            //check for team ID

            Vector3 targetDirection = characterStats.transform.position - transform.position;
            float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

            if(viewableAngle > enemyManager.minimumDetectionAngle
             && viewableAngle < enemyManager.maximumDetectionAngle){
                currentTarget = characterStats;
             }
        }
    } 
   }

   public void HandleMoveToTarget(){

        if(enemyManager.isPerformingAction){
            return;
        }
    Vector3 targetDirection = currentTarget.transform.position - transform.position;
    distanceFromTarget = Vector3.Distance(currentTarget.transform.position, transform.position);
    float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

    if(enemyManager.isPerformingAction){
        enemyAnimManager.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
        navMeshAgent.enabled = false;
    }
    else{
        if(distanceFromTarget > stoppingDistance){
            enemyAnimManager.anim.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);

            
            //targetDirection.Normalize();
            //targetDirection.y = 0;

            //float speed = 2;
            //targetDirection *= speed;
            //Vector3 projectedVelocity = Vector3.ProjectOnPlane(targetDirection, Vector3.up);
            //enemyRigidBody.velocity = projectedVelocity;
        
        }
        else if(distanceFromTarget <= stoppingDistance){
            enemyAnimManager.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
        }
    }



    HandleRotateTowardsTarget();
    transform.position = new Vector3(navMeshAgent.transform.position.x, navMeshAgent.transform.position.y, navMeshAgent.transform.position.z);
    navMeshAgent.transform.localPosition = Vector3.zero;
    navMeshAgent.transform.localRotation = Quaternion.identity;
   }

   private void HandleRotateTowardsTarget(){
    //rotate manually
    if(enemyManager.isPerformingAction){
        Vector3 direction = currentTarget.transform.position - transform.position;
        direction.y = 0;
        direction.Normalize();
        
        if(direction == Vector3.zero){
            direction = transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed / Time.deltaTime);
    }
    //rotate with pathfinding
    else{
        Vector3 relativeDirection = transform.InverseTransformDirection(navMeshAgent.desiredVelocity);
        Vector3 targetVelocity = enemyRigidBody.velocity;

        navMeshAgent.enabled = true;
        navMeshAgent.SetDestination(currentTarget.transform.position);
        enemyRigidBody.velocity = targetVelocity;
        transform.rotation = Quaternion.Slerp(transform.rotation, navMeshAgent.transform.rotation, rotationSpeed / Time.deltaTime);
    }
    
   }
}
