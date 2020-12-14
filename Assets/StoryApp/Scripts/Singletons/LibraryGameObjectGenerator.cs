using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// The storylibraryUI is used only to update the UI of the storylibrary.
/// </summary>
public class LibraryGameObjectGenerator : MonoBehaviour
{
    #region SingletonSetup
    private static LibraryGameObjectGenerator _instance;
    public static LibraryGameObjectGenerator Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("SpawnManager is NULL");
            return _instance;
        }
    }
    //Never destroy the singleton.
    private void DontDestroySingleton()
    {
        _instance = this;
        DontDestroyOnLoad(this);
    }

    #endregion
    #region Variables

    //Declare the variables used for the StoryLibraryUI
    [SerializeField]
    GameObject _storyPrefab;
    public Action UpdateUI;

    #endregion
    #region UnityFunctionality
    private void Awake()
    {
        DontDestroySingleton();
        StoryLibraryManager.Instance.GenerateStories += GenerateStory;
        
    }

    private void OnDisable()
    {
        StoryLibraryManager.Instance.GenerateStories -= GenerateStory;        
    }

    #endregion

    #region StoryGeneration
    //Once the Library is loaded it will send a broadcast which this class will 
    private void GenerateStory()
    {
        if (StoryLibraryManager.Instance.hasUpdated)
        {
            float xOffset = 150.0f;
            foreach (KeyValuePair<int, Story> item in StoryLibraryManager.Instance.storyDict)
            {
                //Debug.Log("ItemIndex " + item.Key);
                GameObject storyObject = Instantiate(_storyPrefab, new Vector3(item.Key * xOffset, item.Key, 0), Quaternion.identity);
                storyObject.transform.SetParent(this.gameObject.transform, false);
                storyObject.name = item.Key.ToString();
                StoryLibraryManager.Instance.storyGOList.Add(storyObject);
            }
        }

        if (UpdateUI != null)
            UpdateUI();

    }

    #endregion
}
