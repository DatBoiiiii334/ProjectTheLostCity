using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public static Enemy _instance;
    public FSM myFSM;
    public NavMeshAgent navMeshAgent;
    public Animator animator;
    public Rigidbody rb;

    [Header("Health Enemy")]
    public int enemyHp = 10;

    [Header("pathfinding")]
    public float timer = 0.0f;
    public float maxTime = 1.0f;
    public float maxDistance = 1.0f;

    public void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        //myFSM = GetComponent<FSM>();
        

        State[] myStatearray = GetComponents<State>();
        foreach (State state in myStatearray)
        {
            myFSM.Add(state.GetType(), state);
        }
        myFSM.SetCurrentState(typeof(SpawnState));
    }

    public void TakeDamage(int damage){
        enemyHp -= damage;
    }

    public void Update(){
        if(enemyHp <= 0){
            myFSM.SetCurrentState(typeof(DeathState));
        }
    }

    // private void Awake()
    // {
    //     if (_instance != null)
    //     {
    //         Destroy(gameObject);
    //     }else{
    //         _instance = this;
    //     }
    // }
}
