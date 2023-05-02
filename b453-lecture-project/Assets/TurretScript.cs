using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    // Start is called before the first frame update



    [SerializeField] public string color;

    public GameObject[] blueBillions;
    public GameObject[] yellowBillions;
    public GameObject[] greenBillions;
    public GameObject[] orangeBillions;
    public GameObject[] spawners;

    private GameObject closestEnemy;

    

    public float closestDistance = 10000f;
    public int rank;

    [SerializeField] private float shootInterval = 5;
    [SerializeField] private float time = 1;
    [SerializeField] private float shootDistance = 5f;
    [SerializeField] private float maxRotate = 3f;



    [SerializeField] GameObject rocketPrefab;




    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rank = transform.root.gameObject.GetComponent<BillionSpawner>().getRank();
        pointAtEnemy();
    }

    void pointAtEnemy(){
        blueBillions = GameObject.FindGameObjectsWithTag("Blue");
        yellowBillions = GameObject.FindGameObjectsWithTag("Yellow");
        greenBillions = GameObject.FindGameObjectsWithTag("Green");
        orangeBillions = GameObject.FindGameObjectsWithTag("Orange");
        spawners = GameObject.FindGameObjectsWithTag("Spawner");
 
        closestDistance = 10000f;

        if(color != "blue" && blueBillions.Length > 0){
            closestEnemy = blueBillions[0];
            //Debug.Log("Not Blue");
            for (int i = 0; i < blueBillions.Length; i++){
                if(Vector3.Distance(blueBillions[i].transform.position, transform.position) < closestDistance){
                    closestEnemy = blueBillions[i];
                    closestDistance = Vector3.Distance(blueBillions[i].transform.position, transform.position);
                   //Debug.Log("Cloeset to blue");
                }
            }
        }
        if(color != "yellow" && yellowBillions.Length > 0){
            //Debug.Log("Not Yellow");
            for (int i = 0; i < yellowBillions.Length; i++){
                if(Vector3.Distance(yellowBillions[i].transform.position, transform.position) < closestDistance){
                    closestEnemy = yellowBillions[i];
                    closestDistance = Vector3.Distance(yellowBillions[i].transform.position, transform.position);
                    //Debug.Log("Cloeset to yellow");
                }
            }
        }
        if(color != "green" && greenBillions.Length > 0){
            for (int i = 0; i < greenBillions.Length; i++){
                if(Vector3.Distance(greenBillions[i].transform.position, transform.position) < closestDistance){
                    closestEnemy = greenBillions[i];
                    closestDistance = Vector3.Distance(greenBillions[i].transform.position, transform.position);
                }
            }
        }
        if(color != "orange" && orangeBillions.Length > 0){
            for (int i = 0; i < orangeBillions.Length; i++){
                if(Vector3.Distance(orangeBillions[i].transform.position, transform.position) < closestDistance){
                    closestEnemy = orangeBillions[i];
                    closestDistance = Vector3.Distance(orangeBillions[i].transform.position, transform.position);
                }
            }
        }

        for (int i = 0; i < spawners.Length; i++){
            if((Vector3.Distance(spawners[i].transform.position, transform.position) < closestDistance) && (spawners[i].GetComponent<BillionSpawner>().getColor() != color)){
                closestEnemy = spawners[i];
                closestDistance = Vector3.Distance(spawners[i].transform.position, transform.position);
            }
        }




        Vector3 targ = closestEnemy.transform.position;
        targ.z = 0f;
 
        Vector3 objectPos = transform.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;
 
        float targetAngle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        float currentAngle = transform.rotation.eulerAngles.z;

        targetAngle = normalizeAngle(targetAngle);
        currentAngle = normalizeAngle(currentAngle);

        float diffrence = normalizeAngle(targetAngle - currentAngle);
        


        if(diffrence < 180){
            targetAngle = currentAngle + maxRotate;
        }else{
            targetAngle = currentAngle - maxRotate;
        } 



        transform.rotation = Quaternion.Euler(new Vector3(0, 0, targetAngle));

        



        if(time >= shootInterval){

            if(closestDistance < shootDistance){
                spawnRocket(rocketPrefab);
                time = 0;
            }
        }
        time = time + 1;
    }


    private void spawnRocket(GameObject rocket){
        //Debug.Log("Trying to spawn rocket");
        Vector3 spawnPos = transform.position + (transform.right*0.2f);
       // Debug.Log("vector assigned");
        GameObject newRocket = Instantiate(rocket, spawnPos, transform.rotation);
        //Debug.Log("Rocket Spawned");
        newRocket.GetComponent<RocketScript>().setColor(color);
        newRocket.GetComponent<RocketScript>().setRank(rank);

        newRocket.transform.parent = gameObject.transform;
        //Debug.Log("Color assigned");

    }

    public static float normalizeAngle(float angle)
     {
        // Debug.Log("PreMod");
        // Debug.Log(angle);
         float result = angle - Mathf.CeilToInt(angle / 360f) * 360f;
         if (result < 0)
         {
             result += 360f;
         }
        // Debug.Log("PostMod");
        // Debug.Log(result);
         return result;
     }
}
