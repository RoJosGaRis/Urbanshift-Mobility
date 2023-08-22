using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ejes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       
        Debug.DrawLine(Vector3.zero, new Vector3(10, 0, 0), Color.red);

        Debug.DrawLine(Vector3.zero, new Vector3(0, 10, 0), Color.green);
        Debug.DrawLine(Vector3.zero, new Vector3(0, 0, 10), Color.blue);
    }
}
