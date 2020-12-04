using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{    
    public delegate void ActionClick();
    public static event ActionClick onClick;
    
    // Update is called once per frame
    void Update()
    {
        //RaycastDetection();
        
    }

    public void ButtonClick() {
        if (onClick != null)
            onClick();
    }


    ///// <summary>
    ///// Mouse input raycast detection
    ///// </summary>
    //private static void RaycastDetection()
    //{
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit hitData;

    //    if (Physics.Raycast(ray, out hitData, 1000))
    //    {
    //        Debug.Log("Object detected");
    //        //Event object detected
    //    }
    //}


    //private void MouseButtonClicked()
    //{
    //    if (true)
    //    {

    //        Debug.Log("Button Pressed");
    //    }
    //}

    //private void MouseRaycast()
    //{

    //    Vector3 mousePos = Input.mousePosition;
    //    mousePos.z = 5;

    //    RaycastHit2D hit = Physics2D.Raycast(mousePos, mousePos - Camera.main.ScreenToWorldPoint(mousePos), Mathf.Infinity);
    //    Debug.DrawRay(mousePos, mousePos - Camera.main.WorldToScreenPoint(mousePos), Color.blue);
    //    if (hit.collider != null)
    //    {
    //        Debug.Log("RayCast Hit");
    //        if (Input.GetMouseButtonDown(0))
    //        {
    //            MouseButtonClicked();
    //        }
    //    }


    //}

    void PauseStory() {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 0;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            Time.timeScale = 1;
        }
    }
}
