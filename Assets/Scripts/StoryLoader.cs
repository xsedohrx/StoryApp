using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryLoader : MonoBehaviour
{
    public static StoryLoader _instance;  

    public static List<Story> storyList;
    private void Awake()
    {
        _instance = this;
        storyList = new List<Story>();
        //Story test = new Story("Davey gets rekt!", "hgfdjkslghkulfdhsgkjlhfkjghsklhdgfjhgfdhdj", 3);
        //storyList.Add(test);


        LoadStories();
       
        
    }

    public static void LoadStories()
    {
        foreach (var item in StoryManager.storyDictionary)
        {
            storyList.Add(new Story(item.Value.StoryID, item.Value.Title, item.Value.Description, item.Value.StoryElementAmount, item.Value.StoryAgeGroup));
        }
    }

}
