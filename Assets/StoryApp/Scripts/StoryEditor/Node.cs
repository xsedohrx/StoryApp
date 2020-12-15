using System;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    // External References
    public DataContainer dataContainer;

    //Prefabs
    public GameObject linkPrefab;

    // Enum
    public enum Access
    {
        open,
        closed,
    }

    //Fields
    public Color nodeHighlightColor;
    public Color nodeNoHighlightColor;
    // ----------------------------------

    // Properties
    public Access access { get; set; }
    public string nodeName { get; set; }
    public int nodeID { get; set; }
    // ----------------------------------

    //INPUT NODES
    public GameObject inNode { get; set; }

    [SerializeField] public int inNodeCount;
    public SortedDictionary<string, GameObject> inNodeDictionary { get; set; }

    // OUTPUTS NODES
    public GameObject outNodeA { get; set; }
    public GameObject outNodeB { get; set; }
    // ----------------------------------

    // INPUTS LINKS
    [SerializeField] public int inLinkCount;
    public GameObject inLink { get; set; }
    public SortedDictionary<string, GameObject> inLinkDictionary { get; set; }
    // ----------------------------------

    // OUTPUTS LINKS
    [SerializeField] public int outLinkCount;
    public int maxOutLinks { get; set; }
    public GameObject outLinkA { get; set; }
    public GameObject outLinkB { get; set; }
    public SortedDictionary<string, GameObject> outLinkDictionary { get; set; }
    // ----------------------------------

    public Action updateUI;
    public bool uiUpdated;

    public static bool queriesHitTriggers = true;   // ensures this object remains a HitTrigger for collider


    private void Awake()
    {
        nodeName = "node";
        inNodeCount = 0;
        inNodeDictionary = new SortedDictionary<string, GameObject>();

        inLinkCount = 0;
        inLinkDictionary = new SortedDictionary<string, GameObject>();

        maxOutLinks = 2;
        outLinkCount = 0;
        outLinkDictionary = new SortedDictionary<string, GameObject>();

        this.gameObject.GetComponent<MeshRenderer>().material.color = this.nodeNoHighlightColor;
        this.access = Access.open;
        updateUI?.Invoke();
    }

    private void Update()
    {
        updateUI?.Invoke();
    }

    public bool isCapacity()
    {
        return this.outLinkCount == this.maxOutLinks ? true : false;
    }



    public string ReturnName()
    {
        return this.nodeName;
    }


    // Node Helper Functions
    public bool duplicateNodeConnectionCheck(string nodeName)
    {
        if (inNodeDictionary.ContainsKey(nodeName))
        {
            Debug.Log("These Nodes were connected - DELETING");
            return true;
        }
        else
        {
            Debug.Log("These Nodes are NOT connected");
            return false;
        }
    }
    public void AddInNodeDict(string nodeName, GameObject gameObject)
    {
        if (!inNodeDictionary.ContainsKey(nodeName))
        {
            inNodeDictionary.Add(nodeName, gameObject);
        }
        else
        {
            Debug.Log("These Nodes are already connected");
        }
    }
    public void RemoveInNodeDict(string linkName)
    {
        if (inNodeDictionary.TryGetValue(linkName, out GameObject linkObject))
        {
            inNodeDictionary.Remove(linkName);
            Destroy(linkObject);
            inNodeCount = inNodeDictionary.Count;
        }
    }

    // Link Helper Functions
    public void AddOutLinkDict(string nodeName, GameObject gameObject)
    {
        if (!outLinkDictionary.ContainsKey(nodeName))
        {
            outLinkDictionary.Add(nodeName, gameObject);
            outLinkCount = this.outLinkDictionary.Count;
        }
    }
    public void RemoveOutLinkDict(string linkName)
    {
        if (outLinkDictionary.TryGetValue(linkName, out GameObject linkObject))
        {
            outLinkDictionary.Remove(linkName);
            Destroy(linkObject);
            outLinkCount = outLinkDictionary.Count;
        }
    }
    public void AddInLinkDict(string nodeName, GameObject gameObject)
    {
        if (!inLinkDictionary.ContainsKey(nodeName))
        {
            inLinkDictionary.Add(nodeName, gameObject);
            inLinkCount = inLinkDictionary.Count;
        }
    }
    public void RemoveInLinksDict(string linkName)
    {
        if (inLinkDictionary.TryGetValue(linkName, out GameObject linkObject))
        {
            inLinkDictionary.Remove(linkName);
            Destroy(linkObject);
            inLinkCount = inLinkDictionary.Count;
        }
    }

    public void DragOutNodes(Vector3 mousePos)
    {
        foreach (KeyValuePair<string, GameObject> valuePair in outLinkDictionary)
        {
            valuePair.Value.GetComponent<LineRenderer>().SetPosition(0, mousePos);
        }
    }
    public void DragInNodes(Vector3 mousePos)
    {
        foreach (KeyValuePair<string, GameObject> valuePair in inLinkDictionary)
        {
            for (int i = 0; i < valuePair.Value.transform.childCount; i++)
            {
                Debug.Log(valuePair.Key);
                if (valuePair.Value.transform.GetChild(i).gameObject.name == valuePair.Key.ToString())
                {
                    valuePair.Value.transform.GetChild(i).GetComponent<LineRenderer>().SetPosition(1, mousePos);
                }
            }
        }
    }

    public GameObject LinkSpawner(GameObject nodeGameObj, out LineRenderer lineLink, out Link currentLink, out string linkName, out GameObject currentGO)
    {
        currentGO = Instantiate(linkPrefab, nodeGameObj.transform.position, Quaternion.identity, nodeGameObj.transform);
        {
            //Assignments
            currentLink = currentGO.GetComponent<Link>();
            Node startNode = nodeGameObj.GetComponent<Node>();
            lineLink = currentLink.GetComponent<LineRenderer>();

            // Link Assignments
            currentLink.name = startNode.outLinkCount == 0 ? startNode.name + "Link" + "A" : startNode.name + "Link" + " B";
            linkName = currentLink.name;
            currentLink.linkIndex = 0;
            currentLink.parentObject = nodeGameObj.transform;
            currentLink.startPos = nodeGameObj.transform.position;

            currentLink.linkOriginNode = nodeGameObj;

            //Node Assignments for the node that has just been clicked on to initiate the LineRenderer;


            if (startNode.outLinkCount == 0)
            {
                startNode.outLinkA = currentGO;
                lineLink.material = currentLink.Red;
            }
            else
            {
                startNode.outLinkB = currentGO;
                lineLink.material = currentLink.Blue;
            }

            // LineRenderer Start Position Assignments
            //startNode.state = Node.State.selectable;
            lineLink.enabled = true;
            lineLink.positionCount = 2;
            lineLink.startWidth = 0.2f;
            lineLink.SetPosition(0, currentLink.startPos);

            // Add Link GameObejct to List
        }
        //dataContainer.AddGameObjList(currentGO);
        AddOutLinkDict(linkName, currentGO);
        return currentGO;
    }

    //public static implicit operator Action<object>(Node v)
    //{
    //    throw new NotImplementedException();
    //}

    //public void HighlightToggle(State state)
    //{
    //    switch (state)
    //    {
    //        case State.draggable:
    //            this.gameObject.GetComponent<MeshRenderer>().material.color = nodeNoHighlightColor;
    //            break;
    //        case State.highlighted:
    //            this.gameObject.GetComponent<MeshRenderer>().material.color = new Color(0.9f, 1, 0);
    //            break;
    //        case State.selectable:
    //            this.gameObject.GetComponent<MeshRenderer>().material.color = new Color(0.9f, 1, 0);
    //            break;
    //        case State.linking:
    //            this.gameObject.GetComponent<MeshRenderer>().material.color = new Color(0.9f, 1, 0);
    //            break;
    //    }
    //}
}


//    Func<int> getRandomNumber = () => new Random().Next(1, 100);

//    //Or 

//    Func<int, int, int> Sum = (x, y) => x + y;

    // OLD CODE - DON'T DELETE
    //// OnMouseDown
    //private void OnMouseDown()
    //{
    //    ReturnDownNode();
    //}
    //public void ReturnDownNode()
    //{
    //    Controller.Instance.returnHitDownNodeGO = this.gameObject;
    //}

    //// OnMouseUp
    //private void OnMouseOver()
    //{
    //    ReturnOverNode();
    //}
    //public void ReturnOverNode()
    //{
    //    Controller.Instance.returnHitUpNodeGO = this.gameObject;
    //}

    //void OnMouseExit()
    //{
    //    ReturnOffNode();
    //}
    //public void ReturnOffNode()
    //{
    //    Controller.Instance.returnHitUpNodeGO = null;
    //}

    //// OnMouseButtonUp (Up on the same object)
    //private void OnMouseUpAsButton()
    //{
    //    ReturnUpButtonNode();
    //}
    //public void ReturnUpButtonNode()
    //{
    //    Controller.Instance.returnHitButtonUpNodeGO = this.gameObject;
    //}

    //public GameObject ReturnDownGameobject()
    //{
    //    Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit hitInfo;

    //    if (Physics.Raycast(rayOrigin, out hitInfo))
    //    {
    //        var obj = hitInfo.collider.gameObject;
    //        if (obj != null)
    //        {
    //            //Debug.Log("This was called from the Node" + obj.ToString());
    //            return obj;
    //        }
    //    }
    //    Debug.Log("Object Inactive - Might receive a null error");
    //    return null;
    //}

    //public GameObject ReturnUpGameobject()
    //{
    //    Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit hitInfo;

    //    if (Physics.Raycast(rayOrigin, out hitInfo))
    //    {
    //        var obj = hitInfo.collider.gameObject; // This!
    //        if (obj != null)
    //        {
    //            //Debug.Log(obj.ToString());
    //            return obj;
    //        }
    //    }
    //    Debug.Log("Object Inactive - Might receive a null error");
    //    return null;
    //}