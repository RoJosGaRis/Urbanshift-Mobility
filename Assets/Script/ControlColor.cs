using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlColor : MonoBehaviour
{
    [SerializeField] Light currentLight;
    int currentState = 0;

    // Start is called before the first frame update
    void Start()
    {
        // currentLight.color = new Color(0, 255, 0);
        StartCoroutine("testSwitch");
    }

    // Update is called once per frame
    IEnumerator testSwitch(){
        
        while(true){
            currentState = (currentState + 1) % 3;
            SetColor(currentState);
            yield return new WaitForSeconds(1f);
        }
    }

    void SetColor(int state){
        if(state == 0){
            currentLight.color = new Color(0, 255, 0);
            currentState = state;
        } else if(state == 1){
            currentLight.color = new Color(255, 255, 0);
            currentState = state;
        } else if(state == 2){
            currentLight.color = new Color(255, 0, 0);
            currentState = state;
        }
    }
}
