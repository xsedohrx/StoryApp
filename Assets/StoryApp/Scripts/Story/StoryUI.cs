using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoryUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _storyInfoPanel;

    private bool _isShowing;

    public bool IsShowing
    {
        get { return _isShowing; }
        set { _isShowing = value; }
    }


    private void Update()
    {
        if (IsShowing)
        {
            togglePanel();
        }
    }


    public void togglePanel()
    {
        if (this._storyInfoPanel.activeSelf)
        {
            this._storyInfoPanel.SetActive(false);
        }
        else
        {
            this._storyInfoPanel.SetActive(true);
        }
        UIManager.IsShowing = false;
        
    }


}
