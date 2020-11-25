using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour
{

    private static TouchInput _touchInstance;

    public static TouchInput TouchInstance
    {
       
        get {
            if (_touchInstance == null)
                Debug.LogError("TouchManager is NULL");
            return _touchInstance; 
        }
    }

    private void Awake()
    {
        _touchInstance = this;
    }

    // Update is called once per frame
    void Update()
    {
        MakeDecision();
    }

    //Detects which side of the screen has been pressed
    private void MakeDecision()
    {

        ////Fingercount testing
        //for (int i = 0; i < Input.touchCount; i++)
        //{
        //    Debug.Log("amount of fingers: " + (i + 1));
        //}

        if (Input.touchCount == 1)
        {

            if (Input.GetTouch(0).position.x > Screen.width / 2)
            {
                Debug.Log("Boy");
            }
            else
            {
                Debug.Log("Girl");
            }
        }

    }
}
