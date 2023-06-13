using System;
using System.Collections.Generic;
using UnityEngine;
public class CharacterMovement : MonoBehaviour
{
    private static CharacterMovement instance;

    private int speed = 2;
    private int _rightTurnAngle = 11111;
    private int _leftTurnAngle = 11111;

    private Vector3 carRayDirection = new Vector3(0, 1, 0);
    private Vector3 carRightDirection = new Vector3(1, 0, 0);
    private Vector3 carLeftDirection = new Vector3(-1, 0, 0);
    
    private Vector3 _frontPos = new Vector3(0, 0.5f, 0);
    private Vector3 _rightPos = new Vector3(0.5f, 0, 0);
    private Vector3 _leftPos = new Vector3(-0.5f, 0.5f, 0);

    private Vector3 _turningPos;
    
    
    public List<Tuple<Vector3, int>> movementMemory = new List<Tuple<Vector3, int>>();
    
    public static CharacterMovement Instance
    {
        get { return instance; }
    }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        transform.Translate(new Vector3(1,0,0)*speed*Time.deltaTime);
        
        RaycastHit2D frontHit = Physics2D.Raycast(transform.position+_frontPos, carRayDirection, 0.1f);
        if (frontHit.collider != null) if(frontHit.transform.gameObject.name == "Terrain") speed = 0;

        RaycastHit2D rightHit = Physics2D.Raycast(transform.position+_rightPos, carRightDirection, 0.1f);
        if (rightHit.collider != null)
            if(rightHit.transform.gameObject.name == "Terrain")
                if (transform.eulerAngles.z == 0) _rightTurnAngle = 270;
                else print(transform.eulerAngles.z - 90);   
                     //_rightTurnAngle = (int)(transform.eulerAngles.z - 90);
        
        RaycastHit2D leftHit = Physics2D.Raycast(transform.position+_leftPos, carLeftDirection, 0.1f);
        if(leftHit.collider != null) 
            if(leftHit.transform.gameObject.name == "Terrain")  
                if (transform.eulerAngles.z == 270) _rightTurnAngle = 0;
                else _rightTurnAngle = (int)(transform.eulerAngles.z + 90);

        if (Input.GetKeyDown(KeyCode.D) && transform.rotation.eulerAngles.z != 180 && 
                transform.rotation.eulerAngles.z != _rightTurnAngle && transform.rotation.eulerAngles.z != _leftTurnAngle) RotateCar(0);
        if (Input.GetKeyDown(KeyCode.W) && transform.rotation.eulerAngles.z != 270 && 
            transform.rotation.eulerAngles.z != _rightTurnAngle && transform.rotation.eulerAngles.z != _leftTurnAngle) RotateCar(90);
        if (Input.GetKeyDown(KeyCode.A) && transform.rotation.eulerAngles.z != 0 && 
            transform.rotation.eulerAngles.z != _rightTurnAngle && transform.rotation.eulerAngles.z != _leftTurnAngle) RotateCar(180);
        if (Input.GetKeyDown(KeyCode.S) && transform.rotation.eulerAngles.z != 90 && 
            transform.rotation.eulerAngles.z != _rightTurnAngle && transform.rotation.eulerAngles.z != _leftTurnAngle) RotateCar(270);
    }

    private void RotateCar(int rotatevalue)
    {
        SetPositions(rotatevalue);
        speed = 2;
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, rotatevalue);
        movementMemory.Add(new Tuple<Vector3, int>(transform.position, rotatevalue));
    }

    private void SetPositions(int rotatevalue)
    {
        if (rotatevalue == 0)
        {
            carRayDirection = new Vector3(1, 0, 0);
            _frontPos = new Vector3(0.5f, 0, 0);

            carRightDirection = new Vector3(0, -1, 0);
            _rightPos = new Vector3(0, -0.5f, 0);
            
            carLeftDirection = new Vector3(0, 1, 0);
            _leftPos = new Vector3(0, 0.5f, 0);
        } 
        else if (rotatevalue == 90)
        {
            carRayDirection = new Vector3(0, 1, 0);
            _frontPos = new Vector3(0, 0.5f, 0);

            carRightDirection = new Vector3(1, 0, 0);
            _rightPos = new Vector3(0.5f, 0, 0);

            carRightDirection = new Vector3(-1, 0, 0);
            _rightPos = new Vector3(-0.5f, 0, 0);
        } 
        else if (rotatevalue == 180)
        {
            carRayDirection = new Vector3(-1,0,0);
            _frontPos = new Vector3(-0.5f, 0, 0);

            carRightDirection = new Vector3(0, 1, 0);
            _rightPos = new Vector3(0, 0.5f, 0);

            carRightDirection = new Vector3(0, -1, 0);
            _rightPos = new Vector3(0, -0.5f, 0);
        } 
        else if (rotatevalue == 270)
        {
            carRayDirection = new Vector3(0, -1, 0);
            _frontPos = new Vector3(0, -0.5f, 0);

            carRightDirection = new Vector3(-1, 0, 0);
            _rightPos = new Vector3(-0.5f, 0, 0);

            carRightDirection = new Vector3(1, 0, 0);
            _rightPos = new Vector3(0.5f, 0, 0);
        } 
    }
}
