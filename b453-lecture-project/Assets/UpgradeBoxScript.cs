using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeBoxScript : MonoBehaviour
{

    public int blueCounter = 0;
    public int yellowCounter = 0;
    public int greenCounter = 0;
    public int orangeCounter = 0;

    public GameObject blueBillion;
    public GameObject yellowBillion;
    public GameObject greenBillion;
    public GameObject orangeBillion;

    public int billionsNeeded = 5;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(blueCounter >= billionsNeeded){
            blueBillion.transform.root.gameObject.GetComponent<BillionSpawner>().collectUpgrade();
            Destroy(gameObject);
        }
        if(yellowCounter >= billionsNeeded){
            yellowBillion.transform.root.gameObject.GetComponent<BillionSpawner>().collectUpgrade();
            Destroy(gameObject);
        }
        if(greenCounter >= billionsNeeded){
            greenBillion.transform.root.gameObject.GetComponent<BillionSpawner>().collectUpgrade();
            Destroy(gameObject);
        }
        if(orangeCounter >= billionsNeeded){
            orangeBillion.transform.root.gameObject.GetComponent<BillionSpawner>().collectUpgrade();
            Destroy(gameObject);
        }
        //blueCounter = 0;
        //yellowCounter = 0;
        //greenCounter = 0;
        //orangeCounter = 0;
    }


    void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.tag == "Wall"){
            Destroy(gameObject);
        }
        if (col.gameObject.tag == "Spawner"){
            Destroy(gameObject);
        }


        if (col.gameObject.tag == "Blue"){
            blueCounter++;
            blueBillion = col.gameObject;
        }
        if (col.gameObject.tag == "Yellow"){
            yellowCounter++;
            yellowBillion = col.gameObject;
        }
        if (col.gameObject.tag == "Green"){
            greenCounter++;
            greenBillion = col.gameObject;
        }
        if (col.gameObject.tag == "Orange"){
            orangeCounter++;
            orangeBillion = col.gameObject;
        }
    }
}
