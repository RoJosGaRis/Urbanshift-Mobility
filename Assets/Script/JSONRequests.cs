using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
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
    private Dictionary<string, CarMovement> varCarDictionary = new Dictionary<string, CarMovement>();
    private Dictionary<string, ControlColor> lightDictionary = new Dictionary<string, ControlColor>();
    [SerializeField]private List<ControlColor> lights = new List<ControlColor>();
    public MainList mainList = new MainList();
    public int count = 0;

    // Start is called before the first frame update
    // void Start(){
        
    // }

    void Start(){
        for (int i = 0; i < lights.Count; i++){
            lightDictionary.Add(lights[i].index, lights[i]);
        }
    }

    public void FetchDataStart(){
        StartCoroutine(FetchData());
    }

    public void ResetSimStart(){
        StartCoroutine(ResetSim());
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
                handleLights(mainList.lightAgents);
            }
        }
    }

    public IEnumerator ResetSim(){
        using (UnityWebRequest request = UnityWebRequest.Get("http://127.0.0.1:5000/reset")){
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError){
                Debug.Log(request.error);
            } else {
                for (int i = 0; i < carDictionary.Count; i++){
                    Destroy(carDictionary.ElementAt(i).Value.gameObject);
                }
                carDictionary.Clear();
            }
        }
    }
    // void Fetch()
    //     // StartCoroutine(GetRequest(JSONData[i]));
    // {
    //     listaAutos = JsonUtility.FromJson<CarList>(JSONData[count].text);
    //     // Debug.Log(listaAutos.cars.Length);
    //     handleJSON(listaAutos);
    // }

    // Update is called once per frame
    void handleCars(Car[] listaAutos)
    {
        varCarDictionary.Clear();
        for(int i = 0; i < carDictionary.Count; i++){
            varCarDictionary.Add(carDictionary.ElementAt(i).Key, carDictionary.ElementAt(i).Value);
        }
    
        foreach (Car car in listaAutos){
            // Debug.Log(carDictionary.ContainsKey("car.index.ToString()"));
            if(carDictionary.ContainsKey(car.index.ToString())){
                varCarDictionary.Remove(car.index.ToString());
                carDictionary[car.index.ToString()].AddToList(new Tuple<float, float>(car.x * -62.3f, (car.z * 62.3f) - 30f));
            } else {
                GameObject newCar;
                if(car.x == 0){
                    newCar = Instantiate(car_prefab, new Vector3(car.x * -62.3f, 6, (car.z * 62.3f) - 30f), Quaternion.Euler(0,180,0)); 
                } else if(car.x == 14){
                    newCar = Instantiate(car_prefab, new Vector3(car.x * -62.3f, 6, (car.z * 62.3f) - 30f), Quaternion.Euler(0,0,0)); 
                } else if(car.z % 2 == 0){
                    newCar = Instantiate(car_prefab, new Vector3(car.x * -62.3f, 6, (car.z * 62.3f) - 30f), Quaternion.Euler(0,270,0)); 
                } else {
                    newCar = Instantiate(car_prefab, new Vector3(car.x * -62.3f, 6, (car.z * 62.3f) - 30f), Quaternion.Euler(0,90,0)); 
                }
                // newCar.transform.parent = transform;
                carDictionary.Add(car.index.ToString(), newCar.GetComponent<CarMovement>());
            }

        }

        for (int i = 0; i < varCarDictionary.Count; i++){
            carDictionary.Remove(varCarDictionary.First().Key);
            Destroy(varCarDictionary.First().Value.gameObject);
            varCarDictionary.Remove(varCarDictionary.First().Key);
        }
    }
    void handleLights(Light[] listaLuces)
    {
        foreach (Light light in listaLuces){
            lightDictionary[light.index.ToString()].SetColor(light.status);
        }
    }
}
