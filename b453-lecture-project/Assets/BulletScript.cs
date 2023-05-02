using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public string typeDescription = "bullet";
    public string color;
    public float speed = 10.0f;
    Rigidbody2D rigidbody;
    public int time = 0;


    public SpriteRenderer spriteRenderer;
    [SerializeField] public Sprite blueSprite;
    [SerializeField] public Sprite yellowSprite;
    [SerializeField] public Sprite greenSprite;
    [SerializeField] public Sprite orangeSprite;

    [SerializeField] public GameObject billion;
    [SerializeField] public GameObject blueSpawner;
    [SerializeField] public GameObject yellowSpawner;
    [SerializeField] public GameObject greenSpawner;
    [SerializeField] public GameObject orangeSpawner;

    public int baseDamage = 1;
    public int rank = 1;
    public int enemyRank;

    public bool colorSet = false;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!colorSet){
            setColor(color);
            colorSet = true;
        }


        //transform.position = transform.position + (transform. * speed);
        //rigidbody.velocity = transform.up * 10;
        transform.position += transform.right * Time.deltaTime * speed;

        time = time + 1;
        if (time > 1000){
            Destroy(gameObject);
        }

        //transform.Translate(transform.up * speed * Time.deltaTime); 

    }

    public void setColor(string pcolor){
        color = pcolor;

        if(color == "blue"){
            spriteRenderer.sprite = blueSprite; 
        }else if(color == "yellow"){
            spriteRenderer.sprite = yellowSprite; 
        }else if(color == "green"){
            spriteRenderer.sprite = greenSprite; 
        }else if(color == "orange"){
            spriteRenderer.sprite = orangeSprite; 
        }
    }

    public void setRank(int pRank){
        rank = pRank; 
    }

    void OnCollisionEnter2D(Collision2D col){
        //Debug.Log("collision");
        //Destroy(gameObject);

        


        if (col.gameObject.tag == "Wall"){
            Destroy(gameObject);
        }
        
        if (col.gameObject.tag == "Blue"){
            //Debug.Log("blue collision");
            if(color != "blue"){
                enemyRank = col.gameObject.GetComponent<billionScript>().takeDamage(rank*baseDamage);
                if(enemyRank > 0){
                    Object.Destroy(col.gameObject);
                    transform.root.gameObject.GetComponent<BillionSpawner>().gainExp(5 * (int)System.Math.Pow(2, (rank - 1)));
                }
                Destroy(gameObject);
            }
        }
        if (col.gameObject.tag == "Yellow"){
            if(color != "yellow"){
                enemyRank = col.gameObject.GetComponent<billionScript>().takeDamage(rank*baseDamage);
                if(enemyRank > 0){
                    Object.Destroy(col.gameObject);
                    transform.root.gameObject.GetComponent<BillionSpawner>().gainExp(5 * (int)System.Math.Pow(2, (rank - 1)));
                }
                Destroy(gameObject);
            }
        }
        if (col.gameObject.tag == "Green"){
            if(color != "green"){
                enemyRank = col.gameObject.GetComponent<billionScript>().takeDamage(rank*baseDamage);
                if(enemyRank > 0){
                    Object.Destroy(col.gameObject);
                    transform.root.gameObject.GetComponent<BillionSpawner>().gainExp(5 * (int)System.Math.Pow(2, (rank - 1)));
                }
                Destroy(gameObject);
            }
        }
        if (col.gameObject.tag == "Orange"){
            if(color != "orange"){
                enemyRank = col.gameObject.GetComponent<billionScript>().takeDamage(rank*baseDamage);
                if(enemyRank > 0){
                    Object.Destroy(col.gameObject);
                    transform.root.gameObject.GetComponent<BillionSpawner>().gainExp(5 * (int)System.Math.Pow(2, (rank - 1)));
                }
                Destroy(gameObject);
            }
        }

        if (col.gameObject.tag == "Spawner"){
            if(color != col.gameObject.GetComponent<BillionSpawner>().getColor()){
                col.gameObject.GetComponent<BillionSpawner>().takeDamage();
                Destroy(gameObject);
            }
        }

    }


}
