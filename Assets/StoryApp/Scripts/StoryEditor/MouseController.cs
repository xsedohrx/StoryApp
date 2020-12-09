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
    private LineRenderer lineLink;

    Link currentLink;

    private int currentIndex;
    private bool isHighlighted;     // TBD for highlighting node gameObj

    private Vector3 currentPos;
    private bool isDraggable;
    private bool isNodeDraggable;
    private bool isClicked;
    void Awake()
    {
        currentIndex = 0;
        currentGameObject = null;
        isNodeDraggable = false;
    }
    private void Update()
    {

        MouseLinkControls();
        MouseNodeControls();
    }
    private void MouseNodeControls()
    {
        if (MouseButtonDownRay() != null)
        {
            if(!MouseButtonDownRay().GetComponent<Node>().isHighlighted)
            {
                MouseButtonDownRay().GetComponent<MeshRenderer>().material.color = new Color(0.9f, 1,0);
                Debug.Log("This is Highlighted");
            }
            else
            {
                MouseButtonDownRay().GetComponent<MeshRenderer>().material.color = MouseButtonDownRay().GetComponent<Node>().nodeNoHighlightColor;
                Debug.Log("This is Not Highlighted");
            }
            Debug.Log(MouseButtonDownRay().GetComponent<Node>().isHighlighted);

            MouseButtonDownRay().GetComponent<Node>().isHighlighted = MouseButtonDownRay().GetComponent<Node>().isHighlighted ? false : true;
            isNodeDraggable = true;
        }
    }

    private void MouseLinkControls()
    {
        // If Mouse(0) is down
        if (MouseButtonDownRay() != null)
        {
            currentGameObject = MouseButtonDownRay();
            GameObject startNodeGO = currentGameObject;
            Node startNode = startNodeGO.GetComponent<Node>();

            if (startNode.isCapacity())
            {
                Debug.Log("Node at Capcity");
                isDraggable = false;
                isClicked = false;
            }
            else if (!startNode.isCapacity())
            {
                startNode.LinkSpawner(currentGameObject, out lineLink, out currentLink);
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
                GameObject startLinkGO = dataContainer.gameObjList[currentIndex].gameObject;
                GameObject endNodeGO = buttonUp;
                Node startNode = currentGameObject.gameObject.GetComponent<Node>();
                Node endNode = endNodeGO.GetComponent<Node>();
                Link currentLink = startLinkGO.GetComponent<Link>();

                // Set Link Attributes
                currentLink.endPos = buttonUp.transform.position;

                // Set Node IN Attributes
                endNode.inNode = currentGameObject;
                endNode.inLink = startLinkGO;



                endNode.AddInToSList(currentGameObject.name, currentGameObject);
                endNode.sortedListCount = endNode.inNodeSList.Count;

                // Set Node OUT Attributes
                if (startNode.linkCount == 1)
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

                // Update current index for hollding current temp variables
                currentIndex++;
                ResetTempVar();
            }
            // If mouse is up over the original gameObj
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
                    //Debug.Log("Hit Return suceeded : " + hitReturnUp.name);
                    return hitReturnUp;
                }
            }
            else
            {
                //Debug.Log("Hit Return was null");
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