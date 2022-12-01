using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    protected Enemy _enemy;

    public FSM myFSM {get; set;}
    public virtual void Enter() { }
    public virtual void Awake() { }
    public virtual void Exit() { }
    public virtual void OnUpdate() { }
    public void FixedUpdate() { }

    protected void Start() {
        _enemy = GetComponent<Enemy>();
    }
}
