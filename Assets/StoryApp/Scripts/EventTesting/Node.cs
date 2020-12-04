using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{

    NodeProperties _nodeProperties;
    int _id;
    string _description;
    public Node(int id, string title) {
        _nodeProperties = new NodeProperties();
        _nodeProperties.id = id;
        _nodeProperties.description = _description;
        
        StoryLibraryManager.loadStory += getStory;

    }

    private void getStory()
    {
        //Debug.Log("StoryID: " + nodeProperties.id + "_StoryDescription: " + nodeProperties.description);
        NodeElements storyElement = new NodeElements(0, "", "");
        
        
    }
}

public class NodeProperties {
    public int id;
    public string description;

}
