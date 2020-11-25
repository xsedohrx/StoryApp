using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class StoryLoaderUI : MonoBehaviour
{
    public GameObject storyPanel;
    TextMeshProUGUI storyTitle, storyDescription, storyElements, storyAge;
    List<GameObject> storyPanels;
    // Start is called before the first frame update
    void Start()
    {
        storyPanels = new List<GameObject>();

        Debug.Log(StoryLoader._instance);
        foreach (var item in StoryLoader.storyList)
        {
            storyPanels.Add(Instantiate(storyPanel, transform));            
        }
        //StoryLoader.LoadStories();    

        UpdateUI();
    }

    //Called on the start and updates the text values of each story.
    private void UpdateUI()
    {
        Debug.Log("Coiunt of List: " + storyPanels.Count);

        for (int i = 0; i < storyPanels.Count; i++)
        {

            storyPanels[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = StoryLoader.storyList[i].Title;
            storyPanels[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = StoryLoader.storyList[i].Description;
            storyPanels[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = StoryLoader.storyList[i].StoryAgeGroup.ToString();
            storyPanels[i].transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = StoryLoader.storyList[i].StoryElementAmount.ToString();             
        }
    }

}
