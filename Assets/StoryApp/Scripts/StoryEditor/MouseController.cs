using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    // External References
    [SerializeField]
    private DataContainer dataContainer;
    
    //Prefabs
    //public GameObject linkPrefab;

    // Temp | Dynamic Variables
    public GameObject currentGameObject;
    public GameObject currentLinkGO;

    public enum State
    {
        draggable,
        highlighted,
        selectable,
        linking
    }

    [SerializeField] public State state;

    // Node Dynamic References
    public GameObject startNodeGO;
    public GameObject endNodeGO;

    private Node startNode;
    private Node endNode;

    private LineRenderer lineLink;


    private Link currentLink;
    private string currentlinkName;

    private int currentIndex;
    public bool isHighlighted;   

    private Vector3 currentPos;
    private bool isDraggable;
    private bool isClicked;
    private int clickCount;

    bool is_down;

    void Awake()
    {
        currentIndex = 0;
        currentGameObject = null;
        currentLinkGO = null;
        clickCount = 0;

        startNodeGO = null;
        endNodeGO = null;

        isDraggable = false;
        this.state = State.draggable;
    }
    private void Update()
    {
        switch(state)
        {
            case State.draggable:
                ResetTempVar();
                if (startNodeGO != null)
                {
                    startNodeGO.GetComponent<MeshRenderer>().material.color = startNode.nodeNoHighlightColor;
                }
                startNodeGO = null;
                if (Input.GetMouseButtonDown(0))
                {
                    startNodeGO = null;
                    MouseButtonDownRay();
                    startNodeGO = MouseButtonDownRay();


                    if (startNodeGO != null && startNodeGO.GetComponent<Node>())
                    {
                        startNode = startNodeGO.GetComponent<Node>();

                        startNode = startNodeGO.GetComponent<Node>();

                        state = State.highlighted;
                    }
                }
                //if (Input.GetMouseButton(0))
                //{
                //    //
                //}
                //if (Input.GetMouseButtonUp(0))
                //{
                //    //
                //}
                break;

            case State.highlighted:

                if (startNodeGO != null)
                {
                    startNodeGO.GetComponent<MeshRenderer>().material.color = startNode.nodeHighlightColor;
                }

                if (Input.GetMouseButtonDown(0))
                {
                    if (MouseButtonDownRay() == null)
                    {
                        is_down = false;
                        startNodeGO.GetComponent<MeshRenderer>().material.color = startNode.nodeNoHighlightColor;
                        state = State.draggable;
                    }
                    else if (startNodeGO != null && startNode.isCapacity())
                    {
                        Debug.Log("Node at Capcity");
                        startNodeGO.GetComponent<MeshRenderer>().material.color = startNode.nodeNoHighlightColor;
                        state = State.draggable;
                    }
                    else if (startNodeGO != null && startNodeGO.GetComponent<Node>() && MouseButtonDownRay().GetComponent<Collider>() == startNodeGO.GetComponent<Collider>())
                    {
                        startNode = startNodeGO.GetComponent<Node>();

                        is_down = true;
                        startNode.LinkSpawner(startNodeGO, out lineLink, out currentLink, out currentlinkName, out currentLinkGO);
                        state = State.selectable;
                    }
                    else if (startNodeGO != null && startNodeGO.GetComponent<Node>() && MouseButtonDownRay().GetComponent<Collider>() != startNodeGO.GetComponent<Collider>())
                    {
                        is_down = false;
                        startNodeGO.GetComponent<MeshRenderer>().material.color = startNode.nodeNoHighlightColor;
                        state = State.draggable;
                    }
                    break;
                }
                else if (Input.GetMouseButton(0))
                {
                    currentPos = GetCurrentMousePosition().GetValueOrDefault();
                    startNodeGO.transform.position = currentPos;
                    startNode.DragOutNodes(currentPos);
                    startNode.DragInNodes(currentPos);
                }
                //if (Input.GetMouseButtonUp(0))
                //{
                //    //
                //}


                break;

            case State.selectable:

                //if (Input.GetMouseButtonDown(0))
                //{
                //    //
                //}
                if (Input.GetMouseButton(0) && is_down)
                {
                    currentPos = GetCurrentMousePosition().GetValueOrDefault();
                    lineLink.SetPosition(1, currentPos);
                }

                if (Input.GetMouseButtonUp(0))
                {
                    if(startNodeGO && MouseButtonUpRay() != null && startNodeGO.GetComponent<Collider>() != MouseButtonUpRay().GetComponent<Collider>())
                    {
                        MouseButtonUpRay();
                        endNodeGO = MouseButtonUpRay();
                        endNode = endNodeGO.GetComponent<Node>();
                        if (endNode.duplicateNodeConnectionCheck(startNodeGO.name))
                        {
                            startNode.RemoveOutLinkDict(currentlinkName);
                            state = State.highlighted;
                        }
                        else if (!endNode.duplicateNodeConnectionCheck(currentlinkName))
                        {
                            currentLink.endPos = endNodeGO.transform.position;

                            // Set Node IN Attributes
                            endNode.inNode = startNodeGO;         // Todo : update this to show dictionary contents in text box

                            endNode.AddInNodeDict(startNodeGO.name, startNodeGO);

                            // Set Node OUT Attributes
                            if (startNode.outLinkCount == 1)
                            {
                                startNode.outNodeA = endNodeGO;
                            }
                            else
                            {
                                startNode.outNodeB = endNodeGO;
                            }

                            // Set LineRenderer Attributes
                            lineLink.positionCount = 2;
                            lineLink.SetPosition(1, currentLink.endPos);


                            endNode.AddInNodeDict(currentlinkName, startNodeGO);
                            endNode.AddInLinkDict(currentlinkName, startNodeGO);
                            state = State.highlighted;
                        }
                    }

                    else if (startNodeGO && MouseButtonUpRay() != null && startNodeGO.GetComponent<Collider>() == MouseButtonUpRay().GetComponent<Collider>()) // Mouse Up As Button
                    {
                        endNodeGO = MouseButtonUpRay();
                        endNode = endNodeGO.GetComponent<Node>();
                        endNode.inNodeCount = endNode.inNodeDictionary.Count;
                        startNodeGO.GetComponent<MeshRenderer>().material.color = startNode.nodeNoHighlightColor;
                        startNode.RemoveOutLinkDict(currentlinkName);
                        state = State.draggable;
                    }
                    else if (startNodeGO != null && MouseButtonUpRay() == null)
                    {
                        startNode.RemoveOutLinkDict(currentlinkName);

                        state = State.highlighted;
                    }

                }

                //if (Input.GetKey(KeyCode.LeftShift) && Input.GetMouseButton(0))
                //{
                //    if (startNodeGO != null && startNodeGO.GetComponent<Node>() && !startNode.isCapacity())
                //    {


                //        startNode.LinkSpawner(startNodeGO, out lineLink, out currentLink, out currentlinkName);
                //        state = State.linking;

                //    }
                //}
                //else if (Input.GetMouseButtonUp(0))
                //{
                //    state = State.draggable;
                //}


                break;
            case State.linking:

                //if (Input.GetMouseButtonDown(0))
                //{
                //    // 
                //}
                //if (Input.GetMouseButton(0) && is_down)
                //{
                //    startNode.LinkSpawner(startNodeGO, out lineLink, out currentLink, out currentlinkName);
                //}
                //if (Input.GetMouseButtonUp(0))
                //{
                //    //
                //}

                break;
        }

        //// If Mouse(0) is down
        //if (Input.GetMouseButtonDown(0) && MouseButtonDownRay() != null)
        //{
        //    MouseButtonDownRay();
        //    startNodeGO = MouseButtonDownRay();

        //    if (startNodeGO.GetComponent<Node>().state == Node.State.draggable)
        //    {
        //        if (startNodeGO != null && startNodeGO.GetComponent<Node>())
        //        {

        //            startNode = startNodeGO.GetComponent<Node>();
        //            startNode.HighlightToggle(Node.State.highlighted);
        //        }

        //    }
        //    else if (startNodeGO.GetComponent<Node>().state == Node.State.highlighted)
        //    {
        //        if (startNodeGO != null && startNode.isCapacity())
        //        {
        //            Debug.Log("Node at Capcity");
        //        }
        //        else if (startNodeGO != null && startNodeGO.GetComponent<Node>() && !startNode.isCapacity())
        //        {
        //            startNode.HighlightToggle(Node.State.selectable);

        //            startNode.LinkSpawner(startNodeGO, out lineLink, out currentLink, out currentlinkName);
        //        }
        //        else if (startNodeGO.GetComponent<Node>().state == Node.State.selectable)
        //        {
        //            isClicked = true;
        //        }
        //        else if (startNodeGO.GetComponent<Node>().state == Node.State.closed)
        //        {
        //        }
        //    }
        //}

        //// If Mouse(0) is dragged
        //else if (Input.GetMouseButton(0))
        //{
        //    if(isClicked)
        //    {

        //        currentPos = GetCurrentMousePosition().GetValueOrDefault();
        //        lineLink.SetPosition(1, currentPos);
        //    }

        //}
        //// If Mouse(0) is up
        //else if (Input.GetMouseButton(0) && MouseButtonUpRay() != null)
        //{
        //    endNodeGO = MouseButtonUpRay();

        //    if (MouseButtonUpRay().GetComponent<Node>().state == Node.State.draggable)
        //    {
        //        if (MouseButtonUpRay() != null)
        //        {
        //            Debug.Log("Clicked to Highlight");
        //        }
        //    }
        //    if (MouseButtonUpRay().GetComponent<Node>().state == Node.State.highlighted)
        //    {
        //        Debug.Log("Skipped Up Selectable");
        //    }
        //    if (MouseButtonUpRay().GetComponent<Node>().state == Node.State.selectable)
        //    {


        //    }
        //    if (MouseButtonUpRay().GetComponent<Node>().state == Node.State.linking)
        //    {
        //        endNodeGO = MouseButtonUpRay();
        //        endNodeGO = MouseButtonUpRay();
        //        if (endNodeGO && startNode != null)
        //        {
        //            // Assign temp variables for the mouse up over end node event
        //            endNodeGO = MouseButtonUpRay();
        //            Node endNode = endNodeGO.GetComponent<Node>();

        //            // Set Link Attributes
        //            //startNode.linkCount++;
        //            currentLink.endPos = endNodeGO.transform.position;

        //            // Set Node IN Attributes
        //            endNode.inNode = startNodeGO;         // Todo : update this to show dictionary contents in text box

        //            endNode.AddInToSList(startNodeGO.name, startNodeGO);
        //            endNode.sortedListCount = endNode.inNodeSList.Count;

        //            // Set Node OUT Attributes
        //            if (startNode.linkCount == 1)
        //            {
        //                startNode.outNodeA = endNodeGO;
        //            }
        //            else
        //            {
        //                startNode.outNodeB = endNodeGO;
        //            }

        //            // Set LineRenderer Attributes
        //            lineLink.positionCount = 2;
        //            lineLink.SetPosition(1, currentLink.endPos);
        //        }
        //        else if (startNodeGO && endNodeGO != null && startNodeGO.GetComponent<Collider>() == endNodeGO.GetComponent<Collider>() && isClicked) // Mouse Up As Button
        //        {

        //            startNode.RemoveOutLinksSDict(currentlinkName);
        //            Debug.Log("currentlinkName : " + currentlinkName);

        //            startNode.HighlightToggle(Node.State.draggable);
        //            ResetTempVar();
        //        }
        //        else if (startNodeGO != null && endNodeGO == null && isClicked)
        //        {
        //            startNode.RemoveOutLinksSDict(currentlinkName);
        //            Debug.Log("currentlinkName : " + currentlinkName);

        //            startNode.HighlightToggle(Node.State.draggable);
        //            ResetTempVar();
        //        }
        //        else
        //        {
        //            Debug.Log("fuck");
        //        }
        //    }

        //}
    }

    //void Foo()
    //{
    //    StartCoroutine(Bar((timer) => {
    //        if (timer) 
    //        {
    //            Debug.Log("over 2 seconds");
    //        }
    //        else { Debug.Log("under 2 seconds"); }
    //    }));

    //}
    //IEnumerator Bar(System.Action<bool> callback)
    //{

    //    float timer = 0;
    //    timer += Time.deltaTime;
    //    if (Input.GetMouseButtonUp(0))
    //    {
    //        if(timer >= 2.0f)
    //        {
    //            callback(true);
    //        }
    //        else
    //        {
    //            callback(false);
    //        }
    //    }
    //    yield return null;
    //}

    // Delete/Reset Methods
    private void ResetTempVar()
    {
        currentLinkGO = null;
        currentLink = null;
        currentlinkName = null;
        startNodeGO = null;
        endNodeGO = null;
        startNode = null;
        endNode = null;
        lineLink = null;
    }

    //private void RemoveCurrentLink(GameObject node)
    //{
    //    int childCount = node.transform.childCount;
        
    //    //currentGameObject.GetComponent<Node>().linkCount--;
    //    //var lineDelete = dataContainer.gameObjList[currentIndex].gameObject;
    //    var lineDelete = node.transform.GetChild(childCount + 1).gameObject;
    //    //dataContainer.RemoveGameObjList(currentIndex);
    //    Destroy(lineDelete);
    //}

    // Raycasts
    GameObject MouseButtonDownRay()
    {
        GameObject hitReturnDown = null;

        Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(rayOrigin, out hitInfo))
        {
            hitReturnDown = hitInfo.collider.gameObject;
        }
    return hitReturnDown;
    }
    GameObject MouseButtonUpRay()
    {
        GameObject hitReturnUp = null;
        Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(rayOrigin, out hitInfo))
        {
            hitReturnUp = hitInfo.collider.gameObject;
            if (hitReturnUp != null)
            {
                Debug.Log("Hit Return suceeded : " + hitReturnUp.name);
                return hitReturnUp;
            }
        }
        else
        {
            Debug.Log("Hit Return was null");
            return null;
        }
        return null;
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
