using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceCar : MonoBehaviour
{
    [SerializeField] private GameObject car_prefab;
    [SerializeField] private int car_count = 1;
    [SerializeField] private List<GameObject> car_list = new List<GameObject>();
    [SerializeField] private Vector3 lastPosition;
    [SerializeField] private Quaternion lastRotation;
    // Start is called before the first frame update
    void Start()
    {
        Clear();
    }

    void Clear(){

        lastPosition = new Vector3 (0,13.3f,0);
        lastRotation = transform.rotation;
        foreach (GameObject car in car_list)
        {
            Destroy(car);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C)){
            Clear();
        }
        if(Input.GetKeyDown(KeyCode.W)){
            lastPosition = lastPosition + new Vector3 (100,0,0);
            lastRotation = Quaternion.Euler(0,0,0);
            CreateCar();
        }     
        if(Input.GetKeyDown(KeyCode.S)){
            lastPosition = lastPosition + new Vector3 (-100,0,0);
            lastRotation = Quaternion.Euler(0,180,0);
            CreateCar();
        }
        if(Input.GetKeyDown(KeyCode.A)){
            lastPosition = lastPosition + new Vector3 (0,0,100);
            lastRotation = Quaternion.Euler(0,-90,0);
            CreateCar();
        }
        if(Input.GetKeyDown(KeyCode.D)){
            lastPosition = lastPosition + new Vector3 (0,0,-100);
            lastRotation = Quaternion.Euler(0,90,0);
            CreateCar();
        } 
        
    }

    public void CreateCar(){
        GameObject newCar = Instantiate(car_prefab, lastPosition, lastRotation); 
        newCar.transform.parent = transform;
        car_list.Add(newCar);
        car_count++;
    }
}
