using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Data class containing the story element data.
/// </summary>
public class StoryElement
{
    // story, id, title, image, description, position, parent, children, haschoice, choiceID
    #region Properties
    public int _id { get; }
    public int _idOfStory { get; }
    public string _title { get; }
    public string _description { get; }
    public bool _hasChoice { get; }
    public int _choiceID { get; }
    public Vector3 _position { get; }
    public Node _parent { get; }
    public List<Node> _children { get; }
    public Image _image { get; }

#endregion
#region StoryElementConstructor

public StoryElement(int id, int idOfStory, string title, string description, bool hasChoice, 
    int choiceID, Vector3 position, Node parent, List<Node> children, Image image)
    {
        this._id = id;
        this._idOfStory = idOfStory;
        this._title = title;
        this._description = description;
        this._hasChoice = hasChoice;
        this._choiceID = choiceID;
        this._position = position;
        this._parent = parent;
        this._children = children;
        this._image = image;
    }
    #endregion

}
