using System;
using System.Collections;
using System.Collections.Generic;
using Eoe.Managers;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.EventSystems;
using UnityEngine.XR;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public Vector3 _offSet;
    public float horizontalSpeed = 5f;

    private Vector3 mousePoisition;
    private Rigidbody2D rb;
    private Vector2 direction;
    private float moveSpeed = 100f;

    private Vector3 _mousePositionDifference=new Vector3(0,0,0);
    private Vector3 _mouseLastPosition=new Vector3(0,0,0);
    private Vector3 _mouseCurrentPosition=new Vector3(0,0,0);
    private Vector3 _zeroVector = new Vector3(0,0,0);

    public float maxRange=0.5f;

    private float originPosition;
    private float _mouseInput;
    private Vector3 balance;
    private BalanceManager balanceManager;
    private float playerOrigin;

    [SerializeField] private float balanceVariable;
    private void Start()
    {
        balance = new Vector3(0,0,0);
        playerOrigin = balance.x;
        
        transform.position = _offSet;
        rb = GetComponent<Rigidbody2D>();
        originPosition = Screen.width / 2f;
        balanceManager = FindObjectOfType<BalanceManager>();
    }

    private void FixedUpdate()
    {
        //MoveForward();
        //MoveHorizontallyRB();
        Deneme();
        //GetAxisHorizontal(); // :)
    }

    private void MoveForward()
    {
        transform.Translate(new Vector3(this.transform.position.x,1,1) * ( speed * Time.deltaTime));
        //transform.position = new Vector3(this.transform.position.x,1,1)*Time.deltaTime*speed;
    }
    
    private void MoveHorizontallyRB() // 
    {
        //originPosition = playerOrigin;
        //_mouseCurrentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float delta = (Input.mousePosition.x - originPosition)/100;
        
        var coord = new Vector3(delta,transform.position.y,transform.position.z);  

        /*if (balance.x < -maxRange)
        {
            transform.position = new Vector3(-maxRange,transform.position.y,1); // sonra sil
        }
        else if (balance.x > maxRange)
        {
            transform.position = new Vector3(maxRange,transform.position.y,1); // sonra sil
        }*/ 
        //Debug.Log("Delta: " + delta);
        balance.x += delta;
        //Debug.Log("Balance X: " + balance.x);
        balance.x = Mathf.Clamp(balance.x, -maxRange, maxRange);
        playerOrigin = balance.x;
        balanceManager.ChangeBalance(delta);
        
        /*
        _mousePositionDifference = _mouseCurrentPosition - _mouseLastPosition; // current - last
        
        if (_mousePositionDifference.x > 0) // positive so, go right
        {
            //transform.Translate(1,0,1);
            transform.position = new Vector3(_mousePositionDifference.x *horizontalSpeed,0,1)*Time.deltaTime;
        }
        else if(_mousePositionDifference.x < 0)
        {
            //transform.Translate(-1,0,1);
            transform.position = new Vector3(_mousePositionDifference.x *horizontalSpeed,0,1)*Time.deltaTime;
        }
        Debug.Log("Difference: " + _mousePositionDifference + "\nCurrent: " + _mouseCurrentPosition + "\nLast Pos: "+ _mouseLastPosition);
        
        /*if (Math.Abs(_mouseCurrentPosition.x - _mouseLastPosition.x) < 0.05f)
        {
            _mouseLastPosition = _mouseCurrentPosition;
        }
        _mouseCurrentPosition = new Vector3(0,0,0);
        */
        /*  
        direction = mousePoisition - transform.position;
        Vector3 newPos = new Vector3(direction.x,0,1);
        //rb.MovePosition((transform.position + newPos) * Time.deltaTime); 
        transform.position = newPos;
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange,transform.position.y,1);
        }
        else if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange,transform.position.y,1);
        }
*/
        
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

    private void MoveHorizontallyRB_OLD()
    {

        Vector3 psn = transform.position;
        mousePoisition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePoisition - transform.position;
        rb.velocity = new Vector2(direction.x*horizontalSpeed,0);

    }
    
    private void GetAxisHorizontal()
    {
        _mouseInput = Input.GetAxis("Mouse X");
        Debug.Log(_mouseInput);
        transform.position = new Vector3(_mouseInput*2,this.transform.position.y,this.transform.position.z);
    }

    private void Deneme()
    {
        Vector3 position = transform.position;
        //_mouseCurrentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float x = (Input.mousePosition.x - originPosition)/100;
        position = new Vector3(x,this.transform.position.y,this.transform.position.z);
        
        if (transform.position.x < -maxRange)
        {
            transform.position = new Vector3(-maxRange,transform.position.y,1);
        }
        else if (transform.position.x > maxRange)
        {
            transform.position = new Vector3(maxRange,transform.position.y,1);
        }
        
        balanceManager.ChangeBalance(balanceVariable);
    }
    
}
