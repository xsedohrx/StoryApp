using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

namespace StoryApp
{
        public class StoryPlayerManager : MonoBehaviour
    {
    
        #region Singleton
        private static StoryPlayerManager _instance;
        public static StoryPlayerManager Instance
        {
            get
            {
                if (_instance == null)
                    Debug.LogError("SpawnManager is NULL");
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

        public Story storyToPlay;
        public void SetStory(int storyIndex) {
            Debug.Log(StoryLibraryManager.Instance.storyDict[storyIndex].ID.ToString());

        //StartStory("StoryScene");
    }

        public void StartStory(string sceneName) {
            SceneManager.LoadScene(sceneName);
        }
   
    }
}