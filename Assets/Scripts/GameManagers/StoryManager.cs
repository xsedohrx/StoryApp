using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    public static Dictionary<int, Story> storyDictionary;
    public static Dictionary<int, StoryElement> storyElementDictionary;
    private static StoryManager _instance;
    public static StoryManager Instance
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
        _instance = this;
        storyDictionary = new Dictionary<int, Story>();
        storyElementDictionary = new Dictionary<int, StoryElement>();
    }

    //Create the story and all of the properties
    public static void CreateStory(int id, string title, string description, int elementAmount, int age) {
        Story story;
        story = new Story(id, title, description, elementAmount, age);
        story.StoryID = id;
        story.Title = title;
        story.Description = description;
        story.StoryElementAmount = elementAmount;
        story.StoryAgeGroup = age;
        for (int i = 0; i < elementAmount; i++)
        {
            StoryElement storyElement = new StoryElement();
            story.storyElements.Add(storyElement);
        }
        AddStoryToDictionary(story);

    }


    ////Create the story element and pass the properties
    //public static void CreateStoryElement(StoryElement.storySection storySection, string title, string description, bool hasChoice)
    //{
    //    StoryElement storyElement;
    //    storyElement = new StoryElement();
    //    storyElement.title = title;
    //    storyElement.StorySection = storySection;
    //    storyElement.description = description;
    //    storyElement.hasChoice = hasChoice;
    //    Debug.Log("Story section: " + storySection + " title: "+ title + " description: " + description + " hasChoice: " + hasChoice );        
    //    AddStoryElementToDictionary(storyElement);
    //}



    static int index = 0;
    public static void AddStoryToDictionary(Story story)
    {
        if (storyDictionary.Count == 0 && !storyDictionary.ContainsKey(index))
        {
            storyDictionary.Add(index, story);
            story.StoryID = index;
            ReturnIndexValue(index);
        }
        else
        {
            //If the dictionary index position has no key
            if (!storyDictionary.ContainsKey(index))
            {
                //ADD KEY 
                storyDictionary.Add(index, story);
                story.StoryID = index;
                ReturnIndexValue(index);
                //Debug.Log("Added key!" + index);
            }
            else
            {
                //DO NOT ADD KEY
                index++;
                //Debug.Log("Recursion number: " + index);
                AddStoryToDictionary(story);

            }
        }
        //Debug.Log("Story elements: " + storyDictionary.Count);

        ResetIndex();
    }

    //private static void AddStoryElementToDictionary(StoryElement storyElement)
    //{
    //    //If there are no keys stored and the index key is empty 
    //    if (storyElementDictionary.Count == 0 && !storyElementDictionary.ContainsKey(index))
    //    {
    //        storyElementDictionary.Add(index, storyElement);
    //        storyElement.KeyID = index;
    //        ReturnIndexValue(index);
    //        index++;
    //        //Debug.Log("Count is 0 so Key has been added!" + index);
    //    }
    //    else
    //    {
    //        //If the dictionary index position has no key
    //        if (!storyElementDictionary.ContainsKey(index))
    //        {
    //            //ADD KEY 
    //            storyElementDictionary.Add(index, storyElement);
    //            storyElement.KeyID = index;
    //            ReturnIndexValue(index);
    //            //Debug.Log("Added key!" + index);
    //        }
    //        else
    //        {
    //            //DO NOT ADD KEY
    //            index++;
    //            //Debug.Log("Recursion number: " + index);
    //            AddStoryElementToDictionary(storyElement);
                
    //        }
    //    }
    //    //Debug.Log("Story elements: " + storyDictionary.Count);

    //    ResetIndex();
    //}

    private static int ReturnIndexValue(int index)
    {

        Debug.Log("Index value: " + index);
        return index;
    }

    private static void ResetIndex()
    {
        index = 0;
    }
}
