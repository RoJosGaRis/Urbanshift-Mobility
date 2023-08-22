using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MovimientoLerp : MonoBehaviour
{
    [SerializeField] float speed = 0.1f;
    Vector3 target;
    List<Tuple<int, int>> posiciones = new List<Tuple<int, int>>();
    Vector3 startPosition = new Vector3(5.5f,1.3f,5f);

    void Start()
    {
        
        posiciones.Add(new Tuple<int, int>(0, 1));
        posiciones.Add(new Tuple<int, int>(0, 2));
        posiciones.Add(new Tuple<int, int>(0, 3));
        posiciones.Add(new Tuple<int, int>(1, 3));
        posiciones.Add(new Tuple<int, int>(2, 3));
        posiciones.Add(new Tuple<int, int>(3, 3));
        posiciones.Add(new Tuple<int, int>(3, 2));
        posiciones.Add(new Tuple<int, int>(3, 1));
        posiciones.Add(new Tuple<int, int>(3, 0));
        posiciones.Add(new Tuple<int, int>(2, 0));
        posiciones.Add(new Tuple<int, int>(1, 0));
        posiciones.Add(new Tuple<int, int>(0, 0));
        
        posiciones.Add(new Tuple<int, int>(0, 1));
        posiciones.Add(new Tuple<int, int>(0, 2));
        posiciones.Add(new Tuple<int, int>(0, 3));
        posiciones.Add(new Tuple<int, int>(1, 3));
        posiciones.Add(new Tuple<int, int>(2, 3));
        posiciones.Add(new Tuple<int, int>(3, 3));
        posiciones.Add(new Tuple<int, int>(3, 4));
        posiciones.Add(new Tuple<int, int>(3, 5));
        posiciones.Add(new Tuple<int, int>(3, 6));
        posiciones.Add(new Tuple<int, int>(2, 6));
        posiciones.Add(new Tuple<int, int>(1, 6));
        posiciones.Add(new Tuple<int, int>(0, 6));
        
        target = new Vector3(posiciones[0].Item1 * 10 + 5.5f, 1.3f, posiciones[0].Item2 * 10 + 5f);
        StartCoroutine(Move(target));
        
    }

    // Update is called once per frame
    void Update()
    {
        if(posiciones.Count == 0 || posiciones == null || transform.position != target){
            return;
        } else {
            int currentCoordX = posiciones[0].Item1;
            int currentCoordZ = posiciones[0].Item2;
            Vector3 currentCoordVector = new Vector3((currentCoordX * 10) + 5.5f , 1.3f, (currentCoordZ * 10) + 5f);
            target = currentCoordVector;
            posiciones.RemoveAt(0);
            StartCoroutine(Move(target));
        }


    }

    IEnumerator Move(Vector3 target)
    {
        if(transform.position == target){
            yield break;
        }
        //yield return new WaitForSeconds(0.5f);
        Vector3 startAngle = transform.eulerAngles;
        Vector3 targetAngle = transform.eulerAngles;
        Vector3 startPosition = transform.position;

        startAngle.y = Mathf.Floor(startAngle.y);

        if(startAngle.y == -90){
            startAngle.y = 270;
        } else if(startAngle.y == 360){
            startAngle.y = 0;
        }
        
        Debug.Log(startAngle.y);

        if(target.x > startPosition.x && startAngle.y == 90){
            Debug.Log("1");
            targetAngle = startAngle + new Vector3(0,-90,0);
        } else if(target.x > startPosition.x && startAngle.y == 270){
            targetAngle = startAngle + new Vector3(0,90,0);
            Debug.Log("2");
        } else if(target.x < startPosition.x && startAngle.y == 90){
            targetAngle = startAngle + new Vector3(0,90,0);
            Debug.Log("3");
        } else if(target.x < startPosition.x && startAngle.y == 270){
            targetAngle = startAngle + new Vector3(0,-90,0);
            Debug.Log("4");
        } else if(target.z > startPosition.z && startAngle.y == 0){
            targetAngle = startAngle + new Vector3(0,-90,0);
            Debug.Log("5");
        } else if(target.z > startPosition.z && startAngle.y == 180){
            targetAngle = startAngle + new Vector3(0,90,0);
            Debug.Log("6");
        } else if(target.z < startPosition.z && startAngle.y == 0){
            targetAngle = startAngle + new Vector3(0,90,0);
            Debug.Log("7");
        } else if(target.z < startPosition.z && startAngle.y == 180){
            targetAngle = startAngle + new Vector3(0,-90,0);
            Debug.Log("8");
        }
        
        Debug.Log(targetAngle);
        float time = 0;
        
        while (transform.position != target)
        {
            transform.position = Vector3.Lerp(startPosition, target, time * speed);
            transform.eulerAngles = Vector3.Lerp(startAngle, targetAngle, time * speed);
            time += Time.deltaTime;
            yield return null;
        }
    }
}
