using System;
using System.Collections.Generic;
using UnityEngine;
public class CharacterMovement : MonoBehaviour
{
    private static CharacterMovement instance;

    private int speed = 2;
    public int _rightTurnAngle = 11111;
    public int _leftTurnAngle = 11111;

    private Vector3 carRayDirection = new Vector3(0, 1, 0);
    private Vector3 carRightDirection = new Vector3(1, 0, 0);
    private Vector3 carLeftDirection = new Vector3(-1, 0, 0);
    
    private Vector3 _frontPos = new Vector3(0, 0.5f, 0);
    private Vector3 _rightPos = new Vector3(0.45f, 0, 0);
    private Vector3 _leftPos = new Vector3(-0.45f, 0, 0);

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

        #region useless
        /*RaycastHit2D rightHit = Physics2D.Raycast(transform.position+_rightPos, carRightDirection, 0.1f);
        if (rightHit.collider != null)
        {
            if (rightHit.transform.gameObject.name == "Terrain")
            {
                if (transform.eulerAngles.z == 0) _rightTurnAngle = 270;
                else _rightTurnAngle = Mathf.RoundToInt(transform.eulerAngles.z - 90);
            }
            else _rightTurnAngle = 11111;
        }
        else _rightTurnAngle = 11111;


        RaycastHit2D leftHit = Physics2D.Raycast(transform.position+_leftPos, carLeftDirection, 0.1f);
        if (leftHit.collider != null)
        {
            if (leftHit.transform.gameObject.name == "Terrain")
            {
                if (transform.eulerAngles.z == 270) _leftTurnAngle = 0;
                else _leftTurnAngle = Mathf.RoundToInt(transform.eulerAngles.z + 90);            
            }
            else _leftTurnAngle = 11111;
        }
        else _rightTurnAngle = 11111;

        print(" left: " + _leftTurnAngle);
        print(" right: " + _rightTurnAngle);*/
        #endregion
        print(_rightTurnAngle);
        
        if (Input.GetKeyDown(KeyCode.D) && transform.eulerAngles.z != 180) 
            if(Math.Abs(0 - _rightTurnAngle) >= 5 && Mathf.Abs(0 - _leftTurnAngle) >= 5) 
                RotateCar(0);
        if (Input.GetKeyDown(KeyCode.W) && transform.eulerAngles.z != 270) 
            if(Mathf.Abs(90 - _rightTurnAngle) >= 5 && Mathf.Abs(90 - _leftTurnAngle) >= 5) 
                RotateCar(90);
        if (Input.GetKeyDown(KeyCode.A) && transform.rotation.eulerAngles.z != 0) 
            if(Mathf.Abs(180 - _rightTurnAngle) >= 5 && Mathf.Abs(180 - _leftTurnAngle) >= 5) 
                RotateCar(180);
        if (Input.GetKeyDown(KeyCode.S) && transform.rotation.eulerAngles.z != 90) 
            if(Mathf.Abs(270 - _rightTurnAngle) >= 5 && Mathf.Abs(270 - _leftTurnAngle) >= 5) 
                RotateCar(270);
    }

    private void RotateCar(int rotatevalue)
    {
        SetPositions(rotatevalue);
        speed = 2;
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, rotatevalue);
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
