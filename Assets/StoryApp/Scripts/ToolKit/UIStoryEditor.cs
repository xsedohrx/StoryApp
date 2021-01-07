using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace StoryApp
{
    public class UIStoryEditor : MonoBehaviour
    {
        /// <summary>
        /// Create an instance and never destroy it
        /// </summary>
        private static UIStoryEditor _instance;
        public static UIStoryEditor Instance
        {
            get
            {
                if (_instance == null)
                    Debug.LogError("UIStoryEditor is NULL");
                return _instance;
            }
        }

        //Never destroy the singleton.
        private void DontDestroySingleton()
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }

        List<GameObject> elementObjectList;
        [SerializeField]
        GameObject elementGameObject, pnl_elementSelected, pnl_elementInfo;
        private float xOffset;
        public Text txt_storyTitle;
        public int nodeNameIndex { get; set; }                          // DAVE

        private void Awake()
        {
            DontDestroySingleton();
            nodeNameIndex = 0;                                          // DAVE
            elementObjectList = new List<GameObject>();
            pnl_elementSelected.SetActive(true);
            pnl_elementInfo.SetActive(false);
        }

        public void SaveStory()
        {
            string storyTitle = txt_storyTitle.text.ToString();
            PlayerPrefs.SetString("StoryTitle", storyTitle);
            PlayerPrefs.SetInt("StoryLength", elementObjectList.Count);
            Debug.Log("Title Set: " + PlayerPrefs.GetString("StoryTitle"));
        }

        public void ReturnToLibrary()
        {
            SceneManager.LoadScene("ToolkitLibrary");
        }


        #region ElementSpawning
        public void GenerateElement()
        {
            xOffset = 1.5f;
            SpawnElement(new Vector3(-8 + elementObjectList.Count * xOffset, 0, 0));
        }

        private void SpawnElement(Vector3 positionToSpawn)
        {
            Vector3 position = positionToSpawn;
            GameObject elementGameObjectToAdd = Instantiate(elementGameObject, positionToSpawn, Quaternion.identity);
            elementGameObjectToAdd.name = "Node" + nodeNameIndex;
            elementGameObjectToAdd.GetComponent<Node>().access = Node.Access.open;
            elementObjectList.Add(elementGameObjectToAdd);
            nodeNameIndex++;
        }

        #endregion

        public void ShowInfoPanel()
        {
            pnl_elementInfo.SetActive(true);
        }

        public void HideInfoPanel()
        {
            pnl_elementInfo.SetActive(false);
        }
    }
}