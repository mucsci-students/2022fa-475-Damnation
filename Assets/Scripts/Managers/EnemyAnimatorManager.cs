using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorManager : AnimatorManager
{
    EnemyLocoManager enemyLocoManager;
  private void Awake(){

    anim = GetComponent<Animator>();
    enemyLocoManager = GetComponent<EnemyLocoManager>();
  }

  private void OnAnimatorMove(){
    float delta = Time.deltaTime;
    enemyLocoManager.enemyRigidBody.drag = 0;
    Vector3 deltaPosition = anim.deltaPosition;
    deltaPosition.y = 0;
    Vector3 velocity = deltaPosition / delta;
    enemyLocoManager.enemyRigidBody.velocity = velocity;
  }
}
