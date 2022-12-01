using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{   
    public bool isPerformingAction;
    [Header("A.I Settings")]
    public float detectionRadius = 20;
    //higher, and lower, change field of view
    public float maximumDetectionAngle = 50;
    public float minimumDetectionAngle = -50;

    public EnemyAttackAction[] enemyAttacks;
    public EnemyAttackAction currentAttack;

    EnemyLocoManager enemyLocoManager;
    EnemyAnimatorManager enemyAnimManager;

    public float currentRecoveryTime = 0;
    // Start is called before the first frame update

    private void Awake()
    {
        enemyLocoManager = GetComponent<EnemyLocoManager>();
        enemyAnimManager = GetComponent<EnemyAnimatorManager>();
        
    }

    // Update is called once per frame
    private void Update()
    {
        HandleRecoveryTime();
    }
    private void FixedUpdate(){
        HandleCurrentAction();
    }

    private void HandleCurrentAction(){
        if(enemyLocoManager.currentTarget != null){
            enemyLocoManager.distanceFromTarget = Vector3.Distance(enemyLocoManager.currentTarget.transform.position, transform.position);

        }
        
        if(enemyLocoManager.currentTarget == null){
            enemyLocoManager.HandleDetection();
        }
        else if(enemyLocoManager.distanceFromTarget > enemyLocoManager.stoppingDistance){
            enemyLocoManager.HandleMoveToTarget();
        }
        else if(enemyLocoManager.distanceFromTarget <= enemyLocoManager.stoppingDistance){
            AttackTarget();

        }
    }

    private void HandleRecoveryTime(){

        if(currentRecoveryTime > 0){
            currentRecoveryTime -= Time.deltaTime;
        }

        if(isPerformingAction){

            if(currentRecoveryTime <= 0){

                isPerformingAction = false;
            }
        }
    }
    #region Attacks

    private void GetNewAttack(){
        Vector3 targetsDirection = enemyLocoManager.currentTarget.transform.position - transform.position;
        float viewableAngle = Vector3.Angle(targetsDirection, transform.forward);
        enemyLocoManager.distanceFromTarget = Vector3.Distance(enemyLocoManager.currentTarget.transform.position,   transform.position);

        int maxScore = 0;

        for(int i = 0; i < enemyAttacks.Length; ++i){

            EnemyAttackAction enemyAttackAction = enemyAttacks[i];

            if(enemyLocoManager.distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
            && enemyLocoManager.distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack){

                maxScore += enemyAttackAction.attackScore;

            }
        }

        int randomValue = Random.Range(0, maxScore);
        int temporaryScore = 0;

        for(int i = 0; i < enemyAttacks.Length; ++i){

            EnemyAttackAction enemyAttackAction = enemyAttacks[i];

            if(enemyLocoManager.distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
            && enemyLocoManager.distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack){

                if(currentAttack != null){
                    return;
                }
                temporaryScore += enemyAttackAction.attackScore;

                if(temporaryScore > randomValue){

                    currentAttack = enemyAttackAction;
                }

            }
        }


    }
    #endregion
    
    private void AttackTarget(){
        if(isPerformingAction)
        {
            return;
        }
        

        if(currentAttack == null){

            GetNewAttack();
        }
        else{
            isPerformingAction = true;
            currentRecoveryTime = currentAttack.recoveryTime;
            enemyAnimManager.PlayTargetAnimation(currentAttack.actionAnimation, true);
            currentAttack = null;
        }
    }
       private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red; //replace red with whatever color you prefer
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
        }
}
