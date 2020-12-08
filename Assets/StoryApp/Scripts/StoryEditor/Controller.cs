using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    // External References
    [SerializeField]
    private DataContainer dataContainer;
    
    //Prefabs
    public GameObject linkPrefab;

    // Temp | Dynamic Variables
    public GameObject currentGameObject;
    private int currentIndex;
    private bool isHighlighted;     // TBD for highlighting node gameObj
    private bool isDraggable;
    private bool isClicked;
    void Awake()
    {
        currentIndex = 0;
        currentGameObject = null;
    }
    private void Update()
    {

        MouseLinkControls();

    }

    private void MouseLinkControls()
    {
        // If Mouse(0) is down
        if (MouseButtonDownRay() != null)
        {
            currentGameObject = MouseButtonDownRay();
            GameObject startNodeGO = currentGameObject;
            GameObject currentLinkGO;
            Link currentLink;
            Node startNode = startNodeGO.GetComponent<Node>();


            if (startNode.isCapacity())
            {
                Debug.Log("Node at Capcity");
                isDraggable = false;
                isClicked = false;
            }
            else if (!startNode.isCapacity() && startNode.linkCount == 0)
            {
                currentLinkGO = Instantiate(linkPrefab, currentGameObject.transform.position, Quaternion.identity, currentGameObject.transform);
                {
                    // Link Assignments
                    currentLink = currentLinkGO.GetComponent<Link>();
                    currentLink.name = startNode.name + "Link" + " A";
                    currentLink.linkID = currentIndex;
                    currentLink.linkIndex = 0;
                    currentLink.parentObject = startNodeGO.transform;
                    currentLink.startPos = startNodeGO.transform.position;
                    currentLink.lineLink = currentLinkGO.GetComponent<LineRenderer>();
                    currentLink.linkOriginNode = currentLinkGO;

                    //Node Assignments;
                    startNode.linkCount = 1;
                    startNode.outLinkA = currentLinkGO;

                    // LineRenderer Start Position Assignments
                    currentLink.lineLink.enabled = true;
                    currentLink.lineLink.positionCount = 2;
                    currentLink.lineLink.startWidth = 0.2f;
                    currentLink.lineLink.SetPosition(0, currentLink.startPos);

                    // Add Link GameObejct to List
                    dataContainer.AddGameObjList(currentLinkGO);
                }
                isDraggable = true;
                isClicked = true;
            }
            else if (!startNode.isCapacity() && startNode.linkCount == 1)
            {
                currentLinkGO = Instantiate(linkPrefab, currentGameObject.transform.position, Quaternion.identity, currentGameObject.transform);
                {
                    // Link Assignments
                    currentLink = currentLinkGO.GetComponent<Link>();
                    currentLink.name = startNode.name + "Link" + " B";
                    currentLink.linkID = currentIndex;
                    currentLink.linkIndex = 0;
                    currentLink.parentObject = startNodeGO.transform;
                    currentLink.startPos = startNodeGO.transform.position;
                    currentLink.lineLink = currentLinkGO.GetComponent<LineRenderer>();
                    currentLink.linkOriginNode = currentLinkGO;

                    //Node Assignments;
                    startNode.linkCount = 2;
                    startNode.outLinkB = currentLinkGO;

                    // LineRenderer Start Position Assignments
                    currentLink.lineLink.enabled = true;
                    currentLink.lineLink.positionCount = 2;
                    currentLink.lineLink.startWidth = 0.2f;
                    currentLink.lineLink.SetPosition(0, currentLink.startPos);

                    // Add Link GameObejct to List
                    dataContainer.AddGameObjList(currentLinkGO);
                }
                isDraggable = true;
                isClicked = true;
            }
            else if (currentGameObject == null)
            {
                isClicked = false;
                isDraggable = false;
                Debug.Log("Nothing Selected");
            }
        }
        // If Mouse(0) is dragged
        else if (Input.GetMouseButton(0) && MouseButtonDownRay() == null)
        {
            if (isDraggable && isClicked)
            {
                LineRenderer lineLink = dataContainer.ReturnLineRendererFromList(currentIndex);

                Vector3 currentPos = currentGameObject.transform.position;

                lineLink.SetPosition(1, currentPos);
                lineLink.positionCount = 2;

                currentPos = GetCurrentMousePosition().GetValueOrDefault();
                lineLink.SetPosition(1, currentPos);
            }
        }
        // If Mouse(0) is up
        else if (MouseButtonUpRay() && isClicked)
        {
            isDraggable = false;

            GameObject buttonUp = MouseButtonUpRay();

            if (currentGameObject != null && currentGameObject != buttonUp && isClicked)
            {
                // Assign temp variables for the mouse up over end node event
                GameObject startNodeGO = dataContainer.gameObjList[currentIndex].gameObject;
                GameObject endNodeGO = buttonUp;
                Node startNode = currentGameObject.gameObject.GetComponent<Node>();
                Node endNode = endNodeGO.GetComponent<Node>();
                Link currentLink = startNodeGO.GetComponent<Link>();
                LineRenderer lineLink = dataContainer.ReturnLineRendererFromList(currentIndex);

                // Set Link Attributes
                currentLink.endPos = buttonUp.transform.position;

                // Set Node Attributes
                endNode.inNode = currentGameObject;
                endNode.inLink = startNodeGO;

                if (startNode.linkCount == 1)
                {
                    startNode.outNodeA = endNodeGO;
                }
                else
                {
                    startNode.outNodeB = endNodeGO;
                }

                // Set LineRenderer Attributes
                lineLink.SetPosition(1, currentLink.endPos);
                lineLink.positionCount = 2;

                // Update current index for hollding current temp variables
                currentIndex++;
                ResetTempVar();
            }
            else if (currentGameObject && MouseButtonUpRay() != null && currentGameObject.GetComponent<Collider>() == MouseButtonUpRay().GetComponent<Collider>() && isClicked) // Mouse Up As Button
            {
                RemoveCurrentLink();
                ResetTempVar();
            }
        }
        else if (currentGameObject != null && MouseButtonUpRay() == null && isClicked)
        {
            RemoveCurrentLink();
            ResetTempVar();
        }
    }

    // Delete/Reset Methods
    private void ResetTempVar()
    {
        currentGameObject = null;
        isClicked = false;
    }

    private void RemoveCurrentLink()
    {
        currentGameObject.GetComponent<Node>().linkCount--;
        var lineDelete = dataContainer.gameObjList[currentIndex].gameObject;
        dataContainer.RemoveGameObjList(currentIndex);
        Destroy(lineDelete);
    }

    // Raycasts
    GameObject MouseButtonDownRay()
    {
        GameObject hitReturnDown = null;
        if (Input.GetMouseButtonDown(0))
        {
            Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(rayOrigin, out hitInfo))
            {
                hitReturnDown = hitInfo.collider.gameObject;
            }
        }
        return hitReturnDown;
    }
    GameObject MouseButtonUpRay()
    {
        GameObject hitReturnUp = null;
        if (Input.GetMouseButtonUp(0))
        {
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