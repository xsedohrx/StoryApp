﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link : MonoBehaviour
{ 

    public string linkName { get; set; }
    public int linkID { get; set; }
    public int linkIndex { get; set; }
    public Vector2 startPos { get; set; }
    public Vector2 endPos { get; set; }
    public Vector2 currentPos { get; set; }


    public GameObject linkOriginNode { get; set; }
    public GameObject linkDestinationNode { get; set; }

    public Transform parentObject { get; set; }
    public LineRenderer lineLink { get; set; }

    public LineRenderer GetLineRenderer()
    {
        return this.lineLink;
    }

    //public void Reset(GameObject nullObj)
    //{
    //    nullObj = null;
    //}

}
