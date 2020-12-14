using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryEditor : MonoBehaviour
{
    List<GameObject> elementObjectList;
    [SerializeField]
    GameObject elementGameObject, pnl_elementSelected;
    private float xOffset;
    public Text txt_storyTitle;

    public int nodeNameIndex { get; set; }                          // DAVE

    private void Awake()
    {
        nodeNameIndex = 0;                                          // DAVE
        elementObjectList = new List<GameObject>();
        pnl_elementSelected.SetActive(true);
    }


    public void SaveStory() {
        string storyTitle = txt_storyTitle.text.ToString();
        PlayerPrefs.SetString("StoryTitle", storyTitle);
        PlayerPrefs.SetInt("StoryLength", elementObjectList.Count);
        Debug.Log("Title Set: " + PlayerPrefs.GetString("StoryTitle")) ;
        SaveStoryToDictionary();

    }

    private void SaveStoryToDictionary()
    {
        StoryLibraryManager.Instance.storyDict.Add(0, new Story(0,"","",0, 0));
    }

    public void ReturnToLibrary() {
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
}
