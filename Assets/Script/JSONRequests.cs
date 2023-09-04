using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class JSONRequests : MonoBehaviour
{
    [System.Serializable]
    public class Car{
        public string index;
        public int x;
        public int z;
    }
    [System.Serializable]
    public class Light{
        public string index;
        public int status;
    }
    [System.Serializable]
    public class MainList{
        public Car[] vehicleAgents;
        public Light[] lightAgents;
    }
    // [SerializeField] TextAsset[] JSONData = new TextAsset[2];
    [SerializeField] private GameObject car_prefab;
    private Dictionary<string, CarMovement> carDictionary = new Dictionary<string, CarMovement>();
    public MainList mainList = new MainList();
    public int count = 0;

    // Start is called before the first frame update
    void Update(){
        // if(Input.GetKeyDown(KeyCode.Space)){
        //     count %= JSONData.Length;
        //     Fetch();
        //     count++;
        // }
    }

    public void FetchDataStart(){
        StartCoroutine(FetchData());
    }

    public IEnumerator FetchData(){
        using (UnityWebRequest request = UnityWebRequest.Get("http://127.0.0.1:5000/step")){
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError){
                Debug.Log(request.error);
            }
            else {
                Debug.Log(request.downloadHandler.text);
                yield return mainList = JsonUtility.FromJson<MainList>(request.downloadHandler.text);
                // foreach(Car car in mainList.vehicleAgents){
                //     Debug.Log(car.index);
                // }
            }
            if(mainList.vehicleAgents != null){
                handleCars(mainList.vehicleAgents);
            }
        }
    }
    // void Fetch()
    // {
    //     // StartCoroutine(GetRequest(JSONData[i]));
    //     listaAutos = JsonUtility.FromJson<CarList>(JSONData[count].text);
    //     // Debug.Log(listaAutos.cars.Length);
    //     handleJSON(listaAutos);
    // }

    // Update is called once per frame
    void handleCars(Car[] listaAutos)
    {
        foreach (Car car in listaAutos){
            // Debug.Log(carDictionary.ContainsKey("car.index.ToString()"));
            if(carDictionary.ContainsKey(car.index.ToString())){
                carDictionary[car.index.ToString()].AddToList(new Tuple<float, float>(car.x * -62.3f, (car.z * 62.3f) - 30f));
            } else {
                GameObject newCar = Instantiate(car_prefab, new Vector3(car.x * -62.3f, 6, (car.z * 62.3f) - 30f), Quaternion.Euler(0,0,0)); 
                // newCar.transform.parent = transform;
                carDictionary.Add(car.index.ToString(), newCar.GetComponent<CarMovement>());
            //     // cars[car.index.ToString()].Appear(new Vector3((car.x * 62.3f), 6, (car.z * 62.3f) - 30f));
            //     // cars[car.index.ToString()].AddToList(new Tuple<int, int>(car.x, car.z));
            }
        }
    }
    void handleLights(Car[] listaAutos)
    {
        foreach (Car car in listaAutos){
            // Debug.Log(carDictionary.ContainsKey("car.index.ToString()"));
            if(carDictionary.ContainsKey(car.index.ToString())){
                carDictionary[car.index.ToString()].AddToList(new Tuple<float, float>(car.x * -62.3f, (car.z * 62.3f) - 30f));
            } else {
                GameObject newCar = Instantiate(car_prefab, new Vector3(car.x * -62.3f, 6, (car.z * 62.3f) - 30f), Quaternion.Euler(0,0,0)); 
                // newCar.transform.parent = transform;
                carDictionary.Add(car.index.ToString(), newCar.GetComponent<CarMovement>());
            //     // cars[car.index.ToString()].Appear(new Vector3((car.x * 62.3f), 6, (car.z * 62.3f) - 30f));
            //     // cars[car.index.ToString()].AddToList(new Tuple<int, int>(car.x, car.z));
            }
        }
    }
}
