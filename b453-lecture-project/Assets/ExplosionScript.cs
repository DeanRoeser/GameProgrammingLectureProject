using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{

    public int time = 0;
    public int baseDamage = 10;
    public int rank = 1;
    public int enemyRank;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(0.01f, 0.01f, 1f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time = time + 1;

        if (time > 50){
            Destroy(gameObject);
        }
        transform.localScale = new Vector3(time * 0.02f, time * 0.02f, 1f);
    }


    public void setRank(int pRank){
        rank = pRank; 
    }


    void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.tag == "Blue" || col.gameObject.tag == "Yellow" || col.gameObject.tag == "Green" || col.gameObject.tag == "Orange"){
            //Debug.Log("blue collision");
            enemyRank = col.gameObject.GetComponent<billionScript>().takeDamage(rank*baseDamage);
            if(enemyRank > 0){
                Object.Destroy(col.gameObject);
            //    transform.root.gameObject.GetComponent<BillionSpawner>().gainExp(5 * (int)System.Math.Pow(2, (rank - 1)));
            }
        }
       
    }
}
