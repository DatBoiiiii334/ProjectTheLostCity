using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DebugNavMeshAgent : MonoBehaviour
{
    public bool velocity;
    public bool desiredVelocity;
    public bool path;
    private Enemy _enemy;

    private void Start(){
        _enemy = GetComponent<Enemy>();
    }

    private void OnDrawGizmos() {
        if(velocity){
            Gizmos.color = Color.green;
            //Gizmos.DrawLine(transform.position, transform.position + _enemy.navMeshAgent.velocity);
        }
    }
}
