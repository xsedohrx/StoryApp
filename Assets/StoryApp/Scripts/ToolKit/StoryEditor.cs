using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryEditor : MonoBehaviour
{
    List<GameObject> elementObjectList;
    [SerializeField]
    GameObject elementGameObject, pnl_elementSelected;
    private float xOffset;

    private void Awake()
    {
        elementObjectList = new List<GameObject>();
        pnl_elementSelected.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

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
        elementObjectList.Add(elementGameObjectToAdd);
    }

    #endregion
}
