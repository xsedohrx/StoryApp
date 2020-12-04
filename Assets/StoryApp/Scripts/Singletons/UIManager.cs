using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region SetInstance

    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("Input Manager is NULL!");
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    #endregion
    public delegate void ClickEvent();
    public static event ClickEvent eventShowOnClick;
    
    private static bool isShowing;
    public static bool IsShowing
    {
        get { return isShowing; }
        set { isShowing = value; }
    }

    private void Update()
    {
        HoverOnStory();
    }

    private void HoverOnStory()
    {
        if (isShowing)
        {
            Debug.Log("Is Hovering: " + IsShowing);
            if (eventShowOnClick != null)
                eventShowOnClick();

            
        }
    }
}
