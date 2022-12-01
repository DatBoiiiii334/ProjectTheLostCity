using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyToSpawn;

    void Start(){
        SpawnEnemy();
    }

    public void SpawnEnemy(){
        Instantiate(enemyToSpawn, transform.position, transform.rotation);
        enemyToSpawn.SetActive(true);
    }
}
