using System;
using System.Collections.Generic;
using UnityEngine;
public class CharacterMovement : MonoBehaviour
{
    private static CharacterMovement instance;

    private float speed = 4.5f;
    public int _rightTurnAngle = 11111;
    public int _leftTurnAngle = 11111;

    private Vector3 carRayDirection = new Vector3(0, 1, 0);
    
    private Vector3 _frontPos = new Vector3(0, 0.5f, 0);

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
        
        print(movementMemory);
    }

    private void RotateCar(int rotatevalue)
    {
        SetPositions(rotatevalue);
        speed = 4.5f;
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, rotatevalue);
        movementMemory.Add(new Tuple<Vector3, int>(transform.position, rotatevalue));
    }

    private void SetPositions(int rotatevalue)
    {
        if (rotatevalue == 0)
        {
            carRayDirection = new Vector3(1, 0, 0);
            _frontPos = new Vector3(0.5f, 0, 0);
        } 
        else if (rotatevalue == 90)
        {
            carRayDirection = new Vector3(0, 1, 0);
            _frontPos = new Vector3(0, 0.5f, 0);
        } 
        else if (rotatevalue == 180)
        {
            carRayDirection = new Vector3(-1,0,0);
            _frontPos = new Vector3(-0.5f, 0, 0);
        } 
        else if (rotatevalue == 270)
        {
            carRayDirection = new Vector3(0, -1, 0);
            _frontPos = new Vector3(0, -0.5f, 0);
        } 
    }
}
