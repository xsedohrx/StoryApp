using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The storylibrary is the database/library where all the stories are stored for users to download.
/// </summary>
public class StoryLibraryManager : MonoBehaviour
{
    public static Dictionary<int, Node> storyDictionary;

    public delegate void LoadStories();
    public static event LoadStories loadStory;

    private static StoryLibraryManager _instance;
    private bool _hasRefreshed;

    public static StoryLibraryManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("SpawnManager is NULL");
            return _instance;
        }
    }

    void Awake()
    {
        DontDestroySingleton();
        storyDictionary = new Dictionary<int, Node>();
        AddToStoryDictionary(0, new Node(0, "First"));
    }

    //Never destroy the singleton.
    private void DontDestroySingleton()
    {
        _instance = this;
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        BroadcastRefreshLibrary();
        StartStoryScene("New Scene 1");
    }

    //Get user Input
    private static void StartStoryScene(string sceneName)
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
