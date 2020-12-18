using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace StoryApp
{
    public class LibraryUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject _storyInfoPanel;
        [SerializeField]
        private TMP_Text _txtTitle, _txtDescription, _txtAge, _txtStoryLength;

        private bool IsStoryInfoPanelShowing;
        public bool IsShowing
        {
            get => IsStoryInfoPanelShowing;
            protected set => IsStoryInfoPanelShowing = value;
        }

        private void OnEnable()
        {
            LibraryGameObjectGenerator.Instance.UpdateUI += GetStoryDictionaryItem;

        }

        private void Update()
        {
            if (IsShowing)
            {
                togglePanel();
            }


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
            if (this._storyInfoPanel.activeSelf)
                this._storyInfoPanel.SetActive(false);
            else
                this._storyInfoPanel.SetActive(true);
        }


    }
}
