using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class billionScript : MonoBehaviour
{

    public string typeDescription = "billion";

    private float currentSpeed = 0;    // 0-1 from no speed to full speed
    [SerializeField] private float speed = 1f;
    private float accelMagnitude = 1f;
    private float decelMagnitude = 1f;
    [SerializeField] private Vector3 velocity;

    GameManager manager;
    private GameObject flag1;
    private GameObject flag2;
    private GameObject target;
    private GameObject closestEnemy;

    private bool target1Initialized = false;
    private bool target2Initialized = false;
    public  float  slowDistance = 0.25f;
    public  float  stopDistance = 0.25f;
    Vector3 direction; 
    [SerializeField] float acceleration = 1.1f;
    float accelerationMax = 15f;
    public string color;

    public SpriteRenderer spriteRenderer;
    public GameObject upgradedCircle;
    public Sprite[] spriteList;
    int stage = 0;

    public GameObject[] blueBillions;
    public GameObject[] yellowBillions;
    public GameObject[] greenBillions;
    public GameObject[] orangeBillions;
    public GameObject[] spawners;
    public GameObject[] upgrades;

    

    public float closestDistance = 10000f;

    [SerializeField] private float shootInterval = 5;
    [SerializeField] private float time = 1;
    [SerializeField] private float shootDistance = 3f;


    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject rocketPrefab;
    [SerializeField] GameObject spinnerPrefab;


    public int rank;
    public int health;
    public int maxHealth;

    public bool isUpgraded = false;

 
    void Start (){
        upgradedCircle.SetActive(false);
        if(isUpgraded){
            upgradedCircle.SetActive(true);
        }
    }

    public void setRank(int pRank){
        rank = pRank; 
        health = rank * 5;
        maxHealth = health;

        

        for(int i = 0; i < rank; i++){

            GameObject newSpinner = Instantiate(spinnerPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, (360/rank) * i)));
            newSpinner.transform.parent = transform;
        }


    }

    public void setColor(string pcolor){
        color = pcolor;
    }

    public int takeDamage(int damage) 
    {
        health = health - damage;
        if(health <= 0){
            return rank;
            //Object.Destroy(this.gameObject);
        }

        for (int i = 0; i < 5; i++){
            if(health <= (maxHealth / 5 * i)){
                spriteRenderer.sprite = spriteList[i]; 
                break;
            }
        }
        return 0;

    }


    void pointAtEnemy(){
        blueBillions = GameObject.FindGameObjectsWithTag("Blue");
        yellowBillions = GameObject.FindGameObjectsWithTag("Yellow");
        greenBillions = GameObject.FindGameObjectsWithTag("Green");
        orangeBillions = GameObject.FindGameObjectsWithTag("Orange");
        spawners = GameObject.FindGameObjectsWithTag("Spawner");
        //upgrades = GameObject.FindGameObjectsWithTag("Upgrade");
 
        closestDistance = 10000f;

        if(color != "blue"){
            //closestEnemy = blueBillions[0];
            //Debug.Log("Not Blue");
            for (int i = 0; i < blueBillions.Length; i++){
                if(Vector3.Distance(blueBillions[i].transform.position, transform.position) < closestDistance){
                    closestEnemy = blueBillions[i];
                    closestDistance = Vector3.Distance(blueBillions[i].transform.position, transform.position);
                   //Debug.Log("Cloeset to blue");
                }
            }
        }
        if(color != "yellow"){
            //Debug.Log("Not Yellow");
            for (int i = 0; i < yellowBillions.Length; i++){
                if(Vector3.Distance(yellowBillions[i].transform.position, transform.position) < closestDistance){
                    closestEnemy = yellowBillions[i];
                    closestDistance = Vector3.Distance(yellowBillions[i].transform.position, transform.position);
                    //Debug.Log("Cloeset to yellow");
                }
            }
        }
        if(color != "green"){
            for (int i = 0; i < greenBillions.Length; i++){
                if(Vector3.Distance(greenBillions[i].transform.position, transform.position) < closestDistance){
                    closestEnemy = greenBillions[i];
                    closestDistance = Vector3.Distance(greenBillions[i].transform.position, transform.position);
                }
            }
        }
        if(color != "orange"){
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
 
        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if(time >= shootInterval){

            if(closestDistance < shootDistance){
                if(isUpgraded){
                    spawnRocket(rocketPrefab);
                }else{
                    spawnBullet(bulletPrefab);
                }
                
                time = 0;
            }
        }
        time = time + 1;

    }

    private void spawnBullet(GameObject bullet){
        Vector3 spawnPos = transform.position + (transform.right*0.05f);
        GameObject newBullet = Instantiate(bullet, spawnPos, transform.rotation);
        newBullet.transform.parent = gameObject.transform;
        newBullet.GetComponent<BulletScript>().setColor(color);
        newBullet.GetComponent<BulletScript>().setRank(rank);

    }

    private void spawnRocket(GameObject rocket){
        Vector3 spawnPos = transform.position + (transform.right*0.2f);
        GameObject newRocket = Instantiate(rocket, spawnPos, transform.rotation);
        newRocket.GetComponent<RocketScript>().setColor(color);
        newRocket.GetComponent<RocketScript>().setRank(rank);
        newRocket.GetComponent<RocketScript>().isUpgraded = true;

        newRocket.transform.parent = gameObject.transform;
        //Debug.Log("Color assigned");

    }


    
 
    private void setTarget()
    {
        manager = FindObjectOfType<GameManager>();
        
        flag1 = manager.getFlag1(color);
        
        flag2 = manager.getFlag2(color);
        

        if (flag1 != null)
        {
            target1Initialized = true;
        }
        if (flag2 != null)
        {
            target2Initialized = true;
        }
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        setTarget();
        pointAtEnemy();

        if (target1Initialized && !target2Initialized){

            target = manager.getFlag1(color);

            float distance1 = Vector3.Distance(flag1.transform.position, transform.position);

            var step =  speed * acceleration * Time.deltaTime / 5; // calculate distance to move
            if(distance1 > slowDistance){
                if(acceleration < accelerationMax){
                    acceleration = acceleration + 0.1f;
                }
            }else{
                if(acceleration > 1){
                    acceleration = acceleration - 0.1f;
                }
                if(distance1 < stopDistance){
                    acceleration = 1f;
                }
            }
            transform.position = Vector3.MoveTowards(transform.position, flag1.transform.position, step);

        }else if((target1Initialized && target2Initialized)){
            flag1 = manager.getFlag1(color);
            flag2 = manager.getFlag2(color);

            float distance1 = Vector3.Distance(flag1.transform.position, transform.position);
            float distance2 = Vector3.Distance(flag2.transform.position, transform.position);

            if(distance1 < distance2){
                target = flag1;
            }else{
                target = flag2;
            }

            var step =  speed * acceleration * Time.deltaTime / 5; // calculate distance to move
            if(Vector3.Distance(target.transform.position, transform.position) > slowDistance){
                if(acceleration < accelerationMax){
                    acceleration = acceleration + 0.1f;
                }
            }else{
                if(acceleration > 1){
                    acceleration = acceleration - 0.1f;
                }
                if(distance1 < stopDistance){
                    acceleration = 1f;
                }
            }
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);


        }
        /*
        if(distance1 > slowDistance){
                increaseVelocity(direction);
            }else{
                velocity = velocity - ((slowDistance / distance1) * direction) * Time.deltaTime;

                transform.position = transform.position + (velocity * Time.deltaTime);
                transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
                
                
                //decreaseVelocity(direction);
                if(distance1 < stopDistance){
                    velocity = velocity - velocity;
                }
            }
        
        
        
         }else if (target1Initialized && target2Initialized)
        {
            blueFlag1 = manager.getBlueFlag1();
            blueFlag2 = manager.getBlueFlag2();
            float distance1 = Vector3.Distance(blueFlag1.transform.position, transform.position);
            float distance2 = Vector3.Distance(blueFlag2.transform.position, transform.position);

            if(distance1 < distance2){
                direction = (blueFlag1.transform.position - transform.position).normalized;
                if(distance1 < stopDistance){
                    velocity = velocity - velocity;
                }else if(distance1 < slowDistance){
                    decreaseVelocity(direction, distance1);
                }else{
                    increaseVelocity(direction);
                }
            }else{
                direction = (blueFlag2.transform.position - transform.position).normalized;
                if(distance1 < stopDistance){
                    velocity = velocity - velocity;
                }else if(distance2 < slowDistance){
                    decreaseVelocity(direction, distance2);
                }else{
                    increaseVelocity(direction);
                }
            }
        } else{
            setTarget();
        }
        */

    }


}
