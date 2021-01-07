using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

namespace StoryApp
{
    public class StoryManager : MonoBehaviour
    {

        #region Singleton
        private static StoryManager _instance;
        public static StoryManager Instance
        {
            get
            {
                if (_instance == null)
                    Debug.LogError("LoadStoryInToolkit is NULL");
                return _instance;
            }
        }


        //Never destroy the singleton.
        private void DontDestroySingleton()
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }

        #endregion
        Story storyToPlay;
        public void GetStoryToPlay(int storyIndex)
        {
            //storyIndex = gameObject.GetComponentInChildren<LibraryUI>()>
            storyToPlay = StoryLibraryManager.Instance.storyDict[storyIndex];

            PlayerPrefs.SetInt("Id", StoryLibraryManager.Instance.storyDict[storyIndex].ID);
            PlayerPrefs.SetString("Title", StoryLibraryManager.Instance.storyDict[storyIndex].Title.ToString());
            PlayerPrefs.SetString("Description", StoryLibraryManager.Instance.storyDict[storyIndex].Description.ToString());
            PlayerPrefs.SetInt("AgeGroup", StoryLibraryManager.Instance.storyDict[storyIndex].AgeGroup);
            PlayerPrefs.SetInt("StoryLength", StoryLibraryManager.Instance.storyDict[storyIndex].StoryLength);
            Debug.Log(PlayerPrefs.GetInt("Id") + PlayerPrefs.GetString("Title") + PlayerPrefs.GetString("Description") + PlayerPrefs.GetInt("AgeGroup") + PlayerPrefs.GetInt("StoryLength"));
            StartStory("StoryScene");
        }


        public void StartStory(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

    }
}