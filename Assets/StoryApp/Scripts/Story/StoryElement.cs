using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryElement
{
    #region Properties
    public int _id { get; }
    public int _idOfStory { get; }
    public string _title { get; }
    public string _description { get; }
    public bool _hasChoice { get; }
    #endregion
    #region StoryElementConstructor
    
    public StoryElement(int id, string title, string description, bool hasChoice, int idOfStory)
    {
        this._id = id;
        this._title = title;
        this._description = description;
        this._hasChoice = hasChoice;
        this._idOfStory = idOfStory;
    }
    #endregion

}
