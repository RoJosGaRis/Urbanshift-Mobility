using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoMoveTowards : MonoBehaviour
{
    int speed = 5;
    Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        target = new Vector3(0, 10, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
        if(transform.position.x < 10 && target == new Vector3(0,10,0)){
           transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        } else if(transform.position.x > 0 && target == new Vector3(0,0,0)){
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0,0,0), speed * Time.deltaTime);
        } else {
            if(target == new Vector3(0,10,0)){
                target = new Vector3(0,0,0);
            } else {
                target = new Vector3(0,10,0);
            }
        }
    }
}
