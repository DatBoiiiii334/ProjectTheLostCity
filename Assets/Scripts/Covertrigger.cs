using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Covertrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other){
        
        if(other.tag == "Player"){
            print("I have touched "+other.name);
            other.GetComponent<ThirdPersonShooterController>()._nearCover = true;
        }
    }

    private void OnTriggerExit(Collider other){
        
        if(other.tag == "Player"){
            print("I have touched "+other.name);
            other.GetComponent<ThirdPersonShooterController>()._nearCover = false;
        }
    }
}
