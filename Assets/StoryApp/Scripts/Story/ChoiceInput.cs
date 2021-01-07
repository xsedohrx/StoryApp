using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceInput : MonoBehaviour
{
    public static Action clicked;
    bool buttonClicked;

    // Update is called once per frame
    void Update()
    {
        if (buttonClicked)
        {
            clicked?.Invoke();
        }
    }

    public void SelectChoice() {
        Debug.Log(gameObject.name );
        buttonClicked = true;
    }
}
