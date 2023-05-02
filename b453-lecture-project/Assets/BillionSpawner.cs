using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillionSpawner : MonoBehaviour
{

     public string typeDescription = "spawner";
    [SerializeField] GameObject billionPrefab;

    [SerializeField] private float spawnInterval = 100;
    [SerializeField] public string color = "blue";
    private float time = 0;


    public SpriteRenderer spriteRenderer;
    public Sprite[] spriteList;

    public SpriteRenderer numberRenderer;
    public Sprite[] numberList;

    public SpriteRenderer expRenderer;
    public Sprite[] expList;

    public int exp = 0;
    public int expToLevel = 25;

    public int spawnUpgradedBillionCounter = 0;


    int stage = 0;


    public int rank = 5;
    public int health;
    public int maxHealth;

    public void setRank(int pRank){
        rank = pRank; 
        health = rank * 16;
        maxHealth = health;
        numberRenderer.sprite = numberList[rank]; 
    }

    public string getColor(){
        return color;
    }

    public int getRank(){
        return rank;
    }


    public void setExpBar(){
        for (int i = 0; i < expList.Length; i++){
            if(exp <= (expToLevel * System.Math.Pow(2, (rank - 1)) / expList.Length * i)){
                expRenderer.sprite = expList[i]; 
                break;
            }
        }
    }





    public void takeDamage() 
    {
        if(stage < 15){
            spriteRenderer.sprite = spriteList[stage+1]; 
            //Debug.Log(stage);
            stage++;
        }else{
            Object.Destroy(this.gameObject);
        }
    }

    public void gainExp(int pExp){
        exp = exp + pExp;
        setExpBar();
    }


    public void updateRank(){
        if(exp > expToLevel * System.Math.Pow(2, (rank - 1))){
            exp = (int)(exp - expToLevel * System.Math.Pow(2, (rank - 1)));
            rank++;
        }
        numberRenderer.sprite = numberList[rank];
    }

   // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if(time % spawnInterval == 0){

            if(spawnUpgradedBillionCounter > 0){
                spawnUpgradedBillion(billionPrefab);
                spawnUpgradedBillionCounter--;
            }else{
                spawnBillion(billionPrefab);
            }
        }
        time++;

        updateRank();
    }

    private void spawnBillion(GameObject billion){
        //GameObject newEnemy = Instantiate(billion, new Vector3(Random.Range(-5f, 5), Random.Range(-6f, 6f), 0), Quaternion.identity);
        GameObject newBillion = Instantiate(billion, new Vector3(transform.position.x- Random.Range(-0.005f, 0.005f) , transform.position.y-  Random.Range(-0.005f, 0.005f)), Quaternion.identity);
        newBillion.transform.parent = gameObject.transform;
        newBillion.GetComponent<billionScript>().setColor(color);
        newBillion.GetComponent<billionScript>().setRank(rank);

    }

    private void spawnUpgradedBillion(GameObject billion){
        //GameObject newEnemy = Instantiate(billion, new Vector3(Random.Range(-5f, 5), Random.Range(-6f, 6f), 0), Quaternion.identity);
        GameObject newBillion = Instantiate(billion, new Vector3(transform.position.x- Random.Range(-0.005f, 0.005f) , transform.position.y-  Random.Range(-0.005f, 0.005f)), Quaternion.identity);
        newBillion.transform.parent = gameObject.transform;
        newBillion.GetComponent<billionScript>().setColor(color);
        newBillion.GetComponent<billionScript>().setRank(rank);
        newBillion.GetComponent<billionScript>().isUpgraded = true;

    }

    public void collectUpgrade(){
        Debug.Log("SPAWN UPGRADE");

        spawnUpgradedBillionCounter = 10;

    }




    
}
