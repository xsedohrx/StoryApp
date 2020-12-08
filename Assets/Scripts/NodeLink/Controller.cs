using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField]
    private DataContainer dataContainer;
    public GameObject linkPrefab;

    private List<GameObject> linkList;
    public Link link;
    public Link getLink;


    private List<GameObject> gameObjectList;
    public GameObject returnHitDownNodeGO;
    public GameObject returnHitUpNodeGO;
    public GameObject returnHitButtonUpNodeGO;


    public GameObject node;

    public Vector3? currentPos;
    public GameObject currentGameObject;
    public Node currentNode;
    public LineRenderer currentLineRenderer;
    public Link currentLink;


    private Link returnLinkComponent;   // Unused?

    private Link returnLink;    // Unused?

    public Link tempLink;

    //LineRenderer linkLine;

    public int currentIndex;
    private bool isDraggable;
    private bool isClicked;

    //int listIndex;

    private static Controller _instance;
    public static Controller Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("ControlManager is NULL");

            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
        currentIndex = 0;
    }
    private void Start()
    {
        Debug.Log(dataContainer.myString);
        Debug.Log(dataContainer.mySeriealizedString);

        //returnLinkComponent = linkPrefab.GetComponent<Link>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && returnHitDownNodeGO != null)
        {
            currentNode = returnHitDownNodeGO.GetComponent<Node>();

            Debug.Log(returnHitDownNodeGO.name);

            if (currentNode.isCapacity())
            {
                Debug.Log(currentNode.linkCount);
                Debug.Log("Node at Capcity");
                isDraggable = false;
                isClicked = false;
            }

            else if (!currentNode.isCapacity() && currentNode.linkCount == 0)
            {
                Debug.Log("linkCount + " + currentNode.linkCount);
                GameObject linkGO = Instantiate(linkPrefab, returnHitDownNodeGO.transform.position, Quaternion.identity, returnHitDownNodeGO.transform);
                {
                    Link link = linkGO.GetComponent<Link>();
                    link.name = currentNode.name + "Link" + " A";
                    link.linkID = currentIndex;
                    link.linkIndex = 0;
                    link.parentObject = returnHitDownNodeGO.transform;
                    link.previousNode = returnHitDownNodeGO;
                    link.startPos = returnHitDownNodeGO.transform.position;
                    link.lineLink = link.GetLineRenderer();

                    link.lineLink.enabled = true;
                    link.lineLink.positionCount = 2;
                    link.lineLink.startWidth = 0.2f;
                    link.lineLink.SetPosition(0, link.startPos);

                    currentNode.inputNodeID = link.linkID;
                    currentNode.linkCount = 1;

                    dataContainer.AddGameObjList(linkGO);

                    currentLink = link;
                }
                isDraggable = true;
                isClicked = true;
            }
            else if (!currentNode.isCapacity() && currentNode.linkCount == 1)
            {
                GameObject linkGO = Instantiate(linkPrefab, returnHitDownNodeGO.transform.position, Quaternion.identity, returnHitDownNodeGO.transform);
                {
                    Link link = linkGO.GetComponent<Link>();
                    link.name = currentNode.name + "Link" + " B";
                    link.linkID = currentIndex;
                    link.linkIndex = 0;
                    link.parentObject = returnHitDownNodeGO.transform;
                    link.previousNode = returnHitDownNodeGO;
                    link.startPos = returnHitDownNodeGO.transform.position;
                    link.lineLink = link.GetLineRenderer();

                    link.lineLink.enabled = true;
                    link.lineLink.positionCount = 2;
                    link.lineLink.startWidth = 0.2f;
                    link.lineLink.SetPosition(0, link.startPos);


                    currentNode.inputNodeID = link.linkID;
                    currentNode.linkCount = 2;

                    dataContainer.AddGameObjList(linkGO);

                    currentLink = link;
                }
                isDraggable = true;
                isClicked = true;
            }
            else if (returnHitDownNodeGO && returnHitButtonUpNodeGO && returnHitUpNodeGO == null)
            {
                isClicked = false;
                isDraggable = false;
                Debug.Log("Nothing Selected");
            }
        }
        else if (Input.GetMouseButton(0))
        {
            if (isDraggable)    // THE POINTS ARE OVERTITING HTEMSELVES
            {
                LineRenderer lineLink = dataContainer.ReturnLineRendererFromList(currentIndex);

                Vector3 currentPos = GetCurrentMousePosition().GetValueOrDefault();

                lineLink.SetPosition(1, currentPos);
                lineLink.positionCount = 2;


            }
        }


        else if (Input.GetMouseButtonUp(0) && isClicked)   // && returnHitDownNodeGO.GetComponent<Link>().previousNode.transform.childCount < 3
        {
            isDraggable = false;
            if (returnHitUpNodeGO != null && returnHitButtonUpNodeGO == null && isClicked)
            {
                //setting endposf
                Debug.Log("Released Game Object : " + returnHitUpNodeGO.name);
                LineRenderer lineLink = dataContainer.ReturnLineRendererFromList(currentIndex);
                Debug.Log("INDEX : " + currentIndex);
                Debug.Log("GameObjectList size = " + dataContainer.gameObjList.Count);
                Debug.Log("GAME OBJECT??? LINE RENDERER = " + dataContainer.gameObjList[currentIndex].gameObject.name);
                Debug.Log("Final LinkLine = " + lineLink.transform.parent.name);

                Vector2 endPos = returnHitUpNodeGO.transform.position;
                dataContainer.gameObjList[currentIndex].gameObject.GetComponent<Link>().endPos = endPos;
                Debug.Log("EndPos: " + returnHitUpNodeGO.name);

                lineLink.SetPosition(1, endPos);
                lineLink.positionCount = 2;

                //returnHitDownNodeGO.GetComponent<Node>().linkCount++;
                currentIndex++;
            }
            else if (returnHitDownNodeGO && returnHitButtonUpNodeGO != null && returnHitDownNodeGO.GetComponent<Collider>() == returnHitButtonUpNodeGO.GetComponent<Collider>() && isClicked) // Mouse Up As Button
            {
                //Debug.Log(gameObjectList[currentIndex].gameObject);
                returnHitDownNodeGO.GetComponent<Node>().linkCount--;
                var lineDelete = dataContainer.gameObjList[currentIndex].gameObject;
                dataContainer.RemoveGameObjList(currentIndex);
                Destroy(lineDelete);
                //dataContainer.linkList.RemoveAt(currentIndex);
            }
            else
            {
                returnHitDownNodeGO.GetComponent<Node>().linkCount--;
                var lineDelete = dataContainer.gameObjList[currentIndex].gameObject;
                dataContainer.RemoveGameObjList(currentIndex);
                Destroy(lineDelete);
                Debug.Log("No Selection on UpClick");
            }
            returnHitDownNodeGO = null;
            returnHitButtonUpNodeGO = null;
            isClicked = false;
        }
    }

    public Vector3? GetCurrentMousePosition()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var plane = new Plane(Vector3.forward, Vector3.zero);

        float rayDistance;
        if (plane.Raycast(ray, out rayDistance))
        {
            return ray.GetPoint(rayDistance);
        }

        return null;
    }
}






//    //private void OnMouseUp()
//    //{
//    //    //    bool moo;
//    //    //if(ReturnUpGameobject().GetType() == typeof(GameObject))
//    //    //{
//    //    //    moo = true;
//    //    //}
//    //    //else
//    //    //{
//    //    //    moo = false;
//    //    //}
//    //    //Debug.Log("MOO = " + moo);

//    //    if (ReturnUpGameobject() != null && linkList[indexContainer].previousNode.transform.childCount < 3)   // != null && ReturnGameobject().name == this.name   | ReturnGameobject().GetType() == typeof(Node)
//    //    {
//    //        //setting endpos
//    //        Debug.Log(ReturnUpGameobject() + "OBject on button release");
//    //        LineRenderer linkLine = new LineRenderer();
//    //        linkLine = gameObjectList[indexContainer].gameObject.GetComponent<LineRenderer>();
//    //        Debug.Log("INDEX : " + indexContainer);
//    //        Debug.Log("GameObjectList size = " + gameObjectList.Count);
//    //        Debug.Log("GAME OBJECT??? LINE RENDERER = " + gameObjectList[indexContainer].gameObject.name.ToString());
//    //        Debug.Log("Final LinkLine = " + linkLine.transform.parent.name.ToString());
//    //        Vector2 endPos = ReturnUpGameobject().transform.position;
//    //        linkList[indexContainer].endPos = endPos;

//    //        Debug.Log("EndPos: " + ReturnUpGameobject().name.ToString());
//    //        linkLine.SetPosition(1, endPos);
//    //        linkLine.positionCount = 2;
//    //    }
//    //    else if (linkList[indexContainer].previousNode.transform.childCount < 3 && ReturnUpGameobject() == null)
//    //    {
//    //        //Have a variable to previous gameobject
//    //        //Access the transform child

//    //        //loop last object created.
//    //        //Node destroyer = previousNode.transform.GetChild(lastChild - 1).gameObject.GetComponent<Node>();
//    //        //Destroy(destroyer.gameObject);

//    //        var lineDelete = gameObjectList[indexContainer].gameObject;
//    //        Debug.Log("Child : " + indexContainer + " removed");
//    //        linkList.RemoveAt(indexContainer);
//    //        gameObjectList.RemoveAt(indexContainer);
//    //        Destroy(lineDelete);
//    //        Debug.Log("listIndex Count = " + indexContainer);

//    //        //Remove link from List and destroy
//    //        //var lineLink = linkList[exitLink].gameObject;
//    //        //linkList.RemoveAt(exitLink);
//    //        //Destroy(lineLink);
//    //    }
//    //    else { Debug.LogError("NEW ERROR FOUND"); }
//    //    isDraggable = false;
//    //}






//private void AddLinkToList(GameObject link)
//    {
//        dataContainer.AddLinkToList(link);
//    }
//    public GameObject RemoveLinkFromList(int index)
//    {
//        return dataContainer.RemoveLinkFromList(index);
//    }

//    private void AddGameObjToList(GameObject gameObj)
//    {
//        dataContainer.AddGameObjList(gameObj);
//    }
//    public GameObject RemoveGameObjFromList(int index)
//    {
//        return dataContainer.RemoveGameObjList(index);
//    }

// ------
//private Link ReturnHitLink()
//{
//    return ReturnHitNode().GetComponent<Link>();
//}

//private string ReturnHitNode()
//{
//    return ReturnNode();
//}

//private LineRenderer ReturnHitLineRenderer()
//{
//    return ReturnHitNode().GetComponent<LineRenderer>();
//}








// SAVED CODE

//public void AddGameobjectDic(int index, GameObject gameObject)
//    {
//        dataContainer.AddGameobjectDic(index, gameObject);
//    }
//    public GameObject RemoveGameobjectDic(int index)
//{
//    GameObject gameObj = new GameObject();
//    dataContainer.AddGameobjectDic(index, gameObj);

//    return gameObj;
//}




////if (linkList.Count == 0)
////    {
////        link.linkIndex = 0;
////    }
////    else
////    {
////        link.linkIndex = linkList.Count - 1;
////        indexContainer = link.linkIndex;
////    }