using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : State
{
    [SerializeField]
    private Transform movePositionTransform;
    //private Enemy _enemy;
    private Animator _animator;
    private float oldSpeed;

    [Header("pathfinding")]
    public float minAttackDistance = 1f;
    public float maxAttackDistance = 0.5f;
    public float timer = 0.0f;
    public float maxTime = 1.0f;
    public float maxDistance = 1.0f;
    //private float distance = Vector3.Distance
    
    public float e_speed;
    public bool isPathfinding;


    [Header("Debug Path")]
    public bool velocity;
    public bool desiredVelocity;
    public bool path;

    public override void Awake(){
        _animator = GetComponent<Animator>();
    }

    public override void Enter() {
        //m_NavMeshAgent = GetComponent<NavMeshAgent>();
        //Debug.Log("ON");
        ChaseThePlayer();
        //Debug.Log(minAttackDistance);
        oldSpeed = _enemy.navMeshAgent.speed;
     }
    public override void Exit() {
        
     }
    public override void OnUpdate() {
        ChaseThePlayer();

        if(Input.GetKey(KeyCode.X)){
            myFSM.SetCurrentState(typeof(DeathState));
        }
     }

     private void OnDrawGizmos() {
        if(velocity){
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + _enemy.navMeshAgent.velocity);
        }

        if(desiredVelocity){
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + _enemy.navMeshAgent.desiredVelocity);
        }

        if(path){
            Gizmos.color = Color.yellow;
            var agentPath = _enemy.navMeshAgent.path;
            Vector3 prevCorner = transform.position;
            foreach(var corner in agentPath.corners){
                Gizmos.DrawLine(prevCorner, corner);
                Gizmos.DrawSphere(corner, 0.1f);
                prevCorner = corner;
            }
        }
    }

     public void ChaseThePlayer(){ 
        // Less performance heavy
        // By pathfingding being regulated evert second or so instaed of every frame
        // Does NOT look nice.

        // timer -= Time.deltaTime;
        // if(timer < 0.0f){
        //     float sqrDistance = (movePositionTransform.position - _enemy.navMeshAgent.destination).sqrMagnitude;
            
            
        //     if(sqrDistance > maxDistance * maxDistance){
        //         _animator.SetBool("isAttacking", false);
        //         _enemy.navMeshAgent.destination = movePositionTransform.position;
        //     }else if(sqrDistance <= 1f){
        //         //Do attack
        //         _animator.SetBool("isAttacking", true);
        //         //_animator.SetFloat("Speed", 0.0f);
        //     }
        //     timer = maxTime;
        // }
        
        float distance = Vector3.Distance(gameObject.transform.position, movePositionTransform.position);
        //Debug.Log(distance);
        
        // if (distance <= minAttackDistance)
        // {
        //     //DoAttack();
        //     _animator.SetBool("isAttacking", true);
        //     _enemy.navMeshAgent.destination = transform.position;
        //     _animator.SetFloat("Speed", 0f);
        // }else{
        //     _animator.SetBool("isAttacking", false);
        //     _enemy.navMeshAgent.destination = movePositionTransform.position;
        //     _animator.SetFloat("Speed", _enemy.navMeshAgent.velocity.magnitude);
        // }
        
        Debug.Log(distance);
        if(distance <= minAttackDistance){
            //_enemy.navMeshAgent.speed = 0;
            _enemy.navMeshAgent.isStopped = true;
            _animator.SetBool("isAttacking", true);
        }else if(distance <= maxAttackDistance){
            //_enemy.navMeshAgent.speed = 0;
            _enemy.navMeshAgent.isStopped = true;
          _enemy.rb.isKinematic = true;
        }else{
       //_enemy.navMeshAgent.speed = oldSpeed;
       _enemy.navMeshAgent.isStopped = false;
        _enemy.navMeshAgent.destination = movePositionTransform.position;
        _animator.SetFloat("Speed", _enemy.navMeshAgent.velocity.magnitude);
        _animator.SetBool("isAttacking", false);
        _enemy.rb.isKinematic = false;
        }
        
     }

}
