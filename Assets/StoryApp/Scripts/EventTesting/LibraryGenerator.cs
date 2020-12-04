using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// The storylibraryUI is used only to update the UI of the storylibrary.
/// </summary>
public class LibraryGenerator : MonoBehaviour
{
    //Declare the variables used for the StoryLibraryUI
    [SerializeField]
    GameObject _storyPrefab;
    List<Node> nodeList;


    // Start is called before the first frame update
    void Start()
    {
        nodeList = new List<Node>();
        StoryLibraryManager.loadStory = GenerateStory;
    }
     
    private void Update()
    {
        GenerateStory();

    }


    //Once the Library is loaded it will send a broadcast which this class will 
    private void GenerateStory()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Key being pressed");
            CreateStoryObjects();
        }
    }

    private void CreateStoryObjects()
    {
        float xOffset = 150.0f;
        
        foreach (KeyValuePair<int, Node> item in StoryLibraryManager.Instance.storyDictionary)
        {
            Debug.Log("ItemIndex " + item.Key);
            GameObject storyObject = Instantiate(_storyPrefab, new Vector3(item.Key * xOffset, item.Key, 0), Quaternion.identity);
            storyObject.transform.SetParent(this.gameObject.transform, false);
            storyObject.name = item.Key.ToString();
            StoryLibraryManager.Instance.storyGOList.Add(storyObject);
                       

           
        }
    }

}
