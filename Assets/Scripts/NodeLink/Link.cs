using System.Collections;
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

    //public GameObject LinkGameObject { get; set; }
    private GameObject nodeGeo { get; set; }
    public GameObject previousNode { get; set; }
    public GameObject nextNode { get; set; }

    public Transform parentObject { get; set; }
    public LineRenderer lineLink { get; set; }

    private void Awake()
    {
        this.lineLink = this.gameObject.GetComponent<LineRenderer>();
    }
    public LineRenderer GetLineRenderer()
    {
        return this.lineLink;
    }
    //public void Reset(GameObject nullObj)
    //{
    //    nullObj = null;
    //}

}
