using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public Vector3 _offSet;
    public float horizontalSpeed = 5f;

    private Vector3 mousePoisition;
    private Rigidbody2D rb;
    private Vector2 direction;
    private float moveSpeed = 100f;
    
    private void Start()
    {
        transform.position = _offSet;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        MoveForward();
        MoveHorizontallyRB();
    }

    void MoveForward()
    {
        transform.Translate(Vector3.up * ( speed * Time.deltaTime));
    }
    
    void MoveHorizontallyRB()
    {
        
        Vector3 psn = transform.position;
        mousePoisition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePoisition - transform.position;
        Vector2 newPos = new Vector2(direction.x,0);
        rb.MovePosition(newPos*Time.deltaTime*horizontalSpeed); //
        if (transform.position.x < -1)
        {
            transform.position = new Vector3(-1,transform.position.y,1);
        }
        else if (transform.position.x > 1)
        {
            transform.position = new Vector3(1,transform.position.y,1);
        }
    }

    void MoveHorizontally() // Old version, redundant
    {
        Vector3 pos = transform.position;
        //pos.x = Input.mousePosition.x + transform.position.x;
        if (Input.mousePosition.x < transform.position.x)
        {
            horizontalSpeed *= -1;
            transform.Translate(Vector3.left * Time.deltaTime);
        }
        else if (Input.mousePosition.x == transform.position.x)
        {
            horizontalSpeed = 0;
        }
        else
        {
            transform.Translate(Vector3.right * Time.deltaTime);
        }
        transform.Translate(Vector3.right * (horizontalSpeed * Time.deltaTime));
        //this.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,this.transform.position.y,_offSet.z);
    }

    

    
}
