using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeElements
{
    string _elementName, _elementDescription;
    float _elementID;


    public NodeElements(int id, string name, string description) {
        
        _elementID = id;
        _elementName = name;
        _elementDescription = description;
        StoryLibraryManager.loadStory += GetElements;
        GetElements();
    }

    private void GetElements()
    {
        Debug.Log("_elementID: " + _elementID + "_elementName: " + _elementName + "_ElementDescription: " + _elementDescription); 
    }

    private void OnDisable()
    {
        StoryLibraryManager.loadStory -= GetElements;
    }
}
