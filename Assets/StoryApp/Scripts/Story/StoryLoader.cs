using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryLoader : MonoBehaviour
{
    [SerializeField]
    private GameObject choiceHolder, prefabStoryChoice;
    bool generateTrigger;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            generateTrigger = true;
        }

        if (generateTrigger)
        {
            GenerateChoicePanels();
        }
    }

    //Clear panels for adding new choices
    private void GenerateChoicePanels() {

        for (int i = 0; i < 2; i++)
        {
            GameObject tempStoryChoice = Instantiate(prefabStoryChoice, choiceHolder.transform);

        }
        generateTrigger = false;
    }
}
