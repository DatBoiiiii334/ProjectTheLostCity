using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : State
{
    //private Enemy _enemy;
    private Animator _animator;

    public override void Enter(){
        //_enemy = GetComponent<Enemy>();
        _animator = GetComponent<Animator>();
        _enemy.navMeshAgent.destination = transform.position;
        _animator.SetFloat("Speed", 0.0f);
        _enemy.animator.SetBool("isAttacking", false);
        _enemy.animator.SetBool("isDead", true);
        //Debug.Log("I am dead");
    }

    public override void OnUpdate(){
        
    }

    public override void Exit(){
        
    }
}
