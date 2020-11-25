using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("SpawnManager is NULL");

            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }

    public void PlaceHolderFunction(string something)
    {
        Debug.Log("This is your string " + something);
    }

    public void StartAdventure() {
        Debug.Log("Are you a boy or are you a girl?");

    }

    private void Update()
    {
        
    }

}
