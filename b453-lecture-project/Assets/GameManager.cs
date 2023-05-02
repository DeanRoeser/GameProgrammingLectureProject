using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Level
  {
    Low,
    Medium,
    High
  }




public class GameManager : MonoBehaviour
{

    public GameObject blueFlag;
    public GameObject yellowFlag;
    public GameObject greenFlag;
    public GameObject orangeFlag;
    [SerializeField] float offsetX;
    [SerializeField] float offsetY;
    private int blueFlagsPlaced = 0;
    private int yellowFlagsPlaced = 0;
    private int greenFlagsPlaced = 0;
    private int orangeFlagsPlaced = 0;
    public GameObject blueFlag1 = null;
    public GameObject blueFlag2 = null;
    public GameObject yellowFlag1 = null;
    public GameObject yellowFlag2 = null;
    public GameObject greenFlag1 = null;
    public GameObject greenFlag2 = null;
    public GameObject orangeFlag1 = null;
    public GameObject orangeFlag2 = null;
    private string currentColor = "blue";



    GameObject flag1;
    GameObject flag2;
    GameObject flag;
    int flagsPlaced;

    [SerializeField] public GameObject upgradePrefab;
    public int upgradeResetCounter = 0;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)){
            currentColor = "blue";
        }else if(Input.GetKeyDown(KeyCode.Alpha2)){
            currentColor = "yellow";
        }else if(Input.GetKeyDown(KeyCode.Alpha3)){
            currentColor = "green";
        }else if(Input.GetKeyDown(KeyCode.Alpha4)){
            currentColor = "orange";
        }else if(Input.GetKeyDown(KeyCode.Alpha0)){
            currentColor = "null";
        }

        if (Input.GetMouseButtonDown(0))
          {
              PlaceFlag(currentColor);
          }

        if(Random.Range(0,15000) < upgradeResetCounter){
            GameObject newUpgradeBox = Instantiate(upgradePrefab, new Vector3(Random.Range(-16f, -5f) , Random.Range(0f, 8f)), Quaternion.identity);
            upgradeResetCounter = 0;
        }
        upgradeResetCounter++;


        

        
    }

    public GameObject getFlag1(string color){
        if(color == "blue" ){
            return blueFlag1;
        }else if(color == "yellow" ){
            return yellowFlag1;
        }else if(color == "green" ){
            return greenFlag1;
        }else{
            return orangeFlag1;
        }
    }

    public GameObject getFlag2(string color){
        if(color == "blue" ){
            return blueFlag2;
        }else if(color == "yellow" ){
            return yellowFlag2;
        }else if(color == "green" ){
            return greenFlag2;
        }else{
            return orangeFlag2;
        }
    }

    public int getFlagsPlaced(string color){
        if(color == "blue" ){
            return blueFlagsPlaced;
        }else if(color == "yellow" ){
            return yellowFlagsPlaced;
        }else if(color == "green" ){
            return greenFlagsPlaced;
        }else if(color == "orange" ){
            return orangeFlagsPlaced;
        }else{
            return -1;
        }
    }


    void PlaceFlag(string color){
        if(color == "blue" ){
            flag1 = blueFlag1;
            flag2 = blueFlag2;
            flag = blueFlag;
            flagsPlaced = blueFlagsPlaced;
        }else if(color == "yellow" ){
            flag1 = yellowFlag1;
            flag2 = yellowFlag2;
            flag = yellowFlag;
            flagsPlaced = yellowFlagsPlaced;
        }else if(color == "green" ){
            flag1 = greenFlag1;
            flag2 = greenFlag2;
            flag = greenFlag;
            flagsPlaced = greenFlagsPlaced;
        }else if(color == "orange" ){
            flag1 = orangeFlag1;
            flag2 = orangeFlag2;
            flag = orangeFlag;
            flagsPlaced = orangeFlagsPlaced;
        }else{
            return;
        }

        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 offset = new Vector3(offsetX,offsetY,10);
        if(flagsPlaced == 0){
            flag1 = (GameObject)Instantiate(flag, pos + offset, Quaternion.identity);
            flag1.GetComponent<flagScript>().setColor(color);
            flagsPlaced = flagsPlaced + 1;
        }else if(flagsPlaced == 1){
            flag2 = (GameObject)Instantiate(flag, pos + offset, Quaternion.identity);
            flag2.GetComponent<flagScript>().setColor(color);
            flagsPlaced = flagsPlaced + 1;
        }else if(flagsPlaced >= 2){
            float distance1 = Vector3.Distance(flag1.transform.position, pos+offset);
            float distance2 = Vector3.Distance(flag2.transform.position, pos+offset);
            //Debug.Log("Distance1: " + distance1);
            //Debug.Log("Distance2: " + distance2);
            if(distance1 < distance2){
                flag1.transform.position = pos + offset;
            }else{
                flag2.transform.position = pos + offset;
            }
        }

        if(color == "blue" ){
            blueFlag1 = flag1;
            blueFlag2 = flag2;
            blueFlag = flag;
            blueFlagsPlaced = flagsPlaced;
        }else if(color == "yellow" ){
            yellowFlag1 = flag1;
            yellowFlag2 = flag2;
            yellowFlag = flag;
            yellowFlagsPlaced = flagsPlaced;
        }else if(color == "green" ){
            greenFlag1 = flag1;
            greenFlag2 = flag2;
            greenFlag = flag;
            greenFlagsPlaced = flagsPlaced;
        }else if(color == "orange" ){
            orangeFlag1 = flag1;
            orangeFlag2 = flag2;
            orangeFlag = flag;
            orangeFlagsPlaced = flagsPlaced;
        }

    }


}
