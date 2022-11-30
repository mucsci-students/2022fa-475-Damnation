using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyLocoManager : MonoBehaviour
{   
    EnemyManager enemyManager;
    public LayerMask detectionLayer;

    public CharacterStats currentTarget;
    // Start is called before the first frame update
    private void Awake()
    {
        enemyManager = GetComponent<EnemyManager>();
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
}
