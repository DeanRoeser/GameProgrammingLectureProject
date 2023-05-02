using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flagScript : MonoBehaviour
{

    private bool dragging = false;
    private Vector3 offset;
    public string color;

    // Start is called before the first frame update
    void Start()
    {
        //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 10f);
    }

    public void setColor(string pcolor){
        color = pcolor;
    }

    // Update is called once per frame
    void Update()
    {
        if (dragging) {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            Debug.Log("dragging");
        }
    }

    public void Move(Vector3 pos){
        transform.position = pos;
    }

    private void OnMouseDown() 
    {
        Debug.Log("MouseDown");
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragging = true;
    }

    private void OnMouseUp() 
    {
        Debug.Log("MouseUp");
        dragging = false;
    }

}
