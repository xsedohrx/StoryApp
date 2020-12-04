using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class StoryLoaderUI : MonoBehaviour
{
    /**
     * Sine variables
     * 
     */
    float _amplitude = 1, _frequency = 0.1f;

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
            Debug.Log("Panel Position:" + storyPanel.transform.position);
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
            float x = 1f, 
                y = Mathf.Sin(0.5f * i * _frequency) * _amplitude, 
                z = storyPanels[i].transform.position.z;

            storyPanels[i].transform.position = new Vector3(x * i,y,z);
            i *= 5;
            //storyPanels[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = StoryLoader.storyList[i].Title;
            //storyPanels[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = StoryLoader.storyList[i].Description;
            //storyPanels[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = StoryLoader.storyList[i].StoryAgeGroup.ToString();
            //storyPanels[i].transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = StoryLoader.storyList[i].StoryElementAmount.ToString();             
        }
    }

}
