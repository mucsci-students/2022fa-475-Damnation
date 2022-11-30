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

    EnemyLocoManager enemyLocoManager;
    // Start is called before the first frame update
    private void Awake()
    {
        enemyLocoManager = GetComponent<EnemyLocoManager>();
    }

    // Update is called once per frame
    private void Update()
    {
    }
    private void FixedUpdate(){
        HandleCurrentAction();
    }

    public void HandleCurrentAction(){
        if(enemyLocoManager.currentTarget == null){
            enemyLocoManager.HandleDetection();
        }
        else{
            enemyLocoManager.HandleMoveToTarget();
        }
    }

       private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red; //replace red with whatever color you prefer
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
        }
}
