using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    // External References
    public DataContainer dataContainer;

    //Prefabs
    public GameObject linkPrefab;


    //Fields
    public Color nodeHighlightColor;
    public Color nodeNoHighlightColor;

    // Properties
    public string nodeName { get; set; }
    public int nodeID { get; set; }

    // INPUTS
    public GameObject inLink { get; set; }
    public GameObject inNode { get; set; }

    [SerializeField]
    public int sortedListCount = 0;
    public Dictionary<string, GameObject> inNodeSList { get; set; }


    // OUTPUTS 
    // A
    public GameObject outNodeA { get; set; }
    public GameObject outLinkA { get; set; }

    // B
    public GameObject outNodeB { get; set; }
    public GameObject outLinkB { get; set; }

    // ----------------------------------

    public int linkCount { get; set; }
    public int maxLinks { get; set; }

    public static bool queriesHitTriggers = true;   // ensures this object remains a HitTrigger for collider

    public bool isHighlighted { get; set; }

    private void Awake()
    {
        nodeName = "node";
        maxLinks = 2;
        inNodeSList = new Dictionary<string, GameObject>();
        this.gameObject.GetComponent<MeshRenderer>().material.color = this.nodeNoHighlightColor;
    }

    public bool isCapacity()
    {
        return this.linkCount == this.maxLinks ?  true : false;
    }

    public string ReturnName()
    {
        return this.nodeName;
    }

    public void AddInToSList(string nodeName, GameObject gameObject)
    {
    if(!inNodeSList.ContainsKey(nodeName))
        {
            inNodeSList.Add(nodeName, gameObject);
        }
        else
        {
            Debug.Log("These Nodes are already connected");
        }
    }

        public GameObject LinkSpawner(GameObject nodeGameObj, out LineRenderer lineLink, out Link currentLink)
    {
        GameObject currentGO = Instantiate(linkPrefab, nodeGameObj.transform.position, Quaternion.identity, nodeGameObj.transform);
        {
            //Assignments
            currentLink = currentGO.GetComponent<Link>();
            Node startNode = nodeGameObj.GetComponent<Node>();
            lineLink = currentLink.GetComponent<LineRenderer>();

            // Link Assignments
            currentLink.name = startNode.linkCount == 0 ? startNode.name + "Link" + " A" : startNode.name + "Link" + " B";
            currentLink.linkIndex = 0;
            currentLink.parentObject = nodeGameObj.transform;
            currentLink.startPos = nodeGameObj.transform.position;

            currentLink.linkOriginNode = nodeGameObj;

            //Node Assignments for the node that has just been clicked on to initiate the LineRenderer;
            startNode.linkCount++;

            if (startNode.linkCount <= 1)
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
            lineLink.enabled = true;
            lineLink.positionCount = 2;
            lineLink.startWidth = 0.2f;
            lineLink.SetPosition(0, currentLink.startPos);

            // Add Link GameObejct to List
            dataContainer.AddGameObjList(currentGO);
        }
        return currentGO;
    }

}





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