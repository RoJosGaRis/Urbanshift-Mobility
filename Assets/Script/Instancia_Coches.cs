using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instancia_Coches : MonoBehaviour
{

    public GameObject PrefabCoche;
    public int cantidadCoches = 10;
    GameObject[] agents;

    // Start is called before the first frame update
    void Start()
    {
        agents = new GameObject[cantidadCoches];
        //int i = 1;
        //float x = Random.Range (-10, 10);
        //float y = 4.5f;
        //float z = Random.Range (-10, 10);
        //agents[i] = Instantiate(PrefabCoche, new Vector3(90, y, 90), Quaternion.Euler(0, 90, 0));

        for (int i = 0; i < cantidadCoches; i++){
            float x = Random.Range (-100, 100);
            float y = 4.5f;
            float z = Random.Range (-100, 100);
            float rot = Random.Range (0, 360);
            agents[i] = Instantiate(PrefabCoche, new Vector3(x, y, z), Quaternion.Euler(0, rot, 0));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
