using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The storylibraryUI is used only to update the UI of the storylibrary.
/// </summary>
public class StoryLibraryUI : MonoBehaviour
{

    //Declare the variables used for the StoryLibraryUI
    [SerializeField]
    GameObject _storyPrefab;





    // Start is called before the first frame update
    void Start()
    {
        StoryLibraryManager.loadStory += UpdateUI;
        
    }
    private void Update()
    {
        UpdateUI();
    }


    //Once the Library is loaded it will send a broadcast which this class will 
    private void UpdateUI()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Key being pressed");
            CreateStoryObjects();
        }
    }

    private void CreateStoryObjects()
    {
        foreach (KeyValuePair<int, Node> item in StoryLibraryManager.storyDictionary)
        {
            int key = 0;
            Node value;
            bool keyExists = StoryLibraryManager.storyDictionary.TryGetValue(key, out value);
            if (keyExists)
            { }
            else
            {
                float xOffset = 150.0f;
                Debug.Log(item.Key);
                GameObject storyObject = Instantiate(_storyPrefab, new Vector3(item.Key * xOffset, 0, 0), Quaternion.identity);
                storyObject.transform.SetParent(this.gameObject.transform, false);
            }
        }
    }

}
