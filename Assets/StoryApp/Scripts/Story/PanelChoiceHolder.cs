using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelChoiceHolder : MonoBehaviour
{
    List<GameObject> panelChildren;
    private void Awake()
    {
        ChoiceInput.clicked = ClearPanels;    
    }

    private void ClearPanels()
    {
        panelChildren = new List<GameObject>();

        for (int i = 0; i < transform.childCount; i++)
        {
            //Debug.Log(transform.GetChild(i).name);
            Destroy(transform.GetChild(i).gameObject);
        }
    }

}
