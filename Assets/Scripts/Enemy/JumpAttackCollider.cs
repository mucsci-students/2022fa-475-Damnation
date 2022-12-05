using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAttackCollider : MonoBehaviour
{
  public GameObject damageCollider;
  Animator anim;
  
  void Start()
  {
    anim = GetComponent<Animator>();
  }

  void Update()
  {
    handleHitbox(anim.GetBool("JumpAttack"));
  }

  public void handleHitbox(bool status)
  {
    damageCollider.SetActive(status);
  }

}
