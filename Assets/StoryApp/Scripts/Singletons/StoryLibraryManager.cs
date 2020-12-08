using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

/// <summary>
/// The storylibrary is the database/library where all the stories are stored for users to Load/download.
/// </summary>
public class StoryLibraryManager : MonoBehaviour
{
    #region SingletonSetup
    /// <summary>
    /// Create an instance and never destroy it
    /// </summary>
    private static StoryLibraryManager _instance;
    public static StoryLibraryManager Instance
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

    public bool hasUpdated;
    public List<GameObject> storyGOList;
    public List<StoryElement> storyElements;
    public Dictionary<int, Story> storyDict;
    public Action GenerateStories;
    
    #endregion
    #region UnityFunctions
    /// <summary>
    /// Never destroy the instance and set the first entries in the Dictionary.
    /// </summary>
    void Awake()
    {
        DontDestroySingleton();
        storyGOList = new List<GameObject>();
        storyElements = new List<StoryElement>();
        storyDict = new Dictionary<int, Story>();

        //Add to Dictionary
        AddStoryToDictionary(0, new Story(0, "First", "hhjdksaglkfh", 6, 10));
        AddStoryToDictionary(1, new Story(1, "second", "jyukuytui  hhjjgbjndkfdsbn  asaglkfh", 12, 2));
        AddStoryToDictionary(2, new Story(2, "third", "dfsa", 9, 3));
        AddStoryElementsToList();
    }

    private void Update()
    {
        BroadcastRefreshLibrary();
    }
    #endregion
    #region DictionaryFunctionality

    /// <summary>
    /// Send a broadcast once the hasrefreshed trigger is true and reset the trigger to false.
    /// </summary>
    private void BroadcastRefreshLibrary()
    {
        if (hasUpdated)
        {
            if (GenerateStories != null)
                GenerateStories();
            hasUpdated = false;
        }
    }

    /// <summary>
    /// Adds a entry to the dictionary and toggles the hasupdated trigger.
    /// </summary>
    /// <param name="index"></param>
    /// <param name="storyToAdd"></param>
    private void AddStoryToDictionary(int index, Story storyToAdd)
    {
        storyDict.Add(index, storyToAdd);        
        hasUpdated = true;
    }

    /// <summary>
    /// For the story length of every story an element is created and added to the storyelement list.
    /// </summary>
    private void AddStoryElementsToList()
    {
        for (int i = 0; i < storyDict.Count; i++)
        {
            for (int j = 0; j < storyDict[i].StoryLength; j++)
            {
                StoryElement se = new StoryElement(j, " ", " ", true, i);
                storyElements.Add(se);
                Debug.Log("Element: " + storyDict[i].Title);
            }
        }
    }

    #endregion

}
