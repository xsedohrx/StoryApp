//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;

//public class UIManager : MonoBehaviour
//{
//    TextMeshProUGUI titleText, descriptionText;
//    string title, description;
//    int ageGroup, storyElementsAmount;
//    Toggle toggleButton;

//    bool isToggleOn;

//    private void Awake()
//    {
//        titleText = GameObject.Find("tmp_title").GetComponent<TextMeshProUGUI>();
//        descriptionText = GameObject.Find("tmp_description").GetComponent<TextMeshProUGUI>();
//        toggleButton = GameObject.Find("Toggle").GetComponent<Toggle>();

//    }



//    public void EnterData() {

//        title = titleText.text;
//        description = descriptionText.text;
//        if (toggleButton.isOn)
//        {
//            isToggleOn = true;
//        }
//        else
//        {
//            isToggleOn = false;
//        }


//        UpdateUI();
//    }

//    public void UpdateUI() {
//        StoryManager.CreateStory(0,title,description,0,0);
//    }

//    public void ShowDictionary() {
//        foreach (var item in StoryManager.storyDictionary)
//        {
//            Debug.Log("Title: " + item.Value.Title.ToString());
//        }
//    }
//}
