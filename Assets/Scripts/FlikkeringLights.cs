using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlikkeringLights : MonoBehaviour
{
    public bool isFlickering;
    public float timeDelay;
    private Light myLight;

    private void Start(){
        myLight = GetComponent<Light>();
    }

    private void Update(){
        if(!isFlickering){
            StartCoroutine(FlickeringLight());
        }
    }

    IEnumerator FlickeringLight(){
        isFlickering = true;
        myLight.enabled = false;
        timeDelay = Random.Range(0.01f, 0.2f);
        yield return new WaitForSeconds(timeDelay);
        myLight.enabled = true;
        timeDelay = Random.Range(0.01f, 0.2f);
        yield return new WaitForSeconds(timeDelay);
        isFlickering = false;
    }
}
