using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace StoryApp
{
    public class MouseController : MonoBehaviour
    {
        // External References
        [SerializeField]
        //private DataContainer dataContainer;          //TBD delete

        // Temp | Dynamic Variables
        public GameObject currentGameObject;
        public GameObject currentLinkGO;
        public enum State
        {
            draggable,
            highlighted,
            selectable
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

        private CapsuleCollider capsule;

        private int currentIndex;           //Keep
        public bool isHighlighted;

        private Vector3 currentPos;

        bool is_down;

        //Actions & Events
        public static Action<GameObject, ColorPallete.NodeColor> OnNodeColor;
        public static Action<LineRenderer, Vector3> OnSetLineEndPos;

        void Awake()
        {
            currentIndex = 0;
            currentGameObject = null;
            currentLinkGO = null;

            startNodeGO = null;
            endNodeGO = null;

            this.state = State.draggable;
        }
        private void Update()
        {
            switch (state)
            {
                case State.draggable:
                    ResetTempVar();
                    if (startNodeGO != null)
                    {
                        OnNodeColor?.Invoke(startNodeGO, ColorPallete.NodeColor.draggable);
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
                    //if (Input.GetMouseButtonUp(0))
                    break;

                case State.highlighted:

                    if (startNodeGO != null)
                    {
                        OnNodeColor?.Invoke(startNodeGO, ColorPallete.NodeColor.highlighted);
                    }

                    if (Input.GetMouseButtonDown(0))
                    {
                        if (MouseButtonDownRay() == null)
                        {
                            is_down = false;
                            OnNodeColor?.Invoke(startNodeGO, ColorPallete.NodeColor.draggable);
                            state = State.draggable;
                        }
                        else if (startNodeGO != null && startNode.isCapacity())
                        {
                            Debug.Log("Node at Capcity");
                            OnNodeColor?.Invoke(startNodeGO, ColorPallete.NodeColor.draggable);
                            state = State.draggable;
                        }
                        else if (startNodeGO != null && startNodeGO.GetComponent<Node>() && MouseButtonDownRay().GetComponent<Collider>() == startNodeGO.GetComponent<Collider>())
                        {
                            startNode = startNodeGO.GetComponent<Node>();

                            is_down = true;
                            startNode.LinkSpawner(startNodeGO, out lineLink, out capsule, out currentLink, out currentlinkName, out currentLinkGO);
                            startNode.AddOutLinkDict(currentlinkName, currentLinkGO);

                            state = State.selectable;
                        }
                        
                        else if (startNodeGO != null && startNodeGO.GetComponent<Node>() && MouseButtonDownRay().GetComponent<Collider>() != startNodeGO.GetComponent<Collider>())
                        {
                            is_down = false;
                            OnNodeColor?.Invoke(startNodeGO, ColorPallete.NodeColor.draggable);
                            state = State.draggable;
                        }
                        //break;
                    }
                    else if (Input.GetMouseButton(0))
                    {
                        currentPos = GetCurrentMousePosition().GetValueOrDefault();
                        startNodeGO.transform.position = currentPos;
                        startNode.DragOutLinks(currentPos);
                        startNode.DragInLinks(currentPos);
                    }
                    else if (Input.GetMouseButtonUp(0))
                    {
                        startNode.DragInColliders(currentPos);
                        startNode.DragOutColliders(currentPos);
                    }
                    else if (Input.GetMouseButtonUp(1))
                    {
                        if (MouseButtonDownRay() != null)
                        {
                            Debug.Log("right cick " + MouseButtonDownRay().gameObject.name);
                        }
                    }
                    break;

                case State.selectable:

                    if (Input.GetMouseButton(0) && is_down)
                    {
                        currentPos = GetCurrentMousePosition().GetValueOrDefault();
                        lineLink.SetPosition(1, currentPos);
                    }

                    if (Input.GetMouseButtonUp(0))
                    {
                        if (startNodeGO && MouseButtonUpRay() != null && startNodeGO.GetComponent<Collider>() != MouseButtonUpRay().GetComponent<Collider>())
                        {
                            // Scope Assignments
                            endNodeGO = MouseButtonUpRay();
                            endNode = endNodeGO.GetComponent<Node>();
                            if (endNode.duplicateNodeConnectionCheck(startNodeGO.name))
                            {
                                startNode.RemoveOutLinkDict(currentlinkName);
                                state = State.highlighted;
                            }
                            else if (!endNode.duplicateNodeConnectionCheck(currentlinkName))
                            {
                                // Set Link OUT Attributes
                                currentLink.endPos = endNodeGO.transform.position;
                                currentLink.linkDestinationNode = endNodeGO;

                                // Set Node IN Attributes
                                endNode.inNode = startNodeGO;         // Todo : update this to show dictionary contents in text box
                                endNode.AddInNodeDict(startNodeGO.name, startNodeGO);

                                // Set Node OUT Attributes
                                startNode.AddOutNodeDict(currentlinkName, endNodeGO);
                                if (startNode.outLinkCount == 1)
                                {
                                    startNode.outNodeA = endNodeGO;
                                }
                                else
                                {
                                    startNode.outNodeB = endNodeGO;
                                }

                                // Set LineRenderer Attributes
                                OnSetLineEndPos?.Invoke(lineLink, currentLink.endPos);


                                // Capsule Collider Assignments
                                capsule.transform.position = startNodeGO.transform.position + (endNodeGO.transform.position - startNodeGO.transform.position) / 2;
                                capsule.transform.LookAt(startNodeGO.transform.position);
                                capsule.height = (endNodeGO.transform.position - startNodeGO.transform.position).magnitude;

                                //Update Dictionaries
                                endNode.AddInLinkDict(currentlinkName, startNodeGO);

                                //Set new State
                                state = State.highlighted;
                            }
                        }
                        else if (startNodeGO && MouseButtonUpRay() != null && startNodeGO.GetComponent<Collider>() == MouseButtonUpRay().GetComponent<Collider>()) // Mouse Up As Button
                        {
                            endNodeGO = MouseButtonUpRay();
                            endNode = endNodeGO.GetComponent<Node>();


                            startNode.RemoveOutLinkDict(currentlinkName);

                            OnNodeColor?.Invoke(startNodeGO, ColorPallete.NodeColor.draggable);
                            state = State.draggable;
                        }
                        else if (startNodeGO != null && MouseButtonUpRay() == null)
                        {
                            startNode.RemoveOutLinkDict(currentlinkName);

                            state = State.highlighted;
                        }
                    }
                    break;
            }
        }

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

        // Raycasts
        GameObject MouseButtonDownRay()
        {
            GameObject hitReturnDown = null;

            Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(rayOrigin, out hitInfo))
            {
                Debug.Log(hitInfo.collider);
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
}