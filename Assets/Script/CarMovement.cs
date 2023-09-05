using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] float speed = 0.1f;
    Vector3 target = new Vector3(-1,-1,-1);
    Vector3 targetAngle = new Vector3(-1,-1,-1);
    bool isMoving = false;
    List<Tuple<float, float>> posiciones = new List<Tuple<float, float>>();
    // Vector3 startPosition = new Vector3(5.5f,1.3f,5f);

    public void Appear(Vector3 startPosition){
        transform.position = startPosition;
    }

    // void Update(){
    //     Move();
    // }

    public void AddToList(Tuple<float, float> newPosition){
        if(target != new Vector3(-1,-1,-1) && targetAngle != new Vector3(-1,-1,-1)){
            transform.position = new Vector3(target.x , 6, target.z);
            transform.eulerAngles = targetAngle;
        }
        posiciones.Add(newPosition);
        Move();
    }
    // Update is called once per frame
    public void Move()
    {
        if(posiciones.Count == 0 || posiciones == null){
            posiciones.RemoveAt(0);
            return;
        } else {
            // StopAllCoroutines();
            isMoving = true;
            float currentCoordX = posiciones[0].Item1;
            float currentCoordZ = posiciones[0].Item2;
            // StopCoroutine(Move");
            
            Vector3 currentCoordVector = new Vector3(currentCoordX , 6, currentCoordZ);
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
        targetAngle = transform.eulerAngles;
        Vector3 startPosition = transform.position;

        startAngle.y = Mathf.Floor(startAngle.y);

        if(startAngle.y == -90){
            startAngle.y = 270;
        } else if(startAngle.y == 360){
            startAngle.y = 0;
        }

        if(target.x > startPosition.x && startAngle.y == 90){
            targetAngle = startAngle + new Vector3(0,-90,0);
        } else if(target.x > startPosition.x && startAngle.y == 270){
            targetAngle = startAngle + new Vector3(0,90,0);
        } else if(target.x < startPosition.x && startAngle.y == 90){
            targetAngle = startAngle + new Vector3(0,90,0);
        } else if(target.x < startPosition.x && startAngle.y == 270){
            targetAngle = startAngle + new Vector3(0,-90,0);
        } else if(target.z > startPosition.z && startAngle.y == 0){
            targetAngle = startAngle + new Vector3(0,-90,0);
        } else if(target.z > startPosition.z && startAngle.y == 180){
            targetAngle = startAngle + new Vector3(0,90,0);
        } else if(target.z < startPosition.z && startAngle.y == 0){
            targetAngle = startAngle + new Vector3(0,90,0);
        } else if(target.z < startPosition.z && startAngle.y == 180){
            targetAngle = startAngle + new Vector3(0,-90,0);
        }
        
        float time = 0;
        
        while (transform.position != target)
        {
            transform.position = Vector3.Lerp(startPosition, target, time * 2 * speed);
            transform.eulerAngles = Vector3.Lerp(startAngle, targetAngle, time * 2 * speed);
            time += Time.deltaTime;
            yield return new WaitForSeconds(0.001f);
        }

        isMoving = false;
    }
}
