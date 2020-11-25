using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Story:MonoBehaviour
{
    public List<StoryElement> storyElements = new List<StoryElement>();
    private int _storyID, _storyElementAmount, _storyAgeGroup;
    private string _title, _description;

    public Story(int storyID, string title, string description, int storyElementAmount, int storyAgeGroup)
    {
        StoryID = storyID;
        Title = title;
        Description = description;
        StoryElementAmount = storyElementAmount;
        StoryAgeGroup = storyAgeGroup;
    }

    public int StoryID
    {
        get { return _storyID; }
        set { _storyID = value; }
    }
    public int StoryElementAmount
    {
        get { return _storyElementAmount; }
        set { _storyElementAmount = value; }
    }
    public int StoryAgeGroup
    {
        get { return _storyAgeGroup; }
        set { _storyAgeGroup = value; }
    }
    public string Title
    {
        get { return _title; }
        set { _title = value; }
    }
    public string Description
    {
        get { return _description; }
        set { _description = value; }
    }
}


public class StoryElement {

    //Story elements can be seen as nodes in a tree.
    private int keyID;
    public int choiceID, choiceAmount, choiceMade, idPreviousPage, idNextPage, choiceA, ChoiceB; 
    public string title, description;
    public bool hasChoice;

    public int KeyID
    {
        get { return keyID; }
        set { keyID = value; }
    }
    public enum storySection
    {
        INTRO,
        MID,
        END
    }
    public storySection StorySection;
    public Image image;

    public StoryElement(){
        //If an element has no choices
        if (!hasChoice)
        {
            choiceID = 0;
            choiceAmount = 0;
            choiceMade = 0;
        }
    }

    public class StoryGenre
    {
        //Genre title Core elements
        public int ageGroup;

        //possible options
        public string storyType;

    }


}

