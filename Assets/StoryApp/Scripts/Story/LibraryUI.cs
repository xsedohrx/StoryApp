using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Library UI is used for showing the the library in the story player version of the app.
/// </summary>
namespace StoryApp
{

    public class LibraryUI : MonoBehaviour
    {

        [SerializeField]
        private GameObject _storyInfoPanel;
        [SerializeField]
        private TMP_Text _txtTitle, _txtDescription, _txtAge, _txtStoryLength;


        private void OnEnable()
        {
            LibraryGameObjectGenerator.Instance.UpdateUI += GetStoryDictionaryItem;
            
        }

        private void GetStoryDictionaryItem()
        {
            for (int i = 0; i < StoryLibraryManager.Instance.storyGOList.Count; i++)
            {
                if (gameObject.name == StoryLibraryManager.Instance.storyGOList[i].name)
                {
                    GetStoryInfo(StoryLibraryManager.Instance.storyDict[i]);
                }
            }
        }

        private void GetStoryInfo(Story story)
        {
            this._txtTitle.text = story.Title.ToString() + "";
            this._txtDescription.text = story.Description.ToString() + "";
            this._txtAge.text = story.AgeGroup.ToString() + "";
            this._txtStoryLength.text = story.StoryLength.ToString() + "";
            
        }

        public void togglePanel()
        {
            if (this._storyInfoPanel.activeSelf) { 
                this._storyInfoPanel.SetActive(false);
                
            }
            else
            {
                this._storyInfoPanel.SetActive(true);
                //transform.SetAsLastSibling();
            }
        }

    }
}