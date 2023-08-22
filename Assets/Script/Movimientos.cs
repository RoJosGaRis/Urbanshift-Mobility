using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimientos : MonoBehaviour
{
    [SerializeField] int speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if((transform.position.x < 10 && speed > 0) || (transform.position.x > 0 && speed < 0)){
            transform.position += new Vector3(speed* Time.deltaTime, 0);
        } else {
            speed = speed * -1;
        }
    }
}
