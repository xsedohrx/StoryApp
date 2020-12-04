using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    

    #region Properties
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


    public Node(int id, string title, string description, int ageGroup, int storyLength) {
        //_nodeProperties = new NodeProperties();
        _id = id;
        _description = description;
        _title = title;
        _ageGroup = ageGroup;
        _storyLength = storyLength;
        

    }
}

