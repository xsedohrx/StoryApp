using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story
{    
    #region Properties
    /// <summary>
    /// All the properties of the story.
    /// </summary>
    private int _id, _ageGroup, _storyLength;
    private string _description, _title;
    public int ID
    {
        get { return _id; }
        set { _id = value; }
    }

    public string Description
    {
        get { return _description; }
        set { _description = value; }
    }

    public string Title
    {
        get { return _title; }
        set { _title = value; }
    }

    public int AgeGroup
    {
        get { return _ageGroup; }
        set { _ageGroup = value; }
    }

    public int StoryLength
    {
        get { return _storyLength; }
        set { _storyLength = value; }
    }

    #endregion
    #region StoryConstructor

    public Story(int id, string title, string description, int ageGroup, int storyLength)
    {        
        _id = id;
        _title = title;
        _description = description;
        _ageGroup = ageGroup;
        _storyLength = storyLength;        
    }

    #endregion

    //public void GenerateStoryElements(int index) {          
    //    StoryElement st = new StoryElement(index, StoryLibraryManager.Instance._storyDict[index].Title, StoryLibraryManager.Instance._storyDict[index].Description );
    //    StoryLibraryManager.Instance._storyElementDict.Add(index, st);

    //}
}

