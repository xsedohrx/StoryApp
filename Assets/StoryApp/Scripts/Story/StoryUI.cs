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
    [SerializeField]
    private TMP_Text _txtTitle, _txtDescription, _txtAge, _txtStoryLength;
    
    private bool _isShowing;
    public bool IsShowing
    {
        get => _isShowing; 
        protected set => _isShowing = value;
    }


    private void Update()
    {
        if (IsShowing)
        {
            togglePanel();
        }

        //SetStoryInfo(StoryLibraryManager.Instance.storyDictionary[i]);

    }

    private void SetStoryInfo(Node story)
    {        
        this._txtTitle.text = story.Title.ToString() + "";
        this._txtDescription.text = story.Description.ToString() + "";
        this._txtAge.text = story.AgeGroup.ToString() + "";
        this._txtStoryLength.text = story.StoryLength.ToString() + "";     
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
