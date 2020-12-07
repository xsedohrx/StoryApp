using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Carousel : MonoBehaviour
{
    private void Start()
    {
        Debug.Log(PlayerPrefs.GetString("StoryTitle"));
        Debug.Log(PlayerPrefs.GetInt("StoryLength"));
    }

    //List<GameObject> _storyList;
    //[SerializeField]
    //GameObject _storyGOPrefab;
    //private void Awake()
    //{
    //    _storyList = new List<GameObject>();
    //}

    //private void Start()
    //{
    //    GenerateStoryList();
    //}

    //private void GenerateStoryList()
    //{
    //    for (int i = 0; i < 10; i++)
    //    {
    //        GameObject _storyGO = Instantiate(_storyGOPrefab);
    //        _storyList.Add(_storyGO);
    //    }
    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}


}
