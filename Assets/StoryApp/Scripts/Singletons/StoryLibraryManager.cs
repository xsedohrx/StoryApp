using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

/// <summary>
/// The storylibrary is the database/library where all the stories are stored for users to download.
/// </summary>
public class StoryLibraryManager : MonoBehaviour
{

    private bool _hasRefreshed;
    public Dictionary<int, Node> storyDictionary;
    public List<GameObject> storyGOList;
    public static Action loadStory;


    void Awake()
    {
        DontDestroySingleton();
        storyDictionary = new Dictionary<int, Node>();
        storyGOList = new List<GameObject>();

        AddToStoryDictionary(0, new Node(0, "First", "hhjdksaglkfh", 6, 0));
        AddToStoryDictionary(1, new Node(1, "second", "jyukuytui  hhjjgbjndkfdsbn  asaglkfh", 12, 0));
        AddToStoryDictionary(2, new Node(2, "third", "dfsa", 9, 0));
    }
    #region Singleton

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
    private void Update()
    {
        BroadcastRefreshLibrary();
        StartStoryScene("New Scene 1");
    }

    private void Start()
    {
        
    }

    //Get user Input to load new Scene
    private void StartStoryScene(string sceneName)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(sceneName);
        }
    }


    /// <summary>
    /// Send a broadcast once the hasrefreshed boolean is true and reset the trigger to false.
    /// </summary>
    private void BroadcastRefreshLibrary()
    {
        if (_hasRefreshed)
        {
            if (loadStory != null)
                loadStory();
            _hasRefreshed = false;
        }
    }

    //Whenever the dictionary is updated set refresh trigger to true.
    private void AddToStoryDictionary(int index, Node nodeToAdd)
    {
        storyDictionary.Add(index, nodeToAdd);
        _hasRefreshed = true;
    }


}
