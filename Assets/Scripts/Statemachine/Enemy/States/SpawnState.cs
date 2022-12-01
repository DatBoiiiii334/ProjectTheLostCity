using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnState : State
{   
    [Header("Spawn related")]
    [SerializeField] private float crawlSpawnInTime = 0.2f;
    [SerializeField] private float climbSpawnInTime = 0.4f;
    private float spawnInTime;
    public bool climbSpawn;
    public bool crawlSpawn;
    private string crawl = "isCrawling";
    private string climb = "isClimbing";
    private string animationBool;
    private Animator _animator;

    public override void Awake(){
        _animator = GetComponent<Animator>();
    }

    public override void Enter(){
        //play spawn in animation
        //When done transition to idle / chase state

        if(climbSpawn){
            animationBool = climb;
            spawnInTime = climbSpawnInTime;
        }else if(crawlSpawn){
            animationBool = crawl;
            spawnInTime = crawlSpawnInTime;
        }else{
            Debug.LogError("No animator spawn Bool chosen!");
        }

        StartCoroutine(PlaySpawnAnimation());
    }
    public override void OnUpdate(){
        
    }
    public override void Exit(){
        StopCoroutine(PlaySpawnAnimation());
    }

    private IEnumerator PlaySpawnAnimation(){

        _animator.SetBool(animationBool, true);
        _animator.applyRootMotion = true;

        yield return new WaitForSeconds(spawnInTime);

        _animator.SetBool(animationBool, false);
        _animator.applyRootMotion = false;
        
        myFSM.SetCurrentState(typeof(ChaseState));
    }
}
